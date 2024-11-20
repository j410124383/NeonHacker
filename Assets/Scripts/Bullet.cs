using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum BulletType
{
    己方,
    敌方

}

public class Bullet : FindGM
{
    [Header("子弹参数（被接收的）")]
    public float FlySpeed;
    public float Damage;
    public float LiveTime;
    public Vector2 target;
    public BulletType bulletType;
    private Rigidbody Rig;



    protected override void Awake()
    {
        base.Awake();
        Rig = GetComponent<Rigidbody>();
       
    }

    

    private void Start()
    {
        switch (bulletType)
        {
            case BulletType.己方:
                gameObject.layer = LayerMask.NameToLayer("Bullet");
                break;
            case BulletType.敌方:
                gameObject.layer = LayerMask.NameToLayer("EBullet");
                break;
            default:
                break;
        }

        //保底销毁机制，避免无限存留场上
        Invoke("Die", LiveTime);
        //print(LiveTime);
    }


    private void Update()
    {
        transform.position += (Vector3)(FlySpeed * target * Time.deltaTime);

    }



    private void OnCollisionEnter(Collision col)
    {


        if (col.gameObject.tag == "AI")
        {
            //print("1");
            var ed = col.gameObject.GetComponent<EnemyDisplay>();
            ed.Behurt(Damage,transform.localRotation);
            _UIM.EnemyStateDisplay(ed._EnemyType.EnemyName, ed.Health,Damage, ed._EnemyType.Health);
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
        string i = "BulletDie_Red";
        if (gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            i = "BulletDie_Yellow";
        }
        EffectManager.instance.StartCoroutine(EffectManager.instance.PlayEffect(i, transform.position));

        Debug.Log("已销毁子弹");

        Destroy(gameObject);
    
    }
}
