using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.InputSystem;

public class ShootController : FindGM
{
    [LabelText("������")]
    public List<Weapon> _WeaponList ;
    private int _WeaponNum=0;

    [LabelText("��ǰ����")]
    public Weapon weapon;
    [LabelText("ʵ����ֵ")]
    public Transform _ShootTrans;
    [SerializeField]public Vector2 target;

    private PlayerAction playerAction;

    protected override void Awake()
    {
        base.Awake();
        //��ʼ����Ϊȭͷ
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

        // �жϵ�ǰʹ�õĿ��Ʒ���


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
    /// ʹ�ü���
    /// </summary>
    /// <param name="context"></param>
    private void OnUseSkill(InputAction.CallbackContext context)
    {
        _SM.SkillUse(weapon.guntype.Skill);
    }


    /// <summary>
    /// �л�����
    /// </summary>
    /// <param name="context"></param>
    void OnSwitchGun(InputAction.CallbackContext context)
    {
        _WeaponNum = (_WeaponNum == 0) ? _WeaponList.Count - 1 :
            (_WeaponNum>0? _WeaponNum--: _WeaponNum);

    }

    /// <summary>
    /// �������
    /// </summary>
    /// <param name="gun"></param>
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
        // ������ǹ����

        // ����ģʽ��ʹ�����λ��
        var mousev = context.ReadValue<Vector2>();
        //Debug.Log("���λ�ã�"+mousev);
        var aimPos = (Vector2)Camera.main.ScreenToWorldPoint(mousev);

        Vector2 aimDirection = aimPos - (Vector2)transform.position;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        _ShootTrans.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void OnGamepadAim(InputAction.CallbackContext context)
    {


        // ��ȡҡ������
        Vector2 joystickInput = context.ReadValue<Vector2>();

        // ���ҡ��������  
        if (joystickInput != Vector2.zero)
        {
            // ���㳯��Ƕ�
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
