using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EffectManager))]
public class SOEffectDataEditor : Editor
{


    public override void OnInspectorGUI()
    {

        // 显示原有的 Inspector 内容
        DrawDefaultInspector();

        EditorGUILayout.Space();



        EffectManager effectManager = (EffectManager)target;
        SO_EffectData effectData = effectManager.effectData;



        // 表头
        EditorGUILayout.LabelField("Effect Data List", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Key", GUILayout.Width(100));
        EditorGUILayout.LabelField("Effect Prefab", GUILayout.Width(200));
        EditorGUILayout.LabelField("Pool Size", GUILayout.Width(100));
        EditorGUILayout.EndHorizontal();

        // 数据行
        if (effectData.effectDataList != null)
        {
            for (int i = 0; i < effectData.effectDataList.Count; i++)
            {
                var entry = effectData.effectDataList[i];

                EditorGUILayout.BeginHorizontal();
                entry.key = EditorGUILayout.TextField(entry.key, GUILayout.Width(100));
                entry.effectPrefab = (GameObject)EditorGUILayout.ObjectField(entry.effectPrefab, typeof(GameObject), false, GUILayout.Width(200));
                entry.poolSize = EditorGUILayout.IntField(entry.poolSize, GUILayout.Width(100));
                EditorGUILayout.EndHorizontal();
            }
        }

        // 添加按钮
        if (GUILayout.Button("Add New Entry"))
        {
            effectData.effectDataList.Add(new SO_EffectEntry());
        }

        // 添加按钮
        if (GUILayout.Button("Delet New Entry"))
        {
            effectData.effectDataList.Remove(effectData.effectDataList[effectData.effectDataList.Count-1]);
        }

        // 保存更改
        if (GUI.changed)
        {
            EditorUtility.SetDirty(effectData);
        }
    }
}
