using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class KitchenObjectSO : ScriptableObject
{
    // game object使用的预制体
    [SerializeField] private Transform prefab;
    // game object名称
    [SerializeField] private string objectName;
    // 物品的sprite，用于生成UI图片
    [SerializeField] private Sprite sprite;

    /// <summary>
    /// 获取object使用的预制体
    /// </summary>
    /// <returns>预制体</returns>
    public Transform GetObjectPrefab()
    {
        return prefab;
    }

    /// <summary>
    /// 获取object的名称
    /// </summary>
    /// <returns>名称</returns>
    public string GetObjectName()
    {
        return objectName;
    }

    /// <summary>
    /// 获取物品的srpite
    /// </summary>
    /// <returns>物品sprite</returns>
    public Sprite GetSprite()
    {
        return sprite;
    }
}
