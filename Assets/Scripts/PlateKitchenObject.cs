using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    /// <summary>
    /// 能放在盘子上的物品
    /// </summary>
    [SerializeField] private KitchenObjectSO[] validKitchenObjectSOArray;
    /// <summary>
    /// 能放在盘子上的物品，哈希表便于查询
    /// </summary>
    private Hashtable vaildKitchenObjectSOHashTable;
    /// <summary>
    /// 盘子上已有物品
    /// </summary>
    private Hashtable kitchenObjectSOHashTable;

    private void Awake()
    {
        vaildKitchenObjectSOHashTable = new Hashtable();
        kitchenObjectSOHashTable = new Hashtable();
        foreach (KitchenObjectSO kitchenObjectSO in validKitchenObjectSOArray)
        {
            vaildKitchenObjectSOHashTable[kitchenObjectSO] = true;
            kitchenObjectSOHashTable[kitchenObjectSO] = false;
        }
    }

    private void Update()
    {
        // foreach (KitchenObjectSO kitchenObjectSO in kitchenObjectSOHashTable.Keys)
        // {
        //     if ((bool)kitchenObjectSOHashTable[kitchenObjectSO] == true)
        //     {
        //         Debug.Log(kitchenObjectSO);
        //     }
        // }
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
        if ((bool)kitchenObjectSOHashTable[kitchenObjectSO] == true) // 物品已经放在盘子上
        {
            return false;
        }
        kitchenObjectSOHashTable[kitchenObjectSO] = true; // 将物品放在盘子上
        return true;
    }
}
