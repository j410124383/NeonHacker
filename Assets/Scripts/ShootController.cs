using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.InputSystem;

public class ShootController : FindGM
{
    [LabelText("武器库")]
    public List<Weapon> _WeaponList ;
    private int _WeaponNum=0;

    [LabelText("当前武器")]
    public Weapon weapon;
    [LabelText("实际数值")]
    public Transform _ShootTrans;
    [SerializeField]public Vector2 target;

    private PlayerAction playerAction;

    protected override void Awake()
    {
        base.Awake();
        //初始武器为拳头
        var s = "Assets/Gun/Fist";
        _WeaponList = new List<Weapon>() {new Weapon(Resources.Load(s)as GunType)};
        weapon = _WeaponList[0];
        playerAction = new PlayerAction();
        playerAction.Player.Shoot.started += OnShoot;
        playerAction.Player.SwitchGun.started += OnSwitchGun;
        playerAction.Player.UseSkill.started += OnUseSkill;
        playerAction.Enable();
        SwitchSchemes();
    }

    protected void Update()
    {
        //Vector3 screenPos = Camera.main.WorldToScreenPoint(_ShootTrans.position);
        //Vector3 mousePos = Input.mousePosition;
        //mousePos.z = screenPos.z;


        //target = Camera.main.ScreenToWorldPoint(mousePos);
        //_ShootTrans.LookAt(new Vector2(target.x, target.y));
        //_ShootTrans.transform.Rotate(new Vector3(0, -90, 0));



        if (_WeaponList.Count==0) return;
        _ShootTrans.GetChild(0).GetComponent<SpriteRenderer>().sprite = weapon.guntype.GunSprite;

        // 判断当前使用的控制方案


        if (Mouse.current.delta.ReadValue() != Vector2.zero)
        {
            modSwitch = true;
        }
        else if (Gamepad.current.rightStick.ReadValue() != Vector2.zero)
        {
            modSwitch = false;
        }

        SwitchSchemes();
    }

    /// <summary>
    /// 使用技能
    /// </summary>
    /// <param name="context"></param>
    private void OnUseSkill(InputAction.CallbackContext context)
    {
        _SM.SkillUse(weapon.guntype.Skill);
    }


    /// <summary>
    /// 切换武器
    /// </summary>
    /// <param name="context"></param>
    void OnSwitchGun(InputAction.CallbackContext context)
    {
        _WeaponNum = (_WeaponNum == 0) ? _WeaponList.Count - 1 :
            (_WeaponNum>0? _WeaponNum--: _WeaponNum);

    }

    /// <summary>
    /// 获得武器
    /// </summary>
    /// <param name="gun"></param>
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

    public bool modSwitch;

    public void SwitchSchemes()
    {
        //modSwitch =! modSwitch;

        if (modSwitch)
        {
            playerAction.Player.MouseAim.performed += OnMouseAim;
            playerAction.Player.ShootLook.performed -= OnGamepadAim;

            //mapModText.text = "Use Mouse Mode";
        }
        else
        {
            playerAction.Player.ShootLook.performed += OnGamepadAim;
            playerAction.Player.MouseAim.performed -= OnMouseAim;

            //mapModText.text = "Use Gamepad Mode";
        }
    }

    private void OnMouseAim(InputAction.CallbackContext context)
    {
        // 更新手枪朝向

        // 键鼠模式，使用鼠标位置
        var mousev = context.ReadValue<Vector2>();
        //Debug.Log("鼠标位置："+mousev);
        var aimPos = (Vector2)Camera.main.ScreenToWorldPoint(mousev);

        Vector2 aimDirection = aimPos - (Vector2)transform.position;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        _ShootTrans.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void OnGamepadAim(InputAction.CallbackContext context)
    {


        // 获取摇杆输入
        Vector2 joystickInput = context.ReadValue<Vector2>();

        // 如果摇杆有输入  
        if (joystickInput != Vector2.zero)
        {
            // 计算朝向角度
            float angle = Mathf.Atan2(joystickInput.y, joystickInput.x) * Mathf.Rad2Deg;
            _ShootTrans.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        }

    }


    public void OnShoot(InputAction.CallbackContext context)
    {
        if (weapon.clipCount <= 0) return;

        target -= (Vector2)GameObject.Find("Player").transform.position;
        target.Normalize();
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
