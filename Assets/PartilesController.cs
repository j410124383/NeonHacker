using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartilesController : MonoBehaviour
{
    //��ǰ�汾���ڹ�������Ч����ʹ���ض�ʱ�������Ĺ���
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
