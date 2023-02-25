using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShootMod{
    点射,
    连射
}

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class GunType : ScriptableObject
{
    //枪械的种类
    [Header("数值参数")]
    public string GunName;         //枪名
    public float ShootSpeed;    //射击速度
    public ShootMod shootMod;   //射击模式
    [Header("子弹参数")]
    public GameObject BulletObj;    //子弹预制体
    public float BulletLiveTime;    //子弹存活时间
    public int ClipCount;       //弹夹数量
    public float BulletSpeed;   //子弹速度
    public float BulletDanmage; //子弹伤害
    [Header("枪械表现类")]
    public Sprite GunSprite;    //游戏中枪贴图
    public Sprite GunIcon;      //UI上的枪logo
    public string SkillName;    //技能名称
    public Color GunColor;      //主题色
    public SkillName Skill;     //技能选取


}
