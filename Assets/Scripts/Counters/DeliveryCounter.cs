using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    /// <summary>
    /// 单例
    /// </summary>
    public static DeliveryCounter Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    /// <summary>
    /// 放置盘子到传送台上
    /// </summary>
    /// <param name="player">角色</param>
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject() == true) // 角色持有物品
        {
            if (player.GetKitchenObject().TryGetPlateKitchenObject(out PlateKitchenObject plateKitchenObject)) // 尝试获取角色手中盘子
            {
                DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
                plateKitchenObject.DestroySelf(); // 成功获取到盘子，将其销毁
            }
        }
    }
}
