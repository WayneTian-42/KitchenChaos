using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    /// <summary>
    /// 全局单例
    /// </summary>
    public static MusicManager Instance { get; private set; }
    /// <summary>
    /// 音源
    /// </summary>
    private AudioSource audioSource;
    /// <summary>
    /// 音量大小
    /// </summary>
    private float volume = 0.5f;
    /// <summary>
    /// 音乐音量字符串常量
    /// </summary>
    private const string PlayerPrefsMusicVolume = "MusicVolume";

    private void Awake()
    {
        Instance = this;

        audioSource = GetComponent<AudioSource>();
        // 获取存储的音量大小
        volume = PlayerPrefs.GetFloat(PlayerPrefsMusicVolume, volume);
        audioSource.volume = volume;
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
        audioSource.volume = volume;

        // 存储音乐音量
        PlayerPrefs.SetFloat(PlayerPrefsMusicVolume, volume);
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
}
