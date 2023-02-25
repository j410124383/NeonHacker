using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShootMod{
    ����,
    ����
}

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class GunType : ScriptableObject
{
    //ǹе������
    [Header("��ֵ����")]
    public string GunName;         //ǹ��
    public float ShootSpeed;    //����ٶ�
    public ShootMod shootMod;   //���ģʽ
    [Header("�ӵ�����")]
    public GameObject BulletObj;    //�ӵ�Ԥ����
    public float BulletLiveTime;    //�ӵ����ʱ��
    public int ClipCount;       //��������
    public float BulletSpeed;   //�ӵ��ٶ�
    public float BulletDanmage; //�ӵ��˺�
    [Header("ǹе������")]
    public Sprite GunSprite;    //��Ϸ��ǹ��ͼ
    public Sprite GunIcon;      //UI�ϵ�ǹlogo
    public string SkillName;    //��������
    public Color GunColor;      //����ɫ
    public SkillName Skill;     //����ѡȡ


}
