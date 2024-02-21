using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    /// <summary>
    /// 单例
    /// </summary>
    public static SoundManager Instance { get; private set; }
    /// <summary>
    /// 音效片段
    /// </summary>
    [SerializeField] private AudioClipRefSO audioClipRefSO;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // 订阅事件
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFail += DeliveryManager_OnRecipeFail;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickUpSomething += Player_OnPickUpSomething;
        BaseCounter.OnAnyObjectPlaced += BaseCounter_OnAnyObjectPlaced;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    /// <summary>
    /// 播放角色脚步音效
    /// </summary>
    /// <param name="position">角色位置</param>
    /// <param name="volume">音效音量</param>
    public void PlayerFootstepSound(Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipRefSO.footstep, position, volume);
    }

    /// <summary>
    /// 播放订单交付成功音效
    /// </summary>
    /// <param name="sender">事件发布者：交付管理类</param>
    /// <param name="e">事件参数：空</param>
    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefSO.deliverySuccess, DeliveryCounter.Instance.transform.position);
    }

    /// <summary>
    /// 播放订单交付失败音效
    /// </summary>
    /// <param name="sender">事件发布者：交付管理类</param>
    /// <param name="e">事件参数：空</param>
    private void DeliveryManager_OnRecipeFail(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefSO.deliveryFail, DeliveryCounter.Instance.transform.position);
    }

    /// <summary>
    /// 播放切菜音效
    /// </summary>
    /// <param name="sender">事件发布者：切菜台</param>
    /// <param name="e">事件参数：空</param>
    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefSO.chop, cuttingCounter.transform.position);
    }

    /// <summary>
    /// 播放角色放下物品音效
    /// </summary>
    /// <param name="sender">事件发布者：角色</param>
    /// <param name="e">事件参数：空</param>
    private void Player_OnPickUpSomething(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefSO.objectPickup, Player.Instance.transform.position);
    }

    /// <summary>
    /// 播放物品放在柜台音效
    /// </summary>
    /// <param name="sender">事件发布者：柜台</param>
    /// <param name="e">事件参数：空</param>
    private void BaseCounter_OnAnyObjectPlaced(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefSO.objectDrop, baseCounter.transform.position);
    }

    /// <summary>
    /// 播放扔垃圾音效
    /// </summary>
    /// <param name="sender">事件发布者：垃圾桶</param>
    /// <param name="e">事件参数：空</param>
    private void TrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipRefSO.trash, trashCounter.transform.position);
    }

    /// <summary>
    /// 在指定位置以指定音量从音效数组中随机播放一个
    /// </summary>
    /// <param name="audioClipArray">音效数组</param>
    /// <param name="position">播放音效位置</param>
    /// <param name="volume">音效音量</param>
    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }

    /// <summary>
    /// 在指定位置以指定音量播放音效
    /// </summary>
    /// <param name="audioClipArray">音效</param>
    /// <param name="position">播放音效位置</param>
    /// <param name="volume">音效音量</param>
    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
}
