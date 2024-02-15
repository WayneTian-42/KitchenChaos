using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    // 角色控制脚本
    [SerializeField] private Player player;

    // 动画组件
    private Animator animator;

    // 动画组件中状态转移参数
    private const string IsWalking = "IsWalking";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // 根据移动状态更新参数
        animator.SetBool(IsWalking, player.IsWalking());
    }
}
