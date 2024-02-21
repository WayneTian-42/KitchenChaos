using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
    /// <summary>
    /// 盘子
    /// </summary>
    [SerializeField] PlateKitchenObject plateKitchenObject;
    /// <summary>
    /// icon模板
    /// </summary>
    [SerializeField] Transform iconTemplate;

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }

    /// <summary>
    /// 更新盘子UI的Icon
    /// </summary>
    /// <param name="sender">事件发布者：盘子</param>
    /// <param name="e">事件参数：物品对象</param>
    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedArgs e)
    {
        UpdateVisual(e.kitchenObjectSO);
    }

    /// <summary>
    /// 更新盘子上方UI中物品图标
    /// </summary>
    /// <param name="kitchenObjectSO"></param>
    private void UpdateVisual(KitchenObjectSO kitchenObjectSO)
    {
        Transform iconTransform = Instantiate(iconTemplate, transform); // 创建新的Icon
        iconTransform.GetComponent<SingleIconUI>().SetIconImage(kitchenObjectSO); // 设置Icon的图片
        iconTransform.gameObject.SetActive(true); // 显示Icon
    }
}
