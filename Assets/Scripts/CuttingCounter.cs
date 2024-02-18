using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IKitchenObjectParent
{
    // 测试用
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    /// <summary>
    /// 交互函数，角色可以将材料放置在切割柜台上
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
        }
    }

    /// <summary>
    /// 交互函数，角色可以切菜
    /// </summary>
    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() == true) // 柜台为空时
        {
            // 销毁原物品
            GetKitchenObject().DestroySelf();
            // 生成切好后的物品
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, this);   
        }
    }
}
