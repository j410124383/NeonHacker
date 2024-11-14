using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxObject : MonoBehaviour
{
    public GameObject obj;

    public ParticleSystem explosionEffect; // 爆炸粒子效果
    public float explosionForce = 500f; // 爆炸的力量
    public float explosionRadius = 5f; // 爆炸的半径
    public float upwardModifier = 1f; // 向上的推力，用于增强爆炸效果

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("碰撞了");
        }
        else if(col.gameObject.tag == "Bullet")
        {
            Debug.Log("碰撞了");
            var x= Instantiate(obj,transform);
            x.transform.SetParent(null);
            foreach (Rigidbody fragment in x.transform.GetComponentsInChildren<Rigidbody>())
            {
                fragment.AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardModifier, ForceMode.Impulse);
            }
            Destroy(gameObject);

        }
    }


}
