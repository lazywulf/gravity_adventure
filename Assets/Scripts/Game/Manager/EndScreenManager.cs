using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreenManager : MonoBehaviour
{
    [Header("First")]
    public GameObject background1;
    public Transform spinningObject;
    public float background1FallSpeed = 0.2f;
    public float spinSpeed = 30f;

    [Header("Second")]
    public GameObject background2;
    public GameObject collisionTarget;
    public GameObject finalImage;
    public float fallSpeedInPhase2 = 100f;

    [Header("Ending")]
    public GameObject finalLine;

    [Header("Setting")]
    public float transitionDelay = 2f;
    public float endSceneDelay = 3f;
    public string nextSceneName = "SelectLevel";

    private bool switched = false;
    private bool collided = false;

    void Start()
    {
        collisionTarget.SetActive(false);
        background1.SetActive(true);
        background2.SetActive(false);
        finalImage.SetActive(false);
        finalLine.SetActive(false);
        StartCoroutine(SwitchToPhase2());
    }

    void Update()
    {
        if (!switched)
        {
            background1.transform.position += Vector3.down * background1FallSpeed * Time.deltaTime;

            spinSpeed += 10f * Time.deltaTime;
            spinningObject.Rotate(Vector3.forward * spinSpeed * Time.deltaTime);
        }
        else if (!collided)
        {
            spinningObject.position += Vector3.down * fallSpeedInPhase2 * Time.deltaTime;

            if (spinningObject.position.y <= collisionTarget.transform.position.y + 1)
            {
                collisionTarget.SetActive(false);
                finalImage.SetActive(true);
                collided = true;
                StartCoroutine(ShowFinalLine());
            }
        }


        
    }

    IEnumerator SwitchToPhase2()
    {
        yield return new WaitForSeconds(transitionDelay);
        background1.SetActive(false);
        background2.SetActive(true);
        collisionTarget.SetActive(true);

        Vector3 newPos = collisionTarget.transform.position + new Vector3(0, 30f, 0); 
        spinningObject.position = newPos;

        switched = true;
    }

    IEnumerator ShowFinalLine()
    {
        yield return new WaitForSeconds(1.0f);
        finalLine.SetActive(true);
        yield return new WaitForSeconds(endSceneDelay);
        SceneManager.LoadScene(nextSceneName);
    }
}
