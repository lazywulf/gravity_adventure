using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    public Transform[] floatingObjects; // 背景物件
    public Transform[] titleFragments;  // 標題碎片們（手動指派）
    public Transform apple;             // 蘋果（滾動）
    public Transform planet;            // 底下行星（旋轉）
    public GameObject pressAnyKeyImage; // "Press any key to start" 圖像 (PNG)

    public AudioSource bgmSource;       // 背景音樂
    public AudioSource sfxSource;       // 音效播放器
    public AudioClip clickSound;        // 點擊音效

    public float floatAmplitude = 0.5f;
    public float floatFrequency = 0.5f;
    public float floatOffset = 0.5f;

    public float appleSpinSpeed = 50f;
    public float planetRotationSpeed = 3f;

    public float transitionDelay = 1.0f; // << 轉場延遲時間（秒）
    public string nextSceneName = "MainScene"; // 轉場場景名稱

    private Vector3[] originalPositions;
    private Vector3[] titleOriginalPositions;
    private float[] fragmentRotationOffsets;
    private float blinkTimer = 0f;
    private bool isImageVisible = true;
    private bool hasPressedKey = false;

    void Start()
    {
        originalPositions = new Vector3[floatingObjects.Length];
        for (int i = 0; i < floatingObjects.Length; i++)
            originalPositions[i] = floatingObjects[i].position;

        titleOriginalPositions = new Vector3[titleFragments.Length];
        fragmentRotationOffsets = new float[titleFragments.Length];
        for (int i = 0; i < titleFragments.Length; i++)
        {
            titleOriginalPositions[i] = titleFragments[i].position;
            fragmentRotationOffsets[i] = Random.Range(0f, 360f);
        }

        if (bgmSource != null && !bgmSource.isPlaying)
        {
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    void Update()
    {
        float time = Time.time;

        // 背景物件漂浮
        for (int i = 0; i < floatingObjects.Length; i++)
        {
            float y = Mathf.Sin(time * floatFrequency + i) * floatAmplitude;
            float x = Mathf.Cos(time * floatFrequency * 0.5f + i) * floatOffset;
            floatingObjects[i].position = originalPositions[i] + new Vector3(x, y, 0f);
        }

        // 標題碎片慢慢飄散與微小偏轉
        for (int i = 0; i < titleFragments.Length; i++)
        {
            Vector3 drift = new Vector3(
                Mathf.PerlinNoise(i * 0.1f, time * 0.00003f) - 0.5f,
                Mathf.PerlinNoise(i * 0.2f, time * 0.00003f + 100) - 0.5f, 0f) * 0.1f;

            titleFragments[i].position = titleOriginalPositions[i] + drift * time;

            float rotationSpeed = Mathf.Sin(time * 0.0000001f + fragmentRotationOffsets[i]) * 5f;
            titleFragments[i].Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }

        // 蘋果滾動 & 星球旋轉
        apple.Rotate(Vector3.forward * -appleSpinSpeed * Time.deltaTime);
        planet.Rotate(Vector3.forward * planetRotationSpeed * Time.deltaTime);

        // 閃爍的 Press Any Key 圖像
        blinkTimer += Time.deltaTime;
        if (blinkTimer > 0.5f)
        {
            isImageVisible = !isImageVisible;
            if (pressAnyKeyImage != null)
                pressAnyKeyImage.SetActive(isImageVisible);
            blinkTimer = 0f;
        }

        // 偵測按鍵並播放音效 + 執行轉場
        if (!hasPressedKey && Input.anyKeyDown)
        {
            hasPressedKey = true;
            if (sfxSource != null && clickSound != null)
            {
                sfxSource.PlayOneShot(clickSound);
            }
            StartCoroutine(LoadNextScene()); // << 啟動轉場
        }
    }

    IEnumerator LoadNextScene() // <<轉場
    {
        yield return new WaitForSeconds(transitionDelay);
        SceneManager.LoadScene(nextSceneName);
    }
}