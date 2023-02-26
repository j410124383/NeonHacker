using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : FindGM
{
    public int Health=3;

    private void Update()
    {
        if (Health<=0)
        {
            _GM.GameOver();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Target")
        {
            print("Completed");
            _GM.GameCompleted();
        }
        else if (col.gameObject.tag == "Prop")
        {
            var g = col.gameObject.GetComponent<Prop>().Fallgun;
            _SC.GunAdd(g);
            Destroy(col.gameObject);
        }
        else if (col.gameObject.tag == "Gift")
        {
            _GM.GetGift();
            Destroy(col.gameObject);
        }
        //else if (col.gameObject.GetComponent<EnemyDisplay>()._EnemyType.EnemyName=="FallBall")
        //{
        //    _PC.Jump();
        //    col.gameObject.GetComponent<EnemyDisplay>().Die();
        //}
    }


}
