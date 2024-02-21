using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    /// <summary>
    /// 放入物品事件
    /// </summary>
    public event EventHandler<OnIngredientAddedArgs> OnIngredientAdded;
    /// <summary>
    /// 放入物品事件的参数
    /// </summary>
    public class OnIngredientAddedArgs : EventArgs
    {
        /// <summary>
        /// 放入的物品
        /// </summary>
        public KitchenObjectSO kitchenObjectSO;
        public OnIngredientAddedArgs(KitchenObjectSO _kitchenOjbectSO)
        {
            kitchenObjectSO = _kitchenOjbectSO;
        }
    }
    /// <summary>
    /// 能放在盘子上的物品
    /// </summary>
    [SerializeField] private KitchenObjectSO[] validKitchenObjectSOArray;
    /// <summary>
    /// 能放在盘子上的物品，哈希表便于查询
    /// </summary>
    private Dictionary<KitchenObjectSO, bool> vaildKitchenObjectSOHashTable;
    /// <summary>
    /// 盘子上已有物品
    /// </summary>
    private Dictionary<KitchenObjectSO, bool> kitchenObjectSODict;

    private void Awake()
    {
        vaildKitchenObjectSOHashTable = new Dictionary<KitchenObjectSO, bool>();
        kitchenObjectSODict = new Dictionary<KitchenObjectSO, bool>();
        foreach (KitchenObjectSO kitchenObjectSO in validKitchenObjectSOArray)
        {
            vaildKitchenObjectSOHashTable[kitchenObjectSO] = true;
            kitchenObjectSODict[kitchenObjectSO] = false;
        }
    }

    /// <summary>
    /// 尝试向盘子上放置物品
    /// </summary>
    /// <param name="kitchenObjectSO">物品</param>
    /// <returns>
    /// true：放置成功
    /// false：放置失败，物品已经放在盘子上或者不能放在盘子上
    /// </returns>
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (vaildKitchenObjectSOHashTable.ContainsKey(kitchenObjectSO) == false) // 物品不能放在盘子上
        {
            return false;
        }
        if (kitchenObjectSODict[kitchenObjectSO] == true) // 物品已经放在盘子上
        {
            return false;
        }
        kitchenObjectSODict[kitchenObjectSO] = true; // 将物品放在盘子上
        OnIngredientAdded?.Invoke(this, new OnIngredientAddedArgs(kitchenObjectSO)); // 发布事件
        return true;
    }

    /// <summary>
    /// 获取盘子中所有物品
    /// </summary>
    /// <returns>盘子中所有物品</returns>
    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSODict.Where(key => key.Value == true).Select(pair => pair.Key).ToList();
    }
}
