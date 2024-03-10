using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
    /// <summary>
    /// 背景图片
    /// </summary>
    [SerializeField] private Image backgroundImage;
    /// <summary>
    /// 结算图标
    /// </summary>
    [SerializeField] private Image iconImage;
    /// <summary>
    /// 结算文本
    /// </summary>
    [SerializeField] private TextMeshProUGUI messageText;
    /// <summary>
    /// 交付成功时颜色
    /// </summary>
    [SerializeField] private Color successColor;
    /// <summary>
    /// 交付失败时颜色
    /// </summary>
    [SerializeField] private Color failColor;
    /// <summary>
    /// 交付成功时图片
    /// </summary>
    [SerializeField] private Sprite successSprite;
    /// <summary>
    /// 交付失败时图片
    /// </summary>
    [SerializeField] private Sprite failSprite;
    /// <summary>
    /// 动画触发器名称
    /// </summary>
    private const string PopUp = "PopUp";
    /// <summary>
    /// 动画控件
    /// </summary>
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFail += DeliveryManager_OnRecipeFail;

        Hide();
    }

    /// <summary>
    /// 交付成功事件
    /// </summary>
    /// <param name="sender">事件发布者: DeliveryManager</param>
    /// <param name="e">事件参数: 空</param>
    private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e)
    {
        // 展示UI
        Show();
        // 展示动画
        animator.SetTrigger(PopUp);
        // 设置UI图片以及颜色
        backgroundImage.color = successColor;
        iconImage.sprite = successSprite;
        messageText.text = "DELIVERY\nSUCCESS";
    }

    /// <summary>
    /// 交付失败事件
    /// </summary>
    /// <param name="sender">事件发布者: DeliveryManager</param>
    /// <param name="e">事件参数: 空</param>
    private void DeliveryManager_OnRecipeFail(object sender, EventArgs e)
    {
        // 展示UI
        Show();
        // 展示动画
        animator.SetTrigger(PopUp);
        // 设置UI图片以及颜色
        backgroundImage.color = failColor;
        iconImage.sprite = failSprite;
        messageText.text = "DELIVERY\nFAILED";
    }

    /// <summary>
    /// 显示UI
    /// </summary>
    private void Show()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 隐藏UI
    /// </summary>
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
