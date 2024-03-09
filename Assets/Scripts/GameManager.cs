using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 全局单例
    /// </summary>
    public static GameManager Instance { get; private set; }

    /// <summary>
    /// 游戏状态
    /// </summary>
    public enum State
    {
        /// <summary>
        /// 等待开始（用于多人）
        /// </summary>
        WaitingToStart,
        /// <summary>
        /// 倒计时
        /// </summary>
        CountdownToStart,
        /// <summary>
        /// 游玩状态
        /// </summary>
        GamePlaying,
        /// <summary>
        /// 游戏结束
        /// </summary>
        GameOver
    }

    /// <summary>
    /// 事件：游戏状态发生改变
    /// </summary>
    public event EventHandler OnStateChanged;
    /// <summary>
    /// 事件：暂停游戏
    /// </summary>
    public event EventHandler OnGamePaused;
    /// <summary>
    /// 事件：继续游戏
    /// </summary>
    public event EventHandler OnGameResumed;
    /// <summary>
    /// 游戏状态
    /// </summary>
    private State gameState;
    /// <summary>
    /// 关卡最大游戏时长
    /// </summary>
    private const float MaxGamePlayingTimer = 15f;
    /// <summary>
    /// 倒计时计时器，游玩计时器
    /// </summary>
    private float countdownToStartTimer = 3f, gamePlayingTimer = MaxGamePlayingTimer;

    /// <summary>
    /// 游戏暂停标识
    /// </summary>
    private bool isPauseGame = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    /// <summary>
    /// 准备状态时按下交互键，进入倒计时状态
    /// </summary>
    /// <param name="sender">事件发布者：GameInput</param>
    /// <param name="e">事件参数：空</param>
    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (gameState == State.WaitingToStart)
        {
            gameState = State.CountdownToStart;

            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Update()
    {
        switch (gameState)
        {
            case State.WaitingToStart:
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer <= 0f)
                {
                    gameState = State.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer <= 0f)
                {
                    gameState = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }
    }

    /// <summary>
    /// 暂停事件
    /// </summary>
    /// <param name="sender">事件发布者：游戏输入类</param>
    /// <param name="e">事件参数：空</param>
    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    /// <summary>
    /// 切换游戏状态：暂停或继续
    /// </summary>
    public void TogglePauseGame()
    {
        isPauseGame = !isPauseGame;
        if (isPauseGame)
        {
            Time.timeScale = 0f;

            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;

            OnGameResumed?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// 是否正在游戏状态
    /// </summary>
    /// <returns>
    /// true: 正在游戏
    /// false: 不在游戏
    /// </returns>
    public bool IsGamePlaying()
    {
        return gameState == State.GamePlaying;
    }

    /// <summary>
    /// 是否正在游戏状态
    /// </summary>
    /// <returns>
    /// true: 正在游戏
    /// false: 不在游戏
    /// </returns>
    public bool IsCountdown()
    {
        return gameState == State.CountdownToStart;
    }

    /// <summary>
    /// 游戏是否结束
    /// </summary>
    /// <returns>
    /// true: 结束
    /// false: 未结束
    /// </returns>
    public bool IsGameOver()
    {
        return gameState == State.GameOver;
    }

    /// <summary>
    /// 获取开始倒计时
    /// </summary>
    /// <returns>开始倒计时</returns>
    public float GetCountdownTimer()
    {
        return countdownToStartTimer;
    }

    /// <summary>
    /// 获取游戏剩余时间所占比例
    /// </summary>
    /// <returns>游戏剩余时间所占比例/returns>
    public float GetGamePlayingTimerNormalized()
    {
        return 1 - gamePlayingTimer / MaxGamePlayingTimer;
    }
}
