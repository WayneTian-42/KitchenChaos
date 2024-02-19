using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IKitchenObjectParent, IHasProgressBar
{
    /// <summary>
    /// 事件：角色切菜
    /// </summary>
    public event EventHandler OnCut;
    /// <summary>
    /// 事件：切割进度发生变化
    /// </summary>
    public event EventHandler<IHasProgressBar.OnProgressChangedEvnetArgs> OnProgressChanged;
    /// <summary>
    /// 菜单，可以查询物品被切片后的状态
    /// </summary>
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    /// <summary>
    /// 当前物品对应的切割菜谱
    /// </summary>
    private CuttingRecipeSO cuttingRecipeSO;
    /// <summary>
    /// 哈希表，存储物品以及对应的菜谱
    /// </summary>
    private Hashtable cuttingRecipeSOHT;
    /// <summary>
    /// 当前切割次数
    /// </summary>
    private int cuttingTimes;

    private void Awake()
    {
        // 根据切割菜谱初始化哈希表
        cuttingRecipeSOHT = new Hashtable();
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            cuttingRecipeSOHT.Add(cuttingRecipeSO.GetKitchenObjectSO(), cuttingRecipeSO);
        }
    }

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
                // // 查询角色手中物品是否能够被切片
                cuttingRecipeSO = (CuttingRecipeSO)cuttingRecipeSOHT[player.GetKitchenObject().GetKitchenObjectSO()];
                // cutKitchenObjectSO = GetCutKitchenObjectSOForKitchenObject(player.GetKitchenObject().GetKitchenObjectSO());
                // 物品能被切片时才可以放下
                if (cuttingRecipeSO != null)
                {
                    // 更新物品父对象为柜台
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    // 重置切割次数
                    cuttingTimes = 0;
                    // 清空进度条
                    OnProgressChanged?.Invoke(this, new IHasProgressBar.OnProgressChangedEvnetArgs());
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
        if (HasKitchenObject() == true && cuttingRecipeSO != null) // 柜台不为空且可以切片时
        // if (HasKitchenObject() == true && cuttingRecipeSOHT.ContainsKey(GetKitchenObject().GetKitchenObjectSO()))
        {
            ++cuttingTimes;
            // 更新动画
            OnProgressChanged?.Invoke(this, new IHasProgressBar.OnProgressChangedEvnetArgs((float)cuttingTimes / cuttingRecipeSO.GetMaxCuttingTimes()));
            // 切菜动画
            OnCut?.Invoke(this, EventArgs.Empty);
            if (cuttingTimes >= cuttingRecipeSO.GetMaxCuttingTimes()) // 切割完成后
            {
                // 销毁原物品
                GetKitchenObject().DestroySelf();
                // 生成切好后的物品
                KitchenObject.SpawnKitchenObject(cuttingRecipeSO.GetKitchenObjectSlicesSO(), this);
                // KitchenObject.SpawnKitchenObject((KitchenObjectSO)cuttingRecipeSOHT[GetKitchenObject().GetKitchenObjectSO()], this);
                // 物品切过一次后不能再切片
                cuttingRecipeSO = null;
            }
        }
    }

    /// <summary>
    /// 查询当前物品切片后的物品
    /// </summary>
    /// <param name="kitchenObjectSO">当前物品</param>
    /// <returns>切片后物品</returns>
    private KitchenObjectSO GetCutKitchenObjectSOForKitchenObject(KitchenObjectSO kitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithKitchenObject(kitchenObjectSO);
        if (cuttingRecipeSO == null)
        {
            return null;
        }
        else
        {
            return cuttingRecipeSO.GetKitchenObjectSlicesSO();
        }
    }

    /// <summary>
    /// 根据当前物品获取对应的切割菜谱
    /// </summary>
    /// <param name="kitchenObjectSO">当前物品</param>
    /// <returns>切割菜谱</returns>
    private CuttingRecipeSO GetCuttingRecipeSOWithKitchenObject(KitchenObjectSO kitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.GetKitchenObjectSO() == kitchenObjectSO)
                return cuttingRecipeSO;
        }
        return null;
    }
}
