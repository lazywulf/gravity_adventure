using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MusicManager : MonoBehaviour
{
    private static MusicManager _instance;
    private AudioSource _audioSource;

    [Header("淡入設定")]
    [Tooltip("從 0 漸強到目標音量所需秒數")]
    public float fadeInDuration = 2f;
    [Tooltip("淡入完成後的音量大小 (0~1)")]
    [Range(0f, 1f)]
    public float targetVolume = 0.3f;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);

            _audioSource = GetComponent<AudioSource>();
            _audioSource.volume = 0f;          // 起始先設為靜音
            _audioSource.Play();               // 先播放，之後再淡入

            StartCoroutine(FadeIn());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator FadeIn()
    {
        float elapsed = 0f;
        while (elapsed < fadeInDuration)
        {
            elapsed += Time.deltaTime;
            _audioSource.volume = Mathf.Lerp(0f, targetVolume, elapsed / fadeInDuration);
            yield return null;
        }
        _audioSource.volume = targetVolume;  // 確保結尾精準
    }

    // 其他控制方法可保留
    public void StopMusic()
    {
        _audioSource.Stop();
    }

    public void SetVolume(float volume)
    {
        _audioSource.volume = Mathf.Clamp01(volume);
    }
}

