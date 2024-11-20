using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDisplay : MonoBehaviour
{

    public EnemyType _EnemyType;

    public float Health;
    protected virtual void Awake()
    {
     
        Health = _EnemyType.Health;
    }

    private void Update()
    {
        if (Health<=0)
        {
            Die();
        }

    }

    public void Behurt(float count,Quaternion hitroation)
    {
        Health -= count;
        lastrotation = hitroation;
    }

    private Quaternion lastrotation;

    public void Die()
    {

        if (GameManager.instance._TargetCount > 0)
        {
            GameManager.instance._TargetCount -= 1;
        }

        //死亡效果，生成贴花
        EffectManager.instance.StartCoroutine(EffectManager.instance.PlayEffect("DecalBlood",transform.position, lastrotation));

        Destroy(gameObject);
    }

}
