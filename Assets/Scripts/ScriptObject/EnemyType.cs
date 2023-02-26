using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Enemy",menuName = "Enemy")]
public class EnemyType : ScriptableObject
{

    public float Health;
    public string EnemyName;

    public float AimingTime;        //����ʱ�䣬ˢ��cd
    public float AimingRange;       //������Χ
    public float BulletDanmage;     //�ӵ��˺�
    public GameObject BulletObj;    //�ӵ�ʵ��
    public float BulletSpeed;       //�ӵ��ٶ�
    public float BulletLiveTime;    //�ӵ�����ٶ�

}
