using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillName
{
    �޼���,
    ��Ծ,
    ���,

}

public class SkillManager : FindGM
{
   
   
    public void SkillUse(SkillName name)
    {
        if (name == SkillName.�޼���)
        {
            print("�������޼���!");
            return;
        }

        //ʹ�ü���
        switch (name)
        {
            case SkillName.��Ծ:
                Skill_Jump();
                break;
            case SkillName.���:
                Skill_Dash();
                break;
            default:
                break;
        }

        //������������
        if (_SC.weapon.gunCount<=1)
        {
            _SC.DestroyWeapon();
        }
        else
        {
            _SC.weapon.gunCount--;
        }
       

    }




    public void Skill_Jump()
    {
        Debug.Log("������");
        _PC._rigidbody2D.velocity = new Vector2(_PC._rigidbody2D.velocity.x, 7f);

    }

    public void Skill_Dash()
    {
        Debug.Log("���");
        _PC._rigidbody2D.velocity = new Vector2(7f, _PC._rigidbody2D.velocity.y);
    }


}
