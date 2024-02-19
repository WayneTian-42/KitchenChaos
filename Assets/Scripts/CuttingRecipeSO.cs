using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CuttingRecipeSO : ScriptableObject
{
    // 原物品
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    // 切片后物品
    [SerializeField] private KitchenObjectSO kitchenObjectSlicesSO;
    // 需要切割的次数
    [SerializeField] private int MaxCuttingTimes;

    /// <summary>
    /// 获取原物品
    /// </summary>
    /// <returns>原物品</returns>
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    /// <summary>
    /// 获取切片后物品
    /// </summary>
    /// <returns>切片后物品</returns>
    public KitchenObjectSO GetKitchenObjectSlicesSO()
    {
        return kitchenObjectSlicesSO;
    }

    /// <summary>
    /// 获取物品需要切割的最大次数
    /// </summary>
    /// <returns>物品要切割的最大次数</returns>
    public int GetMaxCuttingTimes()
    {
        return MaxCuttingTimes;
    }
}
