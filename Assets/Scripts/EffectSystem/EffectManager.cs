using System.Collections;
using System.Collections.Generic;
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
    public List<SO_Effects> effects;

 

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        for (int i = 0; i < effects.Count; i++)
        {
            effects[i].effectPool.Clear();
            //先给他们创建子物体
            GameObject emptyObject = new GameObject(string.Format( "EffectPoll_{0}",effects[i].effectPrefab.name));

            // 设置空子物体的父物体为当前物体
            emptyObject.transform.parent = transform;
            effects[i].effectParents = emptyObject.transform;
            InitializePool(effects[i]);

        }
        

    }

    void InitializePool(SO_Effects _effects)
    {
        for (int i = 0; i < _effects.poolSize; i++)
        {
            GameObject effect = Instantiate(_effects.effectPrefab, transform.position, Quaternion.identity);
            effect.transform.SetParent(_effects.effectParents);
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
    public IEnumerator PlayEffect(int num, Vector3 position)
    {
        return PlayEffect(num, position, Quaternion.Euler(0, 0, 0), new Vector3(1, 1, 1));
    }

    public IEnumerator PlayEffect(int num, Vector3 position, Quaternion rotation)
    {
        return PlayEffect(num, position, rotation, new Vector3(1, 1, 1));
    }

    public IEnumerator PlayEffect(int num, Vector3 position, Vector3 scale)
    {
        return PlayEffect(num, position, Quaternion.Euler(0, 0, 0), scale);
    }


    public IEnumerator PlayEffect(int num, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        if (num >= effects.Count)
        {
            Debug.LogWarning("调用不存在特效池");
        }

        yield return new WaitForEndOfFrame();

        var _effects = effects[num];
        for (int i = 0; i < _effects.effectPool.Count; i++)
        {
            if (!_effects.effectPool[i].activeInHierarchy)
            {
                _effects.effectPool[i].transform.position = position;
                _effects.effectPool[i].transform.localRotation = rotation;
                _effects.effectPool[i].transform.localScale = scale;
                _effects.effectPool[i].SetActive(true);

                break;
            }
        }

        if(isDebug)
        Debug.Log(string.Format("<color=yellow>[播放特效完成]</color>:{0}已于{1}播放完成。",_effects.effectPrefab,position));
    }
}
