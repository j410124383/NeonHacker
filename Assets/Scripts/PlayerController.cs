using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.InputSystem;

public class PlayerController : FindGM
{
    public static PlayerController instance;


    [HideInInspector] public Rigidbody2D _rigidbody2D;
    [HideInInspector] public float _velocityX;
    private bool _isOnGround = false;

    [FoldoutGroup("�ƶ�")]
    [LabelText("�ƶ��ٶ�")]
    public float walkSpeed = 3.5f;
    [FoldoutGroup("�ƶ�")]
    public float AccelerateTime = 0.09f;


    [FoldoutGroup("��Ծ")]
    public float JumpingSpeed;
    [FoldoutGroup("��Ծ")]
    public float FallMultipier;
    [FoldoutGroup("��Ծ")]
    public float LowJumpMultiplier;
    [FoldoutGroup("��Ծ")]
    [LabelText("�Ƿ���Ծ")]private bool _isJumping = false;

    [FoldoutGroup("���ؼ���")]
    public Transform _CheckTrans;   //�����λ��
    [FoldoutGroup("���ؼ���")]
    public Vector2 Size;     //������νӴ���Ĵ�С
    [FoldoutGroup("���ؼ���")]
    public LayerMask GroundLayerMask; //������νӴ���ļ���

    private PhysicsMaterial2D _PM2D_01, _PM2D_02;
    private Animator _Anima;

    public PlayerAction playerAction; //����һ����Ϊ

    protected override void Awake()
    {
        instance = this;
        base.Awake();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _Anima = GetComponent<Animator>();
        _PM2D_01 = Resources.Load("PhysicsMaterials/PM_Normal") as PhysicsMaterial2D;
        _PM2D_02 = Resources.Load("PhysicsMaterials/PM_Jump") as PhysicsMaterial2D;
        playerAction = new PlayerAction();


        playerAction.Player.Jump.started += OnJump;
        playerAction.Enable();

    }

    /// <summary>
    /// ��Ծ��Ϊ
    /// </summary>
    /// <param name="context"></param>
    public void OnJump(InputAction.CallbackContext context)
    {
        if ( !_isJumping)
        {
            Jump();

            _isJumping = true;
        }
        if (_isOnGround )
        {
            _isJumping = false;
        }

    }




    void FixedUpdate()
    {
        //�ж��Ƿ���������
        _isOnGround = OnGround();

        if (_isOnGround)
        {
            _rigidbody2D.sharedMaterial = _PM2D_01;
        }
        else
        {
            _rigidbody2D.sharedMaterial = _PM2D_02;
        }

        float moveX = playerAction.Player.Move.ReadValue<Vector2>().x;
        transform.localScale = (moveX > 0) ? Vector2.one:((moveX<0)?new Vector2(-1,1):transform.localScale);;



        float speed =(moveX == 0 ? 0 : (moveX > 0 ? walkSpeed : -walkSpeed));


        //ƽ�����ƶ�
        _rigidbody2D.velocity =new Vector2(Mathf.SmoothDamp(
                    _rigidbody2D.velocity.x,
                    speed * Time.fixedDeltaTime * 60,
                    ref _velocityX,
                    AccelerateTime), //��ֵԽС�������ٶ�Խ��
                _rigidbody2D.velocity.y);



        //���ٶ��Ǹ�ֵ������׹�У���������׹��
        if (_rigidbody2D.velocity.y < 0)
        {
            _rigidbody2D.velocity += Vector2.up *
                                    Physics2D.gravity.y *  //������y��һ����ֵ
                                    FallMultipier *
                                    Time.fixedDeltaTime;
            //print(Physics2D.gravity.y);
        }
        //���������ʱ����Ҳ��ٰ���Ծ���ˣ�����������
        else if (_rigidbody2D.velocity.y > 0 && Input.GetAxis("Jump") != 1)
        {
            _rigidbody2D.velocity += Vector2.up *
                                    Physics2D.gravity.y *
                                    LowJumpMultiplier *
                                    Time.fixedDeltaTime;
        }

        //��ɫ�Ķ���״̬��
        _Anima.SetFloat("Jump_Y", _rigidbody2D.velocity.y); // ��Ϊfall����Ϊup

        _Anima.SetBool("IsMove", (moveX == 0)? false:true);

    }
    private bool OnGround()
    {
        //�����ж���
        //�������������ġ���С���Ƕȡ���ɸѡ��
        Collider2D Coll = Physics2D.OverlapBox(_CheckTrans.position,Size, 0, GroundLayerMask);
        if (Coll != null)
        {
            return true;
        }
        return false;
    }

    //����һ����Ծ����Ϊ�����ڵ��ߵ���
    public void Jump()
    {
        //��һ�����ϵ����ٶ�
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, JumpingSpeed);
    }



}
