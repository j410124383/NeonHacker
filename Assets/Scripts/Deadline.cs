using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deadline : FindGM
{

    private void OnCollisionEnter2D(Collision2D Col)
    {
        if (Col.gameObject.tag=="Player")
        {
            _GM.GameOver();
        }


    }



}
