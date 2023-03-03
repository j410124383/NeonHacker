using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShootController : FindGM
{
    [Header("������")]
    public List<Weapon> _WeaponList ;
    private int _WeaponNum=0;

    [Header("��ǰ����")]
    public Weapon weapon;
    [Header("ʵ����ֵ")]
    public Transform _ShootTrans;
    [SerializeField]public Vector2 target;

    protected override void Awake()
    {
        base.Awake();
        //��ʼ����Ϊȭͷ
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

    //�л�����
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

    //�������
    public void GunAdd(GunType gun)
    {
        //�ж��Ƿ��ѻ��ͬ������
        //�ѻ�ã�δ�����ޣ�ͬ����������������1
        //�ѻ�ã��������ޣ�ͬ�����������ӵ������ָ�40%
        //δ��ã�����һ���µ�weapon�������뵽weaponlist��
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
                Debug.Log("���:��weapon");
                break;
            }
           

        }

       

    }


    void Shoot()
    {

        var bullet = Instantiate(weapon.guntype.BulletObj, _ShootTrans.GetChild(0).GetChild(0));
        bullet.transform.SetParent(_GM.transform);
        var b = bullet.GetComponent<Bullet>();
        //��������
        b.bulletType = BulletType.����;
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

    //���������ķ���
    //��ǰ�����Ƴ�������
    //��ǰ����ת�Ƶ���һ������
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
