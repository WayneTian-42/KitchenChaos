using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter, IKitchenObjectParent
{
    /// <summary>
    /// 交互函数，角色可以将材料放置在空柜台上，或者从柜台上拿取物品
    /// </summary>
    public override void Interact(Player player)
    {
        if (HasKitchenObject() == false) // 柜台为空时
        {
            // 角色手中有物品
            if (player.HasKitchenObject() == true)
            {
                // 更新物品父对象为柜台
                player.GetKitchenObject().SetKitchenObjectParent(this);       
            }
        }
        else // 柜台不为空时
        {
            if (player.HasKitchenObject() == false) // 角色手中没有物品
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
            else if (player.GetKitchenObject().TryGetPlateKitchenObject(out PlateKitchenObject plateKitchenObject)) // 获取盘子
            {
                if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) // 尝试将物品放在盘子上
                {
                    GetKitchenObject().DestroySelf(); // 放置成功后销毁原物品
                }
            }
            else if (GetKitchenObject().TryGetPlateKitchenObject(out plateKitchenObject)) // 柜台上是盘子
            {
                if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) // 尝试将物品放在盘子上
                {
                    player.GetKitchenObject().DestroySelf(); // 放置成功后销毁原物品
                }
            }
        }
    }
}
