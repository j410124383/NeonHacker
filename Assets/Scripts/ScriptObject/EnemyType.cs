using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Enemy",menuName = "Enemy")]
public class EnemyType : ScriptableObject
{

    public float Health;
    public string EnemyName;

    public float AimingTime;        //攻击时间，刷新cd
    public float AimingRange;       //攻击范围
    public float BulletDanmage;     //子弹伤害
    public GameObject BulletObj;    //子弹实体
    public float BulletSpeed;       //子弹速度
    public float BulletLiveTime;    //子弹存活速度

}
