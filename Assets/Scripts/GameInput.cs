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
    /// 操作
    /// </summary>
    public enum Binding
    {
        /// <summary>
        /// 向上移动
        /// </summary>
        MoveUp,
        /// <summary>
        /// 向下移动
        /// </summary>
        MoveDown,
        /// <summary>
        /// 向左移动
        /// </summary>
        MoveLeft,
        /// <summary>
        /// 向右移动
        /// </summary>
        MoveRight,
        /// <summary>
        /// 放下/拿起物品
        /// </summary>
        Interact,
        /// <summary>
        /// 切菜
        /// </summary>
        InteractAlternate,
        /// <summary>
        /// 暂停
        /// </summary>
        Pause
    };
    /// <summary>
    /// 输入按键类
    /// </summary>
    private PlayerInputActions playerInputActions;
    /// <summary>
    /// 输入绑定，用于持久化存储
    /// </summary>
    private const string PlayerPrefsBindings = "InputBindings";

    // 输入控制
    private void Awake()
    {
        Instance = this;
        // 激活输入
        playerInputActions = new PlayerInputActions();
        // 读取按键
        if (PlayerPrefs.HasKey(PlayerPrefsBindings))
        {
            playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PlayerPrefsBindings));
        }
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
    private void OnDestroy()
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

    /// <summary>
    /// 根据操作以字符串形式返回绑定的按键
    /// </summary>
    /// <param name="binding">操作类型</param>
    /// <returns>绑定的按键</returns>
    public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            case Binding.MoveUp:
                return playerInputActions.Player.Move.bindings[1].ToDisplayString();
            case Binding.MoveDown:
                return playerInputActions.Player.Move.bindings[2].ToDisplayString();
            case Binding.MoveLeft:
                return playerInputActions.Player.Move.bindings[3].ToDisplayString();
            case Binding.MoveRight:
                return playerInputActions.Player.Move.bindings[4].ToDisplayString();
            case Binding.Interact:
                return playerInputActions.Player.Interact.bindings[0].ToDisplayString();
            case Binding.InteractAlternate:
                return playerInputActions.Player.InteractAlternate.bindings[0].ToDisplayString();
            case Binding.Pause:
                return playerInputActions.Player.Pause.bindings[0].ToDisplayString();
            default:
                return "";
        }
    }

    /// <summary>
    /// 重新绑定按键
    /// </summary>
    /// <param name="binding">要绑定的操作</param>
    /// <param name="onActionRebind">绑定完成后执行的操作</param>
    public void Rebinding(Binding binding, Action onActionRebind)
    {
        // 操作
        InputAction inputAction;
        // 绑定数组的序号
        int bindingIndex = 0;
        switch (binding)
        {
            case Binding.MoveUp:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 1;
                break;
            case Binding.MoveDown:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 2;
                break;
            case Binding.MoveLeft:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 3;
                break;
            case Binding.MoveRight:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 3;
                break;
            case Binding.Interact:
                inputAction = playerInputActions.Player.Interact;
                break;
            case Binding.InteractAlternate:
                inputAction = playerInputActions.Player.InteractAlternate;
                break;
            case Binding.Pause:
                inputAction = playerInputActions.Player.Pause;
                break;
            default:
                inputAction = null;
                break;
        }
        playerInputActions.Player.Disable(); // 禁用按键
        inputAction.PerformInteractiveRebinding(bindingIndex).OnComplete(callback =>
        {
            // Debug.Log(callback.action.bindings[1].path); // 输出操作原本绑定的按键
            // Debug.Log(callback.action.bindings[1].overridePath); // 输出操作由玩家重新绑定后的按键
            callback.Dispose(); // 释放内存
            playerInputActions.Player.Enable(); // 启用按键
            onActionRebind();
            PlayerPrefs.SetString(PlayerPrefsBindings, playerInputActions.SaveBindingOverridesAsJson()); // 持久化存储
        }).Start();
    }
}
