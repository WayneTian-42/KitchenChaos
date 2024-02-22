using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 全局单例
    /// </summary>
    public static GameManager Instance {get; private set; }

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
    /// 游戏状态
    /// </summary>
    private State gameState;

    /// <summary>
    /// 等待计时器，倒计时计时器，游玩计时器
    /// </summary>
    private float waitingToStartTimer = 1f, countdownToStartTimer = 3f, gamePlayingTimer = 10f;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        switch (gameState)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer <= 0f)
                {
                    gameState = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
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
    /// 获取开始倒计时
    /// </summary>
    /// <returns>开始倒计时</returns>
    public float GetCountdownTimer()
    {
        return countdownToStartTimer;
    }
}
