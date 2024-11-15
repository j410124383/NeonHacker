using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EffectUniversalSwitch : MonoBehaviour
{
    private ParticleSystem particle;
    public float decalTime = 10f;
    private DecalProjector decal;


    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        decal = GetComponent<DecalProjector>();

    }

    // Update is called once per frame
    void Update()
    {

        if (particle&&!particle.isPlaying)
        {
            gameObject.SetActive(false); // 特效播放完毕后禁用该对象
        }

        if (decal)
        {
            decal.material.
        }

    }
}
