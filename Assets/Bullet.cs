using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum BulletType
{
    ����,
    �з�

}

public class Bullet : FindGM
{

    [HideInInspector] public float FlySpeed;
    [HideInInspector] public float Damage;
    [HideInInspector] public float LiveTime;
    [HideInInspector] public Vector2 target;
    public BulletType bulletType;
    private Rigidbody2D Rig;



    protected override void Awake()
    {
        base.Awake();
        Rig = GetComponent<Rigidbody2D>();
       
    }

    

    private void Start()
    {
        switch (bulletType)
        {
            case BulletType.����:
                gameObject.layer = LayerMask.NameToLayer("Bullet");
                break;
            case BulletType.�з�:
                gameObject.layer = LayerMask.NameToLayer("EBullet");
                break;
            default:
                break;
        }

        //�������ٻ��ƣ��������޴�������
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
            print("1");
            var ed = col.gameObject.GetComponent<EnemyDisplay>();
            ed.Behurt(Damage);
            _UIM.EnemyStateDisplay(ed._Enemy.Name, ed.Health, ed._Enemy.Health);
        }else if (col.gameObject.tag == "Player")
        {

                _PS.Health--;

        }


        Die();
    }



    private void Die()
    {
        //Debug.Log("�������ӵ�");
        Destroy(gameObject);
    
    }
}
