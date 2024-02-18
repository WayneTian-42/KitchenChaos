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
}
