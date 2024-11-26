using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New_EffectData", menuName = "Custom/EffectsDataList", order = 1)]
public class SO_EffectData : ScriptableObject
{
    [SerializeField]
    public List<SO_EffectEntry> effectDataList;



}