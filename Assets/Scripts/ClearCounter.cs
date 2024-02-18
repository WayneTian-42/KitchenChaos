using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter, IKitchenObjectParent
{
    /// <summary>
    /// 交互函数，角色可以将材料放置在空柜台上
    /// </summary>
    public override void Interact(Player player)
    {
        // 只有当物品为空时才能交互，保证最多只存在一个物品
        if (!HasKitchenObject())
        {
            // 角色手中有物品
            if (player.HasKitchenObject())
            {
                // 更新物品父对象为柜台
                player.GetKitchenObject().SetKitchenObjectParent(this);       
            }
        }
    }
}
