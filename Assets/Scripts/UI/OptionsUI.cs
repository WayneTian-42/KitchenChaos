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
    }

    private void Start()
    {
        // 更新UI
        UpdateSoundEffectsButtonVisual();
        UpdateMusicButtonVisual();
        Hide();

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
}
