using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    /// <summary>
    /// 开始按钮
    /// </summary>
    [SerializeField] private Button playButton;
    /// <summary>
    /// 退出按钮
    /// </summary>
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        playButton.onClick.AddListener(() => 
        { 
            Loader.Load(Loader.Scene.GameScene);
        });
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        // 重置时间速度
        Time.timeScale = 1f;
    }
}
