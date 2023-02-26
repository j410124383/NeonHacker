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
    [Header("�ӵ������������յģ�")]
    public float FlySpeed;
    public float Damage;
    public float LiveTime;
    public Vector2 target;
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

    //private void OnTriggerEnter2D(Collider2D col)
    //{

    //}


    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "AI")
        {
            //print("1");
            var ed = col.gameObject.GetComponent<EnemyDisplay>();
            ed.Behurt(Damage);
            _UIM.EnemyStateDisplay(ed._EnemyType.EnemyName, ed.Health, ed._EnemyType.Health);
        }
        if (col.gameObject.tag == "Player")
        {
            print("1");
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
