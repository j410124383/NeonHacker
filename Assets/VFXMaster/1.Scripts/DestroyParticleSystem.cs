using UnityEngine;

public class DestroyParticleSystem : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        // 如果粒子系统不在播放，则销毁该对象
        if (!_particleSystem.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}