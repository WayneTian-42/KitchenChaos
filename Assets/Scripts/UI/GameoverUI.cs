using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameoverUI : MonoBehaviour
{
    /// <summary>
    /// 倒计时UI文本
    /// </summary>
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged; // 订阅游戏状态变化事件
        gameObject.SetActive(false); // 订阅事件后取消激活状态
    }

    /// <summary>
    /// 根据游戏状态开关UI
    /// </summary>
    /// <param name="sender">事件发布者：GameManager</param>
    /// <param name="e">事件参数：空</param>
    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            ShowGameoverUI();
            recipesDeliveredText.text = DeliveryManager.Instance.GetCompletedRecipeNum().ToString();
        }
        else
        {
            HideGameoverUI();
        }
    }

    /// <summary>
    /// 显示倒计时UI
    /// </summary>
    private void ShowGameoverUI()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 隐藏倒计时UI
    /// </summary>
    private void HideGameoverUI()
    {
        gameObject.SetActive(false);
    }
}
