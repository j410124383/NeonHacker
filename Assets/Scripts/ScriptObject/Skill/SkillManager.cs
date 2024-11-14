using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public enum SkillName
{
    无技能,
    跳跃,
    冲刺,

}

public class SkillManager : MonoBehaviour
{

    public static SkillManager instance;



    [Header("需要暴露的参数")]
    public float DashSpeed =40f;
    public float JumpPower =7f;

    private PlayerMovement _PC;


    private void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        _PC = PlayerMovement.instance;
    }


    public void SkillUse(SkillName name)
    {
        if (name == SkillName.无技能)
        {
            //print("该武器无技能!");
            return;
        }

        //使用技能
        switch (name)
        {
            case SkillName.跳跃:
                Skill_Jump();
                break;
            case SkillName.冲刺:
                Skill_Dash();
                break;
            default:
                break;
        }

        //消耗武器数量

        var _SC = ShootController.instance;

        if (_SC.weapon.gunCount<=1)
        {
            _SC.DestroyWeapon();
        }
        else
        {
            _SC.weapon.gunCount--;
        }

        GameObject.FindWithTag("CM").GetComponent<Animator>().SetTrigger("ISACTIVE");
        //print(_CM.name);

    }




    public void Skill_Jump()
    {
        //Debug.Log("二段跳");
        //var _rigidbody = _PC.GetComponent<Rigidbody>();

        //_rigidbody.velocity = new Vector2(_rigidbody.velocity.x, JumpPower);
        _PC.Jump();
    }


    public void Skill_Dash()
    {
        //Debug.Log("冲刺");
        var _rigidbody = _PC.GetComponent<Rigidbody>();
        var x = PlayerState.instance.transform.localScale.x;
        _rigidbody.velocity = new Vector2(DashSpeed * x, _rigidbody.velocity.y);
    }


}
