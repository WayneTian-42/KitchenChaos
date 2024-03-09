using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    /// <summary>
    /// 生成盘子事件
    /// </summary>
    public event EventHandler OnPlateSpawned;
    /// <summary>
    /// 移除盘子事件
    /// </summary>
    public event EventHandler OnPlateRemoved;
    [SerializeField] private KitchenObjectSO plateSO;
    /// <summary>
    /// 生成盘子计时器
    /// </summary>
    private float spawningPlateTimer;
    /// <summary>
    /// 生成一个盘子所需时间
    /// </summary>
    private const float MaxSpawningPlateTime = 4f;
    /// <summary>
    /// 柜台上盘子数量
    /// </summary>
    private int spawningPlateAccount;
    /// <summary>
    /// 柜台盘子最大数量
    /// </summary>
    private const int MaxSpawningPlateAccount = 4;

    private void Update()
    {
        if (!GameManager.Instance.IsGamePlaying())
        {
            return;
        }
        if (spawningPlateAccount >= MaxSpawningPlateAccount) // 盘子数量达到最大
        {
            return;
        }
        spawningPlateTimer += Time.deltaTime;
        if (spawningPlateTimer >= MaxSpawningPlateTime) // 可以生成盘子
        {
            // 计时器归零
            spawningPlateTimer = 0;
            ++spawningPlateAccount;
            // 委托事件
            OnPlateSpawned?.Invoke(this, EventArgs.Empty);
        }
    }

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject()) // 角色手中有物品不能拿盘子
            return;
        if (spawningPlateAccount > 0) // 柜台上有盘子
        {
            --spawningPlateAccount;
            // 直接在玩家手中生成一个盘子
            KitchenObject.SpawnKitchenObject(plateSO, player);
            // 减少盘子动画
            OnPlateRemoved?.Invoke(this, EventArgs.Empty);
        }
    }
}
