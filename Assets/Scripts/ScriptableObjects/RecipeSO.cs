using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 菜谱结构
/// </summary>
// [CreateAssetMenu()]
public class RecipeSO : ScriptableObject, IEquatable<RecipeSO>
{
    /// <summary>
    /// 菜谱中的全部材料
    /// </summary>
    public List<KitchenObjectSO> kitchenObjectSOList;
    /// <summary>
    /// 菜谱名称
    /// </summary>
    public string recipeName;

    /// <summary>
    /// 判断两个菜单元素是否相同，用于实现哈希
    /// </summary>
    /// <param name="other">另一个菜单</param>
    /// <returns>
    /// true: 菜单相同
    /// false: 菜单不同
    /// </returns>
    public bool Equals(RecipeSO other)
    {
        // if (kitchenObjectSOList.Count != other.kitchenObjectSOList.Count)
        //     return false;
        // 使用字典统计元素的频次
        Dictionary<KitchenObjectSO, int> frequency1 = GetElementFrequency(kitchenObjectSOList);
        Dictionary<KitchenObjectSO, int> frequency2 = GetElementFrequency(other.kitchenObjectSOList);

        // 比较两个字典是否相等
        bool equal = frequency1.All(kv => frequency2.TryGetValue(kv.Key, out int value) && kv.Value == value);
        // 若other和自身相等，更改其名字（用于将盘中物品更名为实际菜谱）
        other.recipeName = equal ? recipeName : other.recipeName;
        return equal;
    }

    /// <summary>
    /// 获取当前元素的哈希码，用于实现哈希
    /// </summary>
    /// <returns>菜单元素数量，当作哈希值</returns>
    public override int GetHashCode()
    {
        return kitchenObjectSOList.Count;
    }

    /// <summary>
    /// 获取菜单中元素出现频率，用于比较菜单是否相同
    /// </summary>
    /// <param name="list">菜单使用的材料</param>
    /// <returns>菜单中材料出现频率</returns>
    private Dictionary<KitchenObjectSO, int> GetElementFrequency(List<KitchenObjectSO> list)
    {
        Dictionary<KitchenObjectSO, int> frequency = new Dictionary<KitchenObjectSO, int>();

        foreach (KitchenObjectSO item in list)
        {
            if (frequency.TryGetValue(item, out _))
            {
                ++frequency[item];
            }
            else
            {
                frequency[item] = 1;
            }
        }

        return frequency;
    }
}
