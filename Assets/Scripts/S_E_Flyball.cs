using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_E_Flyball : EnemyDisplay
{
    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("Åöµ½ÁËµ¯Ìø¹Ö");
        if (col.gameObject.tag == "Player")
        {
            col.transform.GetComponent<PlayerMovement>().Jump();
            GetComponent<EnemyDisplay>().Die();
        }
    }



}
