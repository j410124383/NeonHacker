using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : FindGM
{

    [HideInInspector] public Rigidbody2D _rigidbody2D;
    [HideInInspector] public float _velocityX;
    private bool _isOnGround = false;


    [Header("�ƶ��ٶ�")]
    public float walkSpeed = 3.5f;
    public float AccelerateTime = 0.09f;
    [Header("��Ծ")]
    public float JumpingSpeed;
    public float FallMultipier;
    public float LowJumpMultiplier;
    private bool _isJumping = false;

    [Header("���ؼ���")]
    public Transform _CheckTrans;   //�����λ��
    public Vector2 Size;     //������νӴ���Ĵ�С
    public LayerMask GroundLayerMask; //������νӴ���ļ���

    private PhysicsMaterial2D _PM2D_01, _PM2D_02;

    protected override void Awake()
    {
        base.Awake();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _PM2D_01 = Resources.Load("PhysicsMaterials/PM_Normal") as PhysicsMaterial2D;
        _PM2D_02 = Resources.Load("PhysicsMaterials/PM_Jump") as PhysicsMaterial2D;
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

        var Hor = Input.GetAxisRaw("Horizontal");
        if (Hor>0)
        {
            transform.localScale = new Vector2(1,1);
        }
        else if(Hor<0)
        {
            transform.localScale = new Vector2(-1, 1);
        }


        //velocity����������ٶ�
        float speed =
            (Input.GetAxis("Horizontal") == 0 ?
                0 : (Input.GetAxis("Horizontal") > 0 ?
                      walkSpeed : -walkSpeed));

        //ƽ�����ƶ�
        _rigidbody2D.velocity =new Vector2(Mathf.SmoothDamp(
                    _rigidbody2D.velocity.x,
                    speed * Time.fixedDeltaTime * 60,
                    ref _velocityX,
                    AccelerateTime), //��ֵԽС�������ٶ�Խ��
                _rigidbody2D.velocity.y);



        if (Input.GetAxis("Jump") == 1 && !_isJumping)
        {
            Jump();

            _isJumping = true;
        }
        if (_isOnGround && Input.GetAxis("Jump") == 0)
        {
            _isJumping = false;
        }

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

        var a = _CM.GetComponent<Animator>();
        if (_rigidbody2D.velocity==Vector2.zero)
        {
            a.SetBool("IsMove", false);
        }
        else
        {
            a.SetBool("IsMove", true);
        }
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
