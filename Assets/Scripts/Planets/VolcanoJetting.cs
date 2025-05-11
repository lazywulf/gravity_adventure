using System.Collections;
using UnityEngine;
enum State{
    waiting,
    Jetting,
    Cooldown
}
public class VolcanoJetting : MonoBehaviour
{
    [SerializeField] private float jetDuration = 2f;
    [SerializeField] private float jetCooldown = 5f;
    [SerializeField] private float jetOffset = 0f;
    [SerializeField] private State currentState = State.waiting;

    [SerializeField] private Transform playerTransform;

    [SerializeField] private AudioClip jetSfx;
    [SerializeField] private AudioSource _audioSource;

    private GravPad gp;
    private float remainedTime;

	private void Awake()
	{
        if (!TryGetComponent<GravPad>(out gp)) {
            enabled = false;
            throw new MissingComponentException("Missing GravPad");
        }
        playerTransform = GameObject.Find("Player").transform;
    }

    private void playJetSfx()
    {
        float distance = Vector2.Distance(transform.position, playerTransform.position);
        if(jetSfx != null)
        {

            float volumn = 0.0f;
            if (distance < 10.0f)
            {
                volumn = 0.05f;
            }
            else if (distance < 5.0f)
            {
                volumn = 0.025f;
            }
            _audioSource.PlayOneShot(jetSfx, volumn);
        }

    }

	private void Start()
    {
        remainedTime = jetOffset;
    }

    private void Update()
    {
        remainedTime -= Time.deltaTime;
        if (remainedTime < 0f)
        {
            TransformState();
        }
        gp.GravOn = (currentState == State.Jetting);
    }
    private void TransformState()
    {
        if(currentState == State.Jetting)
        {
            currentState = State.Cooldown;
            remainedTime = jetCooldown;
        }
        else
        {
            currentState = State.Jetting;
            remainedTime = jetDuration;
            playJetSfx();

        }
    }
}