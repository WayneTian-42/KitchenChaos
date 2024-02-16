using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.Video;

public class Player : MonoBehaviour
{
    // 角色移动速度
    [SerializeField] private float moveSpeed = 7f;
    // 输入处理
    [SerializeField] private GameInput gameInput;
    // 计数器类的图层
    [SerializeField] private LayerMask counterLayerMask;
    // 角色是否在移动
    private bool isWalking;

    // 记录上次交互方向
    private Vector3 lastInteractDir;

    private void Start()
    {
        // OnInteractAction触发时，会执行GameInput_OnInteractAction函数
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    /// <summary>
    /// 交互函数，当按下交互键时会执行
    /// </summary>
    /// <param name="sender">发布事件者</param>
    /// <param name="eventArgs">事件参数</param>
    private void GameInput_OnInteractAction(object sender, System.EventArgs eventArgs)
    {
        Vector2 inputVector = gameInput.GetInputVectorNormalized();

        // 移动方向
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        // 记录交互方向
        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        float interactDis = 2f;
        // 碰撞检测，添加图层
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDis, counterLayerMask))
        {
            // 尝试获取ClearCounter
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                clearCounter.Interact();
            }
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteraction();
    }

    /// <summary>
    /// 角色是否在移动
    /// </summary>
    /// <returns>true表示角色在移动，false表示不在移动</returns>
    public bool IsWalking()
    {
        return isWalking;
    }


    /// <summary>
    /// 处理角色移动
    /// </summary>
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetInputVectorNormalized();

        // 移动方向
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        // 面朝方向
        Vector3 faceDir = moveDir;

        float moveDistance = moveSpeed * Time.deltaTime;
        float palyerHeight = 2f;
        float playerRadius = 0.7f;
        // 发射胶囊射线，判断前方是否存在碰撞体
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * palyerHeight, playerRadius, moveDir, moveDistance);

        // 不能沿当前方向直线移动时，尝试分解移动
        if (!canMove)
        {
            // 尝试只在x轴移动
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized; // 归一化使得移动速度相同，但是个人觉得不归一化也可以
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * palyerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove) // 当x轴分量为0时，canMove也会为true
            {
                moveDir = moveDirX;
                // Debug.Log("X");
            }
            else
            {
                // 尝试在z轴移动
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized; // 归一化使得移动速度相同，但是个人觉得不归一化也可以
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * palyerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove) // 当z轴分量为0时，canMove也会为true
                {
                    moveDir = moveDirZ;
                    // Debug.Log("Z");
                }
            }

        }

        // 没有物体时，才可以移动
        if (canMove)
        {
            // 注意运算顺序，先进行标量乘法，再进行矢量乘法
            transform.position += moveDistance * moveDir;
        }

        // 移动方向不为0时说明在移动
        isWalking = (moveDir != Vector3.zero);

        float rotationSpeed = 10f;
        // 当面朝方向不是全零时采用插值平滑改变角色面朝方向
        if (faceDir != Vector3.zero)
        {
            transform.forward = Vector3.Slerp(transform.forward, faceDir, rotationSpeed * Time.deltaTime);
        }
        // Debug.Log(Time.deltaTime);
    }

    /// <summary>
    /// 交互处理
    /// </summary>
    private void HandleInteraction()
    {
        Vector2 inputVector = gameInput.GetInputVectorNormalized();

        // 移动方向
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        // 记录交互方向
        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        float interactDis = 2f;
        // 碰撞检测，添加图层
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDis, counterLayerMask))
        {
            // 尝试获取ClearCounter
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                // clearCounter.Interact();
            }
        }
    }
}
