using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MusicManager : MonoBehaviour
{
    private static MusicManager _instance;
    private AudioSource _audioSource;

    [Header("�H�J�]�w")]
    [Tooltip("�q 0 ���j��ؼЭ��q�һݬ��")]
    public float fadeInDuration = 2f;
    [Tooltip("�H�J�����᪺���q�j�p (0~1)")]
    [Range(0f, 1f)]
    public float targetVolume = 0.3f;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);

            _audioSource = GetComponent<AudioSource>();
            _audioSource.volume = 0f;          // �_�l���]���R��
            _audioSource.Play();               // ������A����A�H�J

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
        _audioSource.volume = targetVolume;  // �T�O�������
    }

    // ��L�����k�i�O�d
    public void StopMusic()
    {
        _audioSource.Stop();
    }

    public void SetVolume(float volume)
    {
        _audioSource.volume = Mathf.Clamp01(volume);
    }
}

