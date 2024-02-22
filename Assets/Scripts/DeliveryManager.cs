using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    /// <summary>
    /// 单例模式，只有一个传送管理器
    /// </summary>
    public static DeliveryManager Instance;
    /// <summary>
    /// 生成订单事件
    /// </summary>
    public event EventHandler<DeliveryRecipeArgs> OnRecipeSpawned;
    /// <summary>
    /// 完成订单事件
    /// </summary>
    public event EventHandler<DeliveryRecipeArgs> OnRecipeCompleted;
    /// <summary>
    /// 订单事件参数
    /// </summary>
    public class DeliveryRecipeArgs : EventArgs
    {
        /// <summary>
        /// 生成或完成的订单
        /// </summary>
        public RecipeSO recipeSO;

        public DeliveryRecipeArgs(RecipeSO _recipeSO)
        {
            recipeSO = _recipeSO;
        }
    }
    /// <summary>
    /// 订单交付成功，用于播放音效
    /// </summary>
    public event EventHandler OnRecipeSuccess;
    /// <summary>
    /// 订单交付失败，用于播放音效
    /// </summary>
    public event EventHandler OnRecipeFail;
    /// <summary>
    /// 全部菜单
    /// </summary>
    [SerializeField] private RecipeListSO recipeListSO;
    /// <summary>
    /// 顾客当前订单
    /// </summary>
    private Dictionary<RecipeSO, int> waitingRecipeSODict;
    /// <summary>
    /// 全部菜单
    /// </summary>
    private List<RecipeSO> recipeSOList;
    /// <summary>
    /// 订单生成计时器
    /// </summary>
    private float spawnRecipeTimer = 4f;
    /// <summary>
    /// 生成新订单所需时间
    /// </summary>
    private const float MaxSpawnRecipeTimer = 4f;
    /// <summary>
    /// 当前订单数量
    /// </summary>
    private int waitingRecipeNum = 0;
    /// <summary>
    /// 订单最大数量
    /// </summary>
    private const int MaxWaitingRecipeNum = 4;
    /// <summary>
    /// 订单完成数量
    /// </summary>
    private int completedRecipeNum = 0;

    private void Awake()
    {
        Instance = this;
        // 初始化订单字典
        waitingRecipeSODict = new Dictionary<RecipeSO, int>();
        recipeSOList = recipeListSO.recipeSOList;
        foreach (RecipeSO recipeSO in recipeSOList)
        {
            waitingRecipeSODict.Add(recipeSO, 0);
        }
    }

    private void Update()
    {
        spawnRecipeTimer += Time.deltaTime;
        if (spawnRecipeTimer >= MaxSpawnRecipeTimer) // 计时器大于生成订单时间，生成新订单
        {
            spawnRecipeTimer = 0f;
            if (waitingRecipeNum < MaxWaitingRecipeNum) // 订单数量小于最大数量，还可以接单
            {
                ++waitingRecipeNum;
                // 随机生成新订单
                RecipeSO recipeSO = recipeSOList[UnityEngine.Random.Range(0, recipeSOList.Count)];
                ++waitingRecipeSODict[recipeSO];
                // 生成UI
                OnRecipeSpawned?.Invoke(this, new DeliveryRecipeArgs(recipeSO));
            }
        }
    }

    /// <summary>
    /// 交付订单
    /// </summary>
    /// <param name="plateKitchenObject">盘子</param>
    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        // 获取盘子中的物品
        RecipeSO recipeSO = ScriptableObject.CreateInstance<RecipeSO>();
        recipeSO.kitchenObjectSOList = plateKitchenObject.GetKitchenObjectSOList();
        if (waitingRecipeSODict.TryGetValue(recipeSO, out int num) && num > 0) // 订单中存在该物品且数量大于0
        {
            // 从订单中移除该菜谱
            --waitingRecipeNum;
            --waitingRecipeSODict[recipeSO];
            // 完成数+1
            ++completedRecipeNum;
            // 触发事件，更新UI
            OnRecipeCompleted?.Invoke(this, new DeliveryRecipeArgs(recipeSO));
            // 播放音效
            OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            OnRecipeFail?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// 获取完成订单数量
    /// </summary>
    /// <returns>完成的订单数量</returns>
    public int GetCompletedRecipeNum()
    {
        return completedRecipeNum;
    }
}
