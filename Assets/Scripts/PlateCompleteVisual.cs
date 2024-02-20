using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    /// <summary>
    /// 物品脚本对应实际对象
    /// </summary>
    [Serializable]
    public struct KitchenObjectSO2GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }

    /// <summary>
    /// 盘子中全部可能物品
    /// </summary>
    [SerializeField] private List<KitchenObjectSO2GameObject> kitchenObjectSO2GameObjects;
    /// <summary>
    /// 盘子脚本
    /// </summary>
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    /// <summary>
    /// 物品脚本和实际对象对应的哈希表，用于查询
    /// </summary>
    private Dictionary<KitchenObjectSO, GameObject> kitchenObjectSO2GameObjectDict;

    private void Awake()
    {
        kitchenObjectSO2GameObjectDict = new Dictionary<KitchenObjectSO, GameObject>();
        foreach (KitchenObjectSO2GameObject kitchenObjectSO2GameObject in kitchenObjectSO2GameObjects)
        {
            // 创建哈希表
            kitchenObjectSO2GameObjectDict[kitchenObjectSO2GameObject.kitchenObjectSO] = kitchenObjectSO2GameObject.gameObject;
        }
    }

    private void Start()
    {
        // 订阅事件，更新显示效果
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }

    /// <summary>
    /// 更新盘子中物品显示效果
    /// </summary>
    /// <param name="sender">事件发布者：盘子</param>
    /// <param name="e">事件参数：放入的物品</param>
    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedArgs e)
    {
        kitchenObjectSO2GameObjectDict[e.kitchenObjectSO].SetActive(true);
    }
}
