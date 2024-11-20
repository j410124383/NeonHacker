using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using Sirenix.OdinInspector;

/// <summary>
/// 特效管理器
/// </summary>
public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;
   public bool isDebug;
    //[LabelText("特效池管理器")] 
    public SO_EffectData effectData;

    private List<SO_EffectEntry> currentEEList;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentEEList = effectData.effectDataList;
        InitializePool();
    }

    void InitializePool()
    {
        for (int i = 0; i < currentEEList.Count; i++)
        {
            currentEEList[i].effectPool.Clear();
            //先给他们创建子物体
            GameObject emptyObject = new GameObject(string.Format("EffectPoll_{0}", currentEEList[i].key));

            // 设置空子物体的父物体为当前物体
            emptyObject.transform.parent = transform;
            currentEEList[i].effectParents = emptyObject.transform;
            InitializeEntry(currentEEList[i]);

        }
    }




    void InitializeEntry(SO_EffectEntry _effects)
    {
        for (int i = 0; i < _effects.poolSize; i++)
        {
            GameObject effect = Instantiate(_effects.effectPrefab, transform.position, Quaternion.identity);
            effect.transform.SetParent(_effects.effectParents);
            effect.GetOrAddComponent<EffectUniversalSwitch>();
            effect.SetActive(false); // 初始时将特效对象设置为非激活状态
            _effects.effectPool.Add(effect);
        }
    }

    /// <summary>
    /// 重载
    /// </summary>
    /// <param name="num"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    public IEnumerator PlayEffect(string key, Vector3 position)
    {
        return PlayEffect(key, position, Quaternion.Euler(0, 0, 0), new Vector3(1, 1, 1));
    }

    public IEnumerator PlayEffect(string key, Vector3 position, Quaternion rotation)
    {
        return PlayEffect(key, position, rotation, new Vector3(1, 1, 1));
    }

    public IEnumerator PlayEffect(string key, Vector3 position, Vector3 scale)
    {
        return PlayEffect(key, position, Quaternion.Euler(0, 0, 0), scale);
    }


    public SO_EffectEntry GetEffect(string key )
    {

  
        foreach (var item in effectData.effectDataList)
        {
            if (item.key == key)
            {
                return item;

            }

        }
        return null; // 当没有找到匹配项时返回 null
        //if (eff == null)
        //{
        //    Debug.LogWarning("调用不存在特效池");
        //}

    }


    public IEnumerator PlayEffect(string key, Vector3 position, Quaternion rotation, Vector3 scale)
    {

        yield return new WaitForEndOfFrame();

        var _effects = GetEffect(key);
        for (int i = 0; i < _effects.effectPool.Count; i++)
        {
            if (!_effects.effectPool[i].activeInHierarchy)
            {
                _effects.effectPool[i].transform.position = position;
                _effects.effectPool[i].transform.localRotation = _effects.isRandomRo? _effects.GetRandomRo():rotation;
                _effects.effectPool[i].transform.localScale = scale;
                _effects.effectPool[i].SetActive(true);

                break;
            }
        }

        if(isDebug)
        Debug.Log(string.Format("<color=yellow>[播放特效完成]</color>:{0}已于{1}播放完成。",_effects.effectPrefab,position));
    }




}
