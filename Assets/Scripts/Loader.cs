using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    /// <summary>
    /// 场景
    /// </summary>
    public enum Scene
    {
        /// <summary>
        /// 主菜单
        /// </summary>
        MainMenuScene,
        /// <summary>
        /// 加载界面
        /// </summary>
        LoadingScene,
        /// <summary>
        /// 实际游戏场景
        /// </summary>
        GameScene
    }

    /// <summary>
    /// 最终场景：加载界面在加载的场景
    /// </summary>
    private static Scene finalScene;

    /// <summary>
    /// 加载游戏场景
    /// </summary>
    /// <param name="targetScene">游戏场景（想要加载的实际场景）</param>
    public static void Load(Scene targetScene)
    {
        finalScene = targetScene;
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    /// <summary>
    /// 回调函数，在加载界面调用，加载实际场景
    /// </summary>
    public static void LoaderCallback()
    {
        SceneManager.LoadScene(finalScene.ToString());
    }
}
