using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_E_Flyball : FindGM
{

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.transform.GetComponent<PlayerController>().Jump();
            GetComponent<EnemyDisplay>().Die();
        }
    }

    

    private void OnTriggerEnter2D(Collider2D col)
    {

    }

}
