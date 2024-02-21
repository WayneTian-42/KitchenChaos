using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleRecipeUI : MonoBehaviour
{
    /// <summary>
    /// 订单名
    /// </summary>
    [SerializeField] private TextMeshProUGUI recipeTitle;
    /// <summary>
    /// 订单UI显示区域
    /// </summary>
    [SerializeField] private Transform iconContainer;
    /// <summary>
    /// 订单图标模板
    /// </summary>
    [SerializeField] private Transform iconTemplate;

    /// <summary>
    /// 设置订单名称以及图标
    /// </summary>
    /// <param name="recipeSO">菜单名</param>
    public void SetRecipeIcons(RecipeSO recipeSO)
    {
        recipeTitle.text = recipeSO.recipeName; // 更改订单名

        foreach (KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList)
        {
            Transform iconTransfom = Instantiate(iconTemplate, iconContainer); // 生成图标
            iconTransfom.GetComponent<Image>().sprite = kitchenObjectSO.GetSprite(); // 更改图标图片
            iconTransfom.gameObject.SetActive(true); // 显示图标
        }
    }
}
