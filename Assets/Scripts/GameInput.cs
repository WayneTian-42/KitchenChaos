using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    /// <summary>
    /// 单例模式
    /// </summary>
    public static GameInput Instance { get; private set; }
    /// <summary>
    /// 交互事件1：放下拿起物品
    /// </summary>
    public event EventHandler OnInteractAction;
    /// <summary>
    /// 交互事件2：切菜
    /// </summary>
    public event EventHandler OnInteractAlternateAction;
    /// <summary>
    /// 暂停事件
    /// </summary>
    public event EventHandler OnPauseAction;
    /// <summary>
    /// 输入按键类
    /// </summary>
    private PlayerInputActions playerInputActions;

    // 输入控制
    private void Awake()
    {
        Instance = this;
        // 激活输入
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        // 按下交互键时触发事件，会调用Interact_performed函数
        playerInputActions.Player.Interact.performed += Interact_performed;
        // 按下切菜键时触发事件，会调用InteractAlternate_performed函数
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        // 按下暂停键时触发事件
        playerInputActions.Player.Pause.performed += Pause_performed;
    }

    /// <summary>
    /// 销毁实例时执行，取消事件订阅并释放内存
    /// </summary>
    void OnDestroy()
    {
        // 不取消事件订阅，直接销毁实例似乎也可以
        // 取消事件订阅
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.InteractAlternate.performed -= InteractAlternate_performed;
        playerInputActions.Player.Pause.performed -= Pause_performed;

        // 释放内存
        playerInputActions.Dispose();
    }

    /// <summary>
    /// 暂停事件，按下暂停键后调用的函数
    /// </summary>
    /// <param name="context"></param>
    private void Pause_performed(InputAction.CallbackContext context)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// 交互事件，即玩家按下交互键时调用的函数
    /// </summary>
    /// <param name="context"></param>
    private void Interact_performed(InputAction.CallbackContext context)
    {
        // Debug.Log(callbackContext);
        // 存在该事件的订阅者时执行，即该事件被触发了
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// 按下交互键2
    /// </summary>
    /// <param name="context"></param>
    private void InteractAlternate_performed(InputAction.CallbackContext context)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// 获取归一化的二维输入向量
    /// </summary>
    /// <returns>归一化的二维输入向量</returns>
    public Vector2 GetInputVectorNormalized()
    {
        // 通过playerInputActions获取输入向量
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        // 输入向量归一化
        inputVector = inputVector.normalized;

        return inputVector;
    }
}
