
using System.Collections.Generic;

using UnityEngine;


[System.Serializable]
public class SO_EffectEntry
{
    [Space]
    public string key;
    //[Header("特效预制体")]
    public GameObject effectPrefab; // 特效预制体
    [HideInInspector] public Transform effectParents;
    public int poolSize; // 对象池大小
    //[Header("特效对象池")]
    [HideInInspector] public List<GameObject> effectPool;

    public bool isRandomRo;


    public  Quaternion GetRandomRo()
    {
        float randomZ = Random.Range(0f, 360f); // 随机生成 0 到 360 的角度
        return  Quaternion.Euler(0,  0, randomZ);     // 返回一个只在 Y 轴旋转的向量
    }



    //public SO_EffectEntry(string _key, GameObject _effectPrefab,Transform _effectp, int _poolsize, List<GameObject> _effectPool)
    //{
    //    key = _key;
    //    effectPrefab = _effectPrefab;
    //    effectParents = _effectp;
    //    poolSize = _poolsize;
    //    effectPool = _effectPool;
    //}


}
