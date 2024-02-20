using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IKitchenObjectParent, IHasProgressBar
{
    /// <summary>
    /// 状态机表示当前食物状态
    /// </summary>
    public enum State
    {
        Idle,
        Frying, // 煎炸中
        Fried, // 煎炸完毕
        Burned // 烧焦了
    }
    /// <summary>
    /// 状态发生改变的事件，用于控制视觉效果显示
    /// </summary>
    public event EventHandler<OnStateChangeArgs> OnStateChanged;
    public class OnStateChangeArgs : EventArgs
    {
        public State state;

        public OnStateChangeArgs(State _state)
        {
            state = _state;
        }

    }
    /// <summary>
    /// 事件：煎炸进度发生变化
    /// </summary>
    public event EventHandler<IHasProgressBar.OnProgressChangedEvnetArgs> OnProgressChanged;
    /// <summary>
    /// 菜单，可以查询物品被煎炸后的状态
    /// </summary>
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    /// <summary>
    /// 当前物品对应的煎炸菜谱
    /// </summary>
    private FryingRecipeSO fryingRecipeSO;
    /// <summary>
    /// 哈希表，存储物品以及对应的菜谱
    /// </summary>
    private Hashtable fryingRecipeSOHT;
    /// <summary>
    /// 当前煎炸时间
    /// </summary>
    private float fryingTimer;
    /// <summary>
    /// 食物当前状态
    /// </summary>
    private State objectState;

    private void Awake()
    {
        // 根据煎炸菜谱初始化哈希表
        fryingRecipeSOHT = new Hashtable();
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            fryingRecipeSOHT.Add(fryingRecipeSO.GetKitchenObjectSO(), fryingRecipeSO);
        }
    }

    private void LateUpdate()
    {
        if (HasKitchenObject())
        {
            switch (objectState)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;
                    // 更新进度条
                    OnProgressChanged?.Invoke(this, new IHasProgressBar.OnProgressChangedEvnetArgs(fryingTimer / fryingRecipeSO.GetMaxFryingTime()));
                    if (fryingTimer >= fryingRecipeSO.GetMaxFryingTime()) // 煎炸完成后
                    {
                        // 销毁原物品
                        GetKitchenObject().DestroySelf();
                        // 生成煎炸后的物品
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.GetKitchenObjectSlicesSO(), this);
                        // KitchenObject.SpawnKitchenObject((KitchenObjectSO)cuttingRecipeSOHT[GetKitchenObject().GetKitchenObjectSO()], this);
                        // 物品煎炸一次后更新菜谱
                        fryingRecipeSO = (FryingRecipeSO)fryingRecipeSOHT[GetKitchenObject().GetKitchenObjectSO()];
                        // 清零计时器
                        fryingTimer = 0;
                        // 更新状态
                        if (fryingRecipeSO != null)
                        {
                            objectState = State.Fried;
                            // 更改显示效果
                            OnStateChanged?.Invoke(this, new OnStateChangeArgs(objectState));
                        }
                        else
                        {
                            objectState = State.Burned;
                            // 更改显示效果
                            OnStateChanged?.Invoke(this, new OnStateChangeArgs(objectState));
                        }
                    }
                    break;
                case State.Fried:
                    fryingTimer += Time.deltaTime;
                    // 更新进度条
                    OnProgressChanged?.Invoke(this, new IHasProgressBar.OnProgressChangedEvnetArgs(fryingTimer / fryingRecipeSO.GetMaxFryingTime()));
                    if (fryingTimer >= fryingRecipeSO.GetMaxFryingTime()) // 煎炸完成后
                    {
                        // 销毁原物品
                        GetKitchenObject().DestroySelf();
                        // 生成煎炸后的物品
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.GetKitchenObjectSlicesSO(), this);
                        // 更新状态
                        objectState = State.Burned;
                        // 更改显示效果
                        OnStateChanged?.Invoke(this, new OnStateChangeArgs(objectState));
                    }
                    break;
                case State.Burned:
                    break;
            }
        }
    }


    /// <summary>
    /// 交互函数，角色可以将材料放置在炉灶台上，或者从灶台上拿取物品
    /// </summary>
    public override void Interact(Player player)
    {
        if (HasKitchenObject() == false) // 柜台为空时
        {
            // 角色手中有物品
            if (player.HasKitchenObject() == true)
            {
                // // 查询角色手中物品是否能够被煎炸
                fryingRecipeSO = (FryingRecipeSO)fryingRecipeSOHT[player.GetKitchenObject().GetKitchenObjectSO()];
                // 物品能被煎炸时才可以放下
                if (fryingRecipeSO != null)
                {
                    // 更新物品父对象为柜台
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    // 重置煎炸时间
                    fryingTimer = 0;
                    // 更新进度条
                    OnProgressChanged?.Invoke(this, new IHasProgressBar.OnProgressChangedEvnetArgs());
                    // 更新状态
                    objectState = State.Frying;
                    // 更改显示效果
                    OnStateChanged?.Invoke(this, new OnStateChangeArgs(objectState));
                }
            }
        }
        else // 柜台不为空时
        {
            if (player.HasKitchenObject() == false) // 角色手中没有物品
            {
                GetKitchenObject().SetKitchenObjectParent(player);
                StopFrying();
            }
            else if (player.GetKitchenObject().TryGetPlateKitchenObject(out PlateKitchenObject plateKitchenObject)) // 获取盘子
            {
                if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) // 尝试将物品放在盘子上
                {
                    GetKitchenObject().DestroySelf(); // 放置成功后销毁原物品
                    StopFrying();
                }
            }
        }
    }

    /// <summary>
    /// 停止煎炸
    /// </summary>
    private void StopFrying()
    {
        fryingTimer = 0;
        // 更新进度条
        OnProgressChanged?.Invoke(this, new IHasProgressBar.OnProgressChangedEvnetArgs());
        // 更新状态
        objectState = State.Idle;
        // 更改显示效果
        OnStateChanged?.Invoke(this, new OnStateChangeArgs(objectState));
    }
}