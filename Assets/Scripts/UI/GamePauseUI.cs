using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    /// <summary>
    /// 继续按钮
    /// </summary>
    [SerializeField] private Button resumeButton;
    /// <summary>
    /// 选项按钮
    /// </summary>
    [SerializeField] private Button optionsButton;
    /// <summary>
    /// 主菜单按钮
    /// </summary>
    [SerializeField] private Button mainMenuButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() => 
        {
            GameManager.Instance.TogglePauseGame();
        });
        optionsButton.onClick.AddListener(() => {
            OptionsUI.Instance.Show();
        });
        mainMenuButton.onClick.AddListener(() => 
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameResumed += GameManager_OnGameResumed;

        Hide();
    }

    /// <summary>
    /// 游戏暂停事件
    /// </summary>
    /// <param name="sender">事件发布者：GameManager</param>
    /// <param name="e">事件参数：空</param>
    private void GameManager_OnGamePaused(object sender, EventArgs e)
    {
        Show();
    }

    /// <summary>
    /// 游戏继续事件
    /// </summary>
    /// <param name="sender">事件发布者：GameManager</param>
    /// <param name="e">事件参数：空</param>
    private void GameManager_OnGameResumed(object sender, EventArgs e)
    {
        Hide();
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
