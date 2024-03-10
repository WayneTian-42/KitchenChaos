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
    /// <summary>
    /// 音量大小
    /// </summary>
    private float volume = 1f;
    /// <summary>
    /// 音效音量字符串常量
    /// </summary>
    private const string PlayerPrefsSoundEffectsVolume = "SoundEffectsVolume";

    private void Awake()
    {
        Instance = this;

        // 获取存储的音量大小
        volume = PlayerPrefs.GetFloat(PlayerPrefsSoundEffectsVolume, volume);
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
    /// 循环增加音量，保证音量始终在[0, 1]之间
    /// </summary>
    public void AddVolume()
    {
        volume += 0.1f;
        if (volume > 1.09f)
        {
            volume = 0f;
        }

        // 存储音乐音量
        PlayerPrefs.SetFloat(PlayerPrefsSoundEffectsVolume, volume);
        PlayerPrefs.Save();
    }
    /// <summary>
    /// 获取音效音量大小
    /// </summary>
    /// <returns>音效音量</returns>
    public float GetVolume()
    {
        return volume;
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
    /// 播放警告音效
    /// </summary>
    /// <param name="poisiton">声音播放位置</param>
    /// <param name="volume">音效音量</param>
    public void PlayWarningSound(Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipRefSO.warning, position, volume);
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
    /// <param name="volumeMultiplyer">音效音量</param>
    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplyer = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplyer * volume);
    }
}
