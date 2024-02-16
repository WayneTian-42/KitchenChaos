using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    // 事件处理
    public event EventHandler OnInteractAction;
    // InputSystem类
    private PlayerInputActions playerInputActions;

    // 输入控制
    private void Awake()
    {
        // 激活输入
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        // 按下交互键时触发事件，会调用Interact_performed函数
        playerInputActions.Player.Interact.performed += Interact_performed;
    }

    /// <summary>
    /// 交互事件，即玩家按下交互键时调用的函数
    /// </summary>
    /// <param name="callbackContext"></param>
    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext callbackContext)
    {
        // Debug.Log(callbackContext);
        // 存在该事件的订阅者时执行，即该事件被触发了
        OnInteractAction?.Invoke(this, EventArgs.Empty);
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
