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

    [FoldoutGroup("移动")]
    [LabelText("移动速度")]
    public float walkSpeed = 3.5f;
    [FoldoutGroup("移动")]
    public float AccelerateTime = 0.09f;


    [FoldoutGroup("跳跃")]
    public float JumpingSpeed;
    [FoldoutGroup("跳跃")]
    public float FallMultipier;
    [FoldoutGroup("跳跃")]
    public float LowJumpMultiplier;
    [FoldoutGroup("跳跃")]
    [LabelText("是否跳跃")]private bool _isJumping = false;

    [FoldoutGroup("触地检测盒")]
    public Transform _CheckTrans;   //地面盒位置
    [FoldoutGroup("触地检测盒")]
    public Vector2 Size;     //地面盒形接触框的大小
    [FoldoutGroup("触地检测盒")]
    public LayerMask GroundLayerMask; //地面盒形接触框的检测层

    private PhysicsMaterial2D _PM2D_01, _PM2D_02;
    private Animator _Anima;

    public PlayerAction playerAction; //创建一个行为

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
    /// 跳跃行为
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

        float moveX = playerAction.Player.Move.ReadValue<Vector2>().x;
        transform.localScale = (moveX > 0) ? Vector2.one:((moveX<0)?new Vector2(-1,1):transform.localScale);;



        float speed =(moveX == 0 ? 0 : (moveX > 0 ? walkSpeed : -walkSpeed));


        //平滑的移动
        _rigidbody2D.velocity =new Vector2(Mathf.SmoothDamp(
                    _rigidbody2D.velocity.x,
                    speed * Time.fixedDeltaTime * 60,
                    ref _velocityX,
                    AccelerateTime), //此值越小，到达速度越快
                _rigidbody2D.velocity.y);



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

        //角色的动画状态机
        _Anima.SetFloat("Jump_Y", _rigidbody2D.velocity.y); // 负为fall，正为up

        _Anima.SetBool("IsMove", (moveX == 0)? false:true);

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
