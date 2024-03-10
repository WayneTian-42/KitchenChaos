using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    /// <summary>
    /// 倒计时UI文本
    /// </summary>
    [SerializeField] private TextMeshProUGUI countdownText;
    /// <summary>
    /// 倒计时动画控件
    /// </summary>
    private Animator animator;
    /// <summary>
    /// 动画触发器
    /// </summary>
    private const string NumberPopup = "NumberPopup";
    /// <summary>
    /// 上一秒的数字，用于更新动画
    /// </summary>
    private int previousNumber = 0;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged; // 订阅游戏状态变化事件
        gameObject.SetActive(false); // 订阅事件后取消激活状态
    }

    private void Update()
    {
        int currentNumber = Mathf.CeilToInt(GameManager.Instance.GetCountdownTimer());
        // 更改文本
        countdownText.text = currentNumber.ToString();
        // 数字不同时，触发动画
        if (currentNumber != previousNumber)
        {
            previousNumber = currentNumber;
            // 触发动画
            animator.SetTrigger(NumberPopup);
            // 播放音效
            SoundManager.Instance.CountDownSound();
        }
    }

    /// <summary>
    /// 根据游戏状态开关UI
    /// </summary>
    /// <param name="sender">事件发布者：GameManager</param>
    /// <param name="e">事件参数：空</param>
    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsCountdown())
        {
            ShowCountdownUI();
        }
        else
        {
            HideCountdownUI();
        }
    }

    /// <summary>
    /// 显示倒计时UI
    /// </summary>
    private void ShowCountdownUI()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 隐藏倒计时UI
    /// </summary>
    private void HideCountdownUI()
    {
        gameObject.SetActive(false);
    }
}
