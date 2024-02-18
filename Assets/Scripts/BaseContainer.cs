using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour
{
    // 柜台顶部中心位置
    [SerializeField] private Transform counterTopPoint;
    // 柜台的物品
    private KitchenObject kitchenObject;

    /// <summary>
    /// 角色和柜台交互功能
    /// </summary>
    /// <param name="player">角色</param>
    public virtual void Interact(Player player)
    {
        Debug.LogError("BaseCount.Interact()");
    }

    /// <summary>
    /// 角色和柜台交互功能：切菜
    /// </summary>
    /// <param name="player">角色</param>
    public virtual void InteractAlternate(Player player)
    {
        Debug.Log("BaseCount.Interact()");
        // Debug.LogError("BaseCount.Interact()");
    }

    /// <summary>
    /// 获取柜台顶部中心位置
    /// </summary>
    /// <returns>柜台顶部中心位置</returns>
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    /// <summary>
    /// 设置柜台的物品
    /// </summary>
    /// <param name="newKitchenObject">物品</param>
    public void SetKitchenObject(KitchenObject newKitchenObject)
    {
        if (kitchenObject == null)
        {
            kitchenObject = newKitchenObject;
        }
        else
        {
            Debug.LogError("Counter already had a kitchen object!");
        }
    }

    /// <summary>
    /// 获取柜台的物品
    /// </summary>
    /// <returns>柜台的物品</returns>
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    /// <summary>
    /// 清空柜台物品
    /// </summary>
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    /// <summary>
    /// 判断柜台是否含有物品
    /// </summary>
    /// <returns>true表示含有物品，false表示不含有物品</returns>
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
