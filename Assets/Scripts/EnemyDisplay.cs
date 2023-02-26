using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDisplay : FindGM
{

    public EnemyType _EnemyType;

    public float Health;
    protected override void Awake()
    {
        base.Awake();
        Health = _EnemyType.Health;
    }

    private void Update()
    {
        if (Health<=0)
        {
            Die();
        }

    }

    public void Behurt(float count)
    {
        Health -= count;
    }


    public void Die()
    {
        if (_GM._TargetCount > 0)
        {
            _GM._TargetCount -= 1;
        }
        Destroy(gameObject);
    }

}
