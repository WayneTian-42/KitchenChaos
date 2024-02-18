using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IKitchenObjectParent
{
    // 菜单，可以查询物品被切片后的状态
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    // 切片后状态
    private KitchenObjectSO cutKitchenObjectSO;

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
                // 查询角色手中物品是否能够被切片
                cutKitchenObjectSO = GetCutKitchenObjectSOForKitchenObject(player.GetKitchenObject().GetKitchenObjectSO());
                // 物品能被切片时才可以放下
                if (cutKitchenObjectSO != null)
                {
                    // 更新物品父对象为柜台
                    player.GetKitchenObject().SetKitchenObjectParent(this);       
                }
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
        if (HasKitchenObject() == true && cutKitchenObjectSO) // 柜台不为空且可以切片时
        {
            // 销毁原物品
            GetKitchenObject().DestroySelf();
            // 生成切好后的物品
            KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
            // 物品切过一次后不能再切片
            cutKitchenObjectSO = null;
        }
    }

    /// <summary>
    /// 查询当前物品切片后的物品
    /// </summary>
    /// <param name="kitchenObjectSO">当前物品</param>
    /// <returns>切片后物品</returns>
    private KitchenObjectSO GetCutKitchenObjectSOForKitchenObject(KitchenObjectSO kitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.GetKitchenObjectSO() == kitchenObjectSO)
                return cuttingRecipeSO.KitchenObjectSlicesSO();
        }
        return null;
    }
}
