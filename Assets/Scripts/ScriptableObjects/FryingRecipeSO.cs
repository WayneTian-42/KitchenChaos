using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FryingRecipeSO : ScriptableObject
{
    // 原物品
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    // 煎炸后物品
    [SerializeField] private KitchenObjectSO kitchenObjectFriedSO;
    // 需要煎炸的时间
    [SerializeField] private float maxFryingTime;

    /// <summary>
    /// 获取原物品
    /// </summary>
    /// <returns>原物品</returns>
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    /// <summary>
    /// 获取煎炸后物品
    /// </summary>
    /// <returns>煎炸后物品</returns>
    public KitchenObjectSO GetKitchenObjectFriedSO()
    {
        return kitchenObjectFriedSO;
    }

    /// <summary>
    /// 获取物品需要煎炸的最大时间
    /// </summary>
    /// <returns>物品要煎炸的最大时间</returns>
    public float GetMaxFryingTime()
    {
        return maxFryingTime;
    }
}
