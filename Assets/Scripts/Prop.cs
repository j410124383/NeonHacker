using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : FindGM
{
    public GunType Fallgun;

    protected override void Awake()
    {
        base.Awake();
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Fallgun.GunIcon;


    }

}
