using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 角色移动速度
    [SerializeField] private float moveSpeed = 7f;

    // 输入处理
    [SerializeField] private GameInput gameInput;

    // 角色是否在移动
    private bool isWalking;

    private void Update()
    {
        Vector2 inputVector = gameInput.GetInputVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        // 注意运算顺序，先进行标量乘法，再进行矢量乘法
        transform.position += moveSpeed * Time.deltaTime * moveDir;
        
        // 移动方向不为0时说明在移动
        isWalking = (moveDir != Vector3.zero);

        float rotationSpeed = 10f;
        // 采用插值平滑改变角色面朝方向
        if (isWalking)
        {
            transform.forward = Vector3.Slerp(transform.forward, moveDir, rotationSpeed * Time.deltaTime);
        }
        // Debug.Log(Time.deltaTime);
    }

    /// <summary>
    /// 角色是否在移动
    /// </summary>
    /// <returns>true表示角色在移动，false表示不在移动</returns>
    public bool IsWalking()
    {
        return isWalking;
    }
}
