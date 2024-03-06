using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    /// <summary>
    /// 单例模式
    /// </summary>
    public static OptionsUI Instance { get; private set; }
    /// <summary>
    /// 音效音量按钮
    /// </summary>
    [SerializeField] private Button soundEffectsButton;
    /// <summary>
    /// 音乐音量按钮
    /// </summary>
    [SerializeField] private Button musicButton;
    /// <summary>
    /// 关闭按钮
    /// </summary>
    [SerializeField] private Button closeButton;
    /// <summary>
    /// 音效按钮文本
    /// </summary>
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    /// <summary>
    /// 音乐按钮文本
    /// </summary>
    [SerializeField] private TextMeshProUGUI musicText;
    // 修改按键
    /// <summary>
    /// 向上移动按钮
    /// </summary>
    [SerializeField] private Button moveUpButton;
    /// <summary>
    /// 向上移动按钮文本
    /// </summary>
    [SerializeField] private TextMeshProUGUI moveUpText;
    /// <summary>
    /// 向上移动按钮
    /// </summary>
    [SerializeField] private Button moveDownButton;
    /// <summary>
    /// 向上移动按钮文本
    /// </summary>
    [SerializeField] private TextMeshProUGUI moveDownText;
    /// <summary>
    /// 向上移动按钮
    /// </summary>
    [SerializeField] private Button moveLeftButton;
    /// <summary>
    /// 向上移动按钮文本
    /// </summary>
    [SerializeField] private TextMeshProUGUI moveLeftText;
    /// <summary>
    /// 向上移动按钮
    /// </summary>
    [SerializeField] private Button moveRightButton;
    /// <summary>
    /// 向上移动按钮文本
    /// </summary>
    [SerializeField] private TextMeshProUGUI moveRightText;
    /// <summary>
    /// 向上移动按钮
    /// </summary>
    [SerializeField] private Button interactButton;
    /// <summary>
    /// 向上移动按钮文本
    /// </summary>
    [SerializeField] private TextMeshProUGUI interactText;
    /// <summary>
    /// 向上移动按钮
    /// </summary>
    [SerializeField] private Button interactAlternateButton;
    /// <summary>
    /// 向上移动按钮文本
    /// </summary>
    [SerializeField] private TextMeshProUGUI interactAlternateText;
    /// <summary>
    /// 向上移动按钮
    /// </summary>
    [SerializeField] private Button pauseButton;
    /// <summary>
    /// 向上移动按钮文本
    /// </summary>
    [SerializeField] private TextMeshProUGUI pauseText;
    /// <summary>
    /// 绑定按键UI
    /// </summary>
    [SerializeField] private Transform pressToRebindKeyUI;

    private void Awake()
    {
        Instance = this;

        // 添加按键绑定
        soundEffectsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.AddVolume();
            UpdateSoundEffectsButtonVisual();
        });
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.AddVolume();
            UpdateMusicButtonVisual();
        });
        closeButton.onClick.AddListener(() =>
        {
            Hide();
        });

        // 重新绑定操作
        moveUpButton.onClick.AddListener(() =>
        {
            RebindAction(GameInput.Binding.MoveUp);
        });
        moveDownButton.onClick.AddListener(() =>
        {
            RebindAction(GameInput.Binding.MoveDown);
        });
        moveLeftButton.onClick.AddListener(() =>
        {
            RebindAction(GameInput.Binding.MoveLeft);
        });
        moveRightButton.onClick.AddListener(() =>
        {
            RebindAction(GameInput.Binding.MoveRight);
        });
        interactButton.onClick.AddListener(() =>
        {
            RebindAction(GameInput.Binding.Interact);
        });
        interactAlternateButton.onClick.AddListener(() =>
        {
            RebindAction(GameInput.Binding.InteractAlternate);
        });
        pauseButton.onClick.AddListener(() =>
        {
            RebindAction(GameInput.Binding.Pause);
        });
    }

    private void Start()
    {
        // 更新UI
        UpdateSoundEffectsButtonVisual();
        UpdateMusicButtonVisual();
        UpdateBindingVisual();
        Hide();
        HidePressToRebindKey();

        GameManager.Instance.OnGameResumed += GameManager_OnGameResumed;
    }

    /// <summary>
    /// 展示UI
    /// </summary>
    public void Show()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 关闭UI
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 游戏恢复事件
    /// </summary>
    /// <param name="sender">事件发布者：GameManager</param>
    /// <param name="e">事件参数：空</param>
    private void GameManager_OnGameResumed(object sender, EventArgs e)
    {
        Hide();
    }

    /// <summary>
    /// 更新音效按钮视觉效果
    /// </summary>
    private void UpdateSoundEffectsButtonVisual()
    {
        string text = "Sound Effects: ";
        soundEffectsText.text = text + Mathf.Round(SoundManager.Instance.GetVolume() * 10);
    }

    /// <summary>
    /// 更新音乐按钮视觉效果
    /// </summary>
    private void UpdateMusicButtonVisual()
    {
        string text = "Music: ";
        musicText.text = text + Mathf.Round(MusicManager.Instance.GetVolume() * 10);
    }

    /// <summary>
    /// 更新操作对应的显示文本为实际按键
    /// </summary>
    private void UpdateBindingVisual()
    {
        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveUp);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveDown);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveLeft);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveRight);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
    }

    /// <summary>
    /// 绑定按键
    /// </summary>
    /// <param name="binding">要修改的操作</param>
    private void RebindAction(GameInput.Binding binding)
    {
        ShowPressToRebindKey();
        GameInput.Instance.Rebinding(binding, () =>
        {
            HidePressToRebindKey();
            UpdateBindingVisual();
        });
    }

    /// <summary>
    /// 显示绑定按键UI
    /// </summary>
    private void ShowPressToRebindKey()
    {
        pressToRebindKeyUI.gameObject.SetActive(true);
    }

    /// <summary>
    /// 隐藏绑定按键UI
    /// </summary>
    private void HidePressToRebindKey()
    {
        pressToRebindKeyUI.gameObject.SetActive(false);
    }
}
