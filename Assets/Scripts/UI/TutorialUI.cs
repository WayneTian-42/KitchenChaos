using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    /// <summary>
    /// 向上移动按键文本
    /// </summary>
    [SerializeField] private TextMeshProUGUI MoveUpText;
    /// <summary>
    /// 向下移动按键文本
    /// </summary>
    [SerializeField] private TextMeshProUGUI MoveDownText;
    /// <summary>
    /// 向左移动按键文本
    /// </summary>
    [SerializeField] private TextMeshProUGUI MoveLeftText;
    /// <summary>
    /// 向右移动按键文本
    /// </summary>
    [SerializeField] private TextMeshProUGUI MoveRightText;
    /// <summary>
    /// 拿取/放下按键文本
    /// </summary>
    [SerializeField] private TextMeshProUGUI InteractText;
    /// <summary>
    /// 切菜按键文本
    /// </summary>
    [SerializeField] private TextMeshProUGUI InteractAlternateText;
    /// <summary>
    /// 暂停按键文本
    /// </summary>
    [SerializeField] private TextMeshProUGUI PauseText;
    /// <summary>
    /// 手柄移动按键文本
    /// </summary>
    [SerializeField] private TextMeshProUGUI GamepadMoveText;
    /// <summary>
    /// 手柄拿取/放下按键文本
    /// </summary>
    [SerializeField] private TextMeshProUGUI GamepadInteractText;
    /// <summary>
    /// 手柄切菜按键文本
    /// </summary>
    [SerializeField] private TextMeshProUGUI GamepadInteractAlternateText;
    /// <summary>
    /// 手柄暂停按键文本
    /// </summary>
    [SerializeField] private TextMeshProUGUI GamepadPauseText;

    private void Start()
    {
        GameInput.Instance.OnBindingRebind += GameInput_OnBindingRebind;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        UpdateVisual();
        Show();
    }

    /// <summary>
    /// 游戏状态切换到倒计时状态时，隐藏UI
    /// </summary>
    /// <param name="sender">事件发布者：GameManager</param>
    /// <param name="e">事件参数：空</param>
    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsCountdown())
        {
            Hide();
        }
    }

    /// <summary>
    /// 重新绑定按键后更新教程中按键
    /// </summary>
    /// <param name="sender">事件发布者：GameInput</param>
    /// <param name="e">事件参数：空</param>
    private void GameInput_OnBindingRebind(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    /// <summary>
    /// 更新按键文本
    /// </summary>
    private void UpdateVisual()
    {
        MoveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveUp);
        MoveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveDown);
        MoveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveLeft);
        MoveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveRight);
        InteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        InteractAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        PauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        // MoveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Game);
        GamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadInteract);
        GamepadInteractAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadInteractAlternate);
        GamepadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadPause);
    }

    /// <summary>
    /// 展示UI
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
