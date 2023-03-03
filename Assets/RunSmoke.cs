using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunSmoke : FindGM
{
    private ParticleSystem _PS;

    private void Start()
    {
        _PS = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        var X = _P.GetComponent<Rigidbody2D>().velocity.x;
        var Y = _P.GetComponent<Rigidbody2D>().velocity.y;

        if (X == 0 && Y != 0)
        {
            _PS.Play();
        }
        else
        {
            _PS.Stop();
        }


    }
}
