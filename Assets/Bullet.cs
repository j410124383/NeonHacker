using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : FindGM
{

    [HideInInspector] public float FlySpeed;
    [HideInInspector] public float Damage;
    [HideInInspector] public float LiveTime;
    [HideInInspector] public Vector2 target;

    private Rigidbody2D Rig;



    protected override void Awake()
    {
        base.Awake();
        Rig = GetComponent<Rigidbody2D>();

    }

    private void Start()
    {
        //保底销毁机制，避免无限存留场上
        Invoke("Die", LiveTime);
        //print(LiveTime);
    }


    private void Update()
    {
        transform.position += (Vector3)(FlySpeed * target * Time.deltaTime);

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "AI")
        {
            var ed = col.gameObject.GetComponent<EnemyDisplay>();
           ed.Behurt(Damage);
            _UIM.EnemyStateDisplay(ed._Enemy.Name,ed.Health,ed._Enemy.Health);
        }

        Die();
    }



    private void Die()
    {
        Debug.Log("已销毁子弹");
        Destroy(gameObject);
    
    }
}
