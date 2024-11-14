using UnityEngine;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine.Diagnostics;
public class PlayerMovement : MonoBehaviour
{

    public static PlayerMovement instance;

    public float moveSpeed = 5f; // 移动速度
    public float jumpHeight = 2f; // 跳跃高度
    public float gravity = -9.8f; // 重力

    public GameObject playerSprite;

    public Transform gunTransform; // 枪的Transform


    private CharacterController characterController;
    private Vector3 velocity; // 角色的速度
    private bool isGrounded;

    private Animator animator;


    private void Awake()
    {
        instance = this;
    }


    void Start()
    {

        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {


        // 检查是否在地面
        isGrounded = characterController.isGrounded;

        ////// 如果在地面并且下落，则重置垂直速度
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // 保持一个小的下落值
        }




        // 获取玩家的输入
        float moveX = Input.GetAxis("Horizontal"); // 左右移动
        //float moveZ = Input.GetAxis("Vertical"); // 前后移动

        // 根据输入计算方向
        Vector3 move = transform.right * moveX/* + transform.forward * moveZ*/;

        // 移动角色
        characterController.Move(move * moveSpeed * Time.deltaTime);


        if (moveX > 0)
        {
            playerSprite.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveX < 0)
        {
            playerSprite.transform.localScale = new Vector3(-1, 1, 1);
        }

        animator.SetBool("ISRUN", moveX != 0);



        // 处理跳跃
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
            
        }

        // 应用重力
        velocity.y += gravity * Time.deltaTime;

        animator.SetFloat("JUMP_Y", velocity.y);

        animator.SetBool("ISGROUND", isGrounded);
        // 处理垂直方向的移动
        characterController.Move(velocity * Time.deltaTime);



    }


    //公开一个跳跃的行为，便于道具调用
    public void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // 跳跃公式
    }

}