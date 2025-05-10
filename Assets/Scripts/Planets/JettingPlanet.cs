// VolcanoJetting.cs
using System.Collections;
using UnityEngine;
enum state{
    waiting,
    Jetting,
    Cooldown
}
public class VolcanoJetting : MonoBehaviour
{
    [SerializeField] private float jetDuration = 2f;
    [SerializeField] private float jetCooldown = 5f;
    [SerializeField] private float jetOffset = 0f;
    [SerializeField] private state currentState = state.waiting;
    private float remainedTime;
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
        if(currentState == state.Jetting)
        {
            this.gameObject.GetComponent<GravPad>().gravityEnable = true;
        }
        else
        {
            this.gameObject.GetComponent<GravPad>().gravityEnable = false;
        }
    }
    private void TransformState()
    {
        if(currentState == state.Jetting)
        {
            currentState = state.Cooldown;
            remainedTime = jetCooldown;
        }
        else
        {
            currentState = state.Jetting;
            remainedTime = jetDuration;
        }
    }
}