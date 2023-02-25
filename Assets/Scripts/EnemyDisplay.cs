using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDisplay : FindGM
{

    public Enemy _Enemy;

    public float Health;
    protected override void Awake()
    {
        base.Awake();
        Health = _Enemy.Health;
    }

    private void OnCollisionEnter2D(Collision2D col)    
    {

    }

    private void OnCollisionStay2D(Collision2D col)
    {

        if (col.gameObject.tag == "Player")
        {
            col.transform.GetComponent<PlayerController>().Jump();
            Die();
        }
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
