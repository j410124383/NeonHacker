using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


[CreateAssetMenu(fileName = "Effects", menuName = "Custom/Effects", order = 1)]
public class SO_Effects : ScriptableObject
{
    [LabelText("特效预制体")]
    public GameObject effectPrefab; // 特效预制体
    public Transform effectParents;
    public int poolSize ; // 对象池大小
    [Header("特效对象池")]
    [SerializeField]
    public List<GameObject> effectPool = new List<GameObject>(); 

}
