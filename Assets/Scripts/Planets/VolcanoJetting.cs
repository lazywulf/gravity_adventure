// VolcanoJetting.cs
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

    private GravPad gp;
    private float remainedTime;

	private void Awake()
	{
        if (!TryGetComponent<GravPad>(out gp)) {
            enabled = false;
            throw new MissingComponentException("Missing GravPad");
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
        }
    }
}