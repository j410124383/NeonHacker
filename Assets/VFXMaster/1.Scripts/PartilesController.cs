using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartilesController : MonoBehaviour
{
    //当前版本用于管理粒子效果，使其特定时候死亡的功能
    public bool Filp;
    private ParticleSystem[] particleSystems;

    void Start()
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>();


    }

    void Update()
    {
        if (Filp)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale =  Vector3.one;
        }


        bool allStopped = true;

        foreach (ParticleSystem ps in particleSystems)
        {
            if (!ps.isStopped)
            {
                allStopped = false;
            }
        }

        if (allStopped)
            GameObject.Destroy(gameObject);
    }


}
