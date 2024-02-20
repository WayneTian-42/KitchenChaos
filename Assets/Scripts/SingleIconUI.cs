using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleIconUI : MonoBehaviour
{
    /// <summary>
    /// 物品UI图片
    /// </summary>
    [SerializeField] Image iconImage;

    /// <summary>
    /// 用物品的sprite设置图标的图片
    /// </summary>
    /// <param name="kitchenObjectSO">物品脚本</param>
    public void SetIconImage(KitchenObjectSO kitchenObjectSO)
    {
        iconImage.sprite = kitchenObjectSO.GetSprite();
    }
}
