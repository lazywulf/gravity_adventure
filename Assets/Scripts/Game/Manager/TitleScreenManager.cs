using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    public Transform[] floatingObjects; // �I������
    public Transform[] titleFragments;  // ���D�H���̡]��ʫ����^
    public Transform apple;             // ī�G�]�u�ʡ^
    public Transform planet;            // ���U��P�]����^
    public GameObject pressAnyKeyImage; // "Press any key to start" �Ϲ� (PNG)

    public AudioSource bgmSource;       // �I������
    public AudioSource sfxSource;       // ���ļ���
    public AudioClip clickSound;        // �I������

    public float floatAmplitude = 0.5f;
    public float floatFrequency = 0.5f;
    public float floatOffset = 0.5f;

    public float appleSpinSpeed = 50f;
    public float planetRotationSpeed = 3f;

    public float transitionDelay = 1.0f; // << �������ɶ��]��^
    public string nextSceneName = "MainScene"; // ��������W��

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

        // �I������}�B
        for (int i = 0; i < floatingObjects.Length; i++)
        {
            float y = Mathf.Sin(time * floatFrequency + i) * floatAmplitude;
            float x = Mathf.Cos(time * floatFrequency * 0.5f + i) * floatOffset;
            floatingObjects[i].position = originalPositions[i] + new Vector3(x, y, 0f);
        }

        // ���D�H���C�C�ƴ��P�L�p����
        for (int i = 0; i < titleFragments.Length; i++)
        {
            Vector3 drift = new Vector3(
                Mathf.PerlinNoise(i * 0.1f, time * 0.00003f) - 0.5f,
                Mathf.PerlinNoise(i * 0.2f, time * 0.00003f + 100) - 0.5f, 0f) * 0.1f;

            titleFragments[i].position = titleOriginalPositions[i] + drift * time;

            float rotationSpeed = Mathf.Sin(time * 0.0000001f + fragmentRotationOffsets[i]) * 5f;
            titleFragments[i].Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }

        // ī�G�u�� & �P�y����
        apple.Rotate(Vector3.forward * -appleSpinSpeed * Time.deltaTime);
        planet.Rotate(Vector3.forward * planetRotationSpeed * Time.deltaTime);

        // �{�{�� Press Any Key �Ϲ�
        blinkTimer += Time.deltaTime;
        if (blinkTimer > 0.5f)
        {
            isImageVisible = !isImageVisible;
            if (pressAnyKeyImage != null)
                pressAnyKeyImage.SetActive(isImageVisible);
            blinkTimer = 0f;
        }

        // ��������ü��񭵮� + �������
        if (!hasPressedKey && Input.anyKeyDown)
        {
            hasPressedKey = true;
            if (sfxSource != null && clickSound != null)
            {
                sfxSource.PlayOneShot(clickSound);
            }
            StartCoroutine(LoadNextScene()); // << �Ұ����
        }
    }

    IEnumerator LoadNextScene() // <<���
    {
        yield return new WaitForSeconds(transitionDelay);
        SceneManager.LoadScene(nextSceneName);
    }
}