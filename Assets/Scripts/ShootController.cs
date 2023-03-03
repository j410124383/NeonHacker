using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShootController : FindGM
{
    [Header("武器库")]
    public List<Weapon> _WeaponList ;
    private int _WeaponNum=0;

    [Header("当前武器")]
    public Weapon weapon;
    [Header("实际数值")]
    public Transform _ShootTrans;
    [SerializeField]public Vector2 target;

    protected override void Awake()
    {
        base.Awake();
        //初始武器为拳头
        var s = "Assets/Gun/Fist";
        _WeaponList = new List<Weapon>() {new Weapon(Resources.Load(s)as GunType)};
        weapon = _WeaponList[0];
    }

    protected void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(_ShootTrans.position);
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = screenPos.z;


        target = Camera.main.ScreenToWorldPoint(mousePos);
        _ShootTrans.LookAt(new Vector2(target.x, target.y));
        _ShootTrans.transform.Rotate(new Vector3(0, -90, 0));

        if (Input.GetKeyDown(KeyCode.Q))
        {
            GunSwitch();
        }


        if (_WeaponList.Count==0) return;
        _ShootTrans.GetChild(0).GetComponent<SpriteRenderer>().sprite = weapon.guntype.GunSprite;


        if (Input.GetButtonDown("Fire1") && weapon.clipCount >0)
        {
            target -= (Vector2)GameObject.Find("Player").transform.position;
            target.Normalize();
            Shoot();
        }

        if(Input.GetButtonDown("Fire2"))
        {
            _SM.SkillUse(weapon.guntype.Skill);
        }

    }

    //切换武器
    void GunSwitch()
    {
        if (_WeaponNum==0)
        {
            _WeaponNum=_WeaponList.Count-1;
        }
        else if(_WeaponNum>0)
        {
            _WeaponNum--;
        }
        weapon = _WeaponList[_WeaponNum];
    }

    //获得武器
    public void GunAdd(GunType gun)
    {
        //判定是否已获得同类武器
        //已获得，未满上限：同类武器卡，数量加1
        //已获得，已满上限：同类武器卡，子弹数量恢复40%
        //未获得，创造一个新的weapon，并加入到weaponlist中
        for (int i = 0; i < _WeaponList.Count; i++)
        {
            var W = _WeaponList[i];
            if (W.guntype.GunName == gun.GunName)
            {
                W.clipCount += W.guntype.ClipCount ;

                if (W.gunCount<3)
                {
                    W.gunCount++;
                }
                break;
            }else if(W.guntype!=gun &&i==_WeaponList.Count-1){
                var x = new Weapon(gun);
                _WeaponList.Add(x);
                weapon = x;
                _WeaponNum = _WeaponList.Count - 1;
                Debug.Log("获得:新weapon");
                break;
            }
           

        }

       

    }


    void Shoot()
    {

        var bullet = Instantiate(weapon.guntype.BulletObj, _ShootTrans.GetChild(0).GetChild(0));
        bullet.transform.SetParent(_GM.transform);
        var b = bullet.GetComponent<Bullet>();
        //参数赋予
        b.bulletType = BulletType.己方;
        b.FlySpeed = weapon.guntype.BulletSpeed;
        b.Damage = weapon.guntype.BulletDanmage;
        b.transform.GetChild(0).GetComponent<SpriteRenderer>().color = weapon.guntype.GunColor;
        b.LiveTime = weapon.guntype.BulletLiveTime;
        b.target = target;

        if (weapon.clipCount > 1)
        {
            weapon.clipCount--;
        }
        else
        {
            DestroyWeapon();
        }

    }

    //销毁武器的方法
    //当前武器移除武器卡
    //当前武器转移到上一个武器
    public void DestroyWeapon()
    {
        _WeaponList.Remove(weapon);
        if (_WeaponList.Count>0)
        {
            weapon = _WeaponList[_WeaponList.Count - 1];
        }
        else
        {
            _GM.GameOver();
        }


    }



}
