using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : FindGM
{
    public int Health=3;

    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //print("�Ӵ�"+col.gameObject.name);
        if (col.gameObject.tag=="Target")
        {
            print("Completed");
            _GM.GameCompleted();
        }
        else if(col.gameObject.tag == "Prop")
        {
            var g = col.gameObject.GetComponent<Prop>().Fallgun;
            _SC.GunAdd(g);
            print("��ʰȡ��"+g.GunName);
            Destroy(col.gameObject);
        }




    }



}
