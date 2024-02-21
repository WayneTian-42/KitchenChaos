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
                RecipeSO recipeSO = recipeSOList[Random.Range(0, recipeSOList.Count)];
                ++waitingRecipeSODict[recipeSO];
                Debug.Log(recipeSO.recipeName);
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
        if (waitingRecipeSODict.TryGetValue(recipeSO, out int num) && num > 0)
        {
            --waitingRecipeNum;
            --waitingRecipeSODict[recipeSO];
            Debug.Log("Player delivered the correct recipe!");
        }
        else
        {
            Debug.Log("Player did not deliver a correct recipe!");
        }
    }

}
