using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : FindGM
{

    [HideInInspector] public Rigidbody2D _rigidbody2D;
    [HideInInspector] public float _velocityX;
    private bool _isOnGround = false;


    [Header("移动速度")]
    public float walkSpeed = 3.5f;
    public float AccelerateTime = 0.09f;
    [Header("跳跃")]
    public float JumpingSpeed;
    public float FallMultipier;
    public float LowJumpMultiplier;
    private bool _isJumping = false;

    [Header("触地检测盒")]
    public Transform _CheckTrans;   //地面盒位置
    public Vector2 Size;     //地面盒形接触框的大小
    public LayerMask GroundLayerMask; //地面盒形接触框的检测层

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
        //判断是否碰到地面
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


        //velocity：刚体的线速度
        float speed =
            (Input.GetAxis("Horizontal") == 0 ?
                0 : (Input.GetAxis("Horizontal") > 0 ?
                      walkSpeed : -walkSpeed));

        //平滑的移动
        _rigidbody2D.velocity =new Vector2(Mathf.SmoothDamp(
                    _rigidbody2D.velocity.x,
                    speed * Time.fixedDeltaTime * 60,
                    ref _velocityX,
                    AccelerateTime), //此值越小，到达速度越快
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

        //线速度是负值（即下坠中）（加速下坠）
        if (_rigidbody2D.velocity.y < 0)
        {
            _rigidbody2D.velocity += Vector2.up *
                                    Physics2D.gravity.y *  //重力的y是一个负值
                                    FallMultipier *
                                    Time.fixedDeltaTime;
            //print(Physics2D.gravity.y);
        }
        //当玩家上升时，玩家不再按跳跃键了（减缓上升）
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
        //物理判定框
        //参数：盒子中心、大小、角度、层筛选器
        Collider2D Coll = Physics2D.OverlapBox(_CheckTrans.position,Size, 0, GroundLayerMask);
        if (Coll != null)
        {
            return true;
        }
        return false;
    }

    //公开一个跳跃的行为，便于道具调用
    public void Jump()
    {
        //给一个向上的线速度
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, JumpingSpeed);
    }



}
