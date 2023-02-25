using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    public GunType guntype;
    public int gunCount;
    public int clipCount;

    public Weapon(GunType gunType)
    {
        guntype = gunType;
        gunCount = 1;
        clipCount = gunType.ClipCount;
    }

}