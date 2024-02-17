using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectParent
{
    /// <summary>
    /// 获取父对象存放物品的位置
    /// </summary>
    /// <returns>父对象存放物品的位置</returns>
    public Transform GetKitchenObjectFollowTransform();

    /// <summary>
    /// 设置父对象的物品
    /// </summary>
    /// <param name="newKitchenObject">物品</param>
    public void SetKitchenObject(KitchenObject newKitchenObject);

    /// <summary>
    /// 获取父对象的物品
    /// </summary>
    /// <returns>父对象的物品</returns>
    public KitchenObject GetKitchenObject();

    /// <summary>
    /// 清空父对象的物品
    /// </summary>
    public void ClearKitchenObject();

    /// <summary>
    /// 判断父对象是否含有物品
    /// </summary>
    /// <returns>true表示含有物品，false表示不含有物品</returns>
    public bool HasKitchenObject();
}
