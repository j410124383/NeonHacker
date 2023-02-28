using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillName
{
    无技能,
    跳跃,
    冲刺,

}

public class SkillManager : FindGM
{

    [Header("需要暴露的参数")]
    public float DashSpeed =40f;
    public float JumpPower =7f;
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
        if (_SC.weapon.gunCount<=1)
        {
            _SC.DestroyWeapon();
        }
        else
        {
            _SC.weapon.gunCount--;
        }

        _CM.GetComponent<Animator>().SetTrigger("ISACTIVE");
        //print(_CM.name);

    }




    public void Skill_Jump()
    {
        //Debug.Log("二段跳");
        _PC._rigidbody2D.velocity = new Vector2(_PC._rigidbody2D.velocity.x, JumpPower);

    }


    public void Skill_Dash()
    {
        //Debug.Log("冲刺");
        var x = _P.transform.localScale.x;
        _PC._rigidbody2D.velocity = new Vector2(DashSpeed * x, _PC._rigidbody2D.velocity.y);
    }


}
