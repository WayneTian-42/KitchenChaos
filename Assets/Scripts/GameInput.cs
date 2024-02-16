using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    // InputSystem类
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        // 激活PlayerInputActions
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }
    /// <summary>
    /// 获取归一化的输入向量
    /// </summary>
    /// <returns>归一化的输入向量</returns>
    public Vector2 GetInputVectorNormalized()
    {
        // 通过playerInputActions获取输入向量
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        // 输入向量归一化
        inputVector = inputVector.normalized;

        return inputVector;
    }
}
