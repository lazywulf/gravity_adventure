using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movements : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;

    [SerializeField] private float torque = 10f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float maxChargeTime = 2f;
    [SerializeField] private float spacebarDampingFactor  = 100f;
    [SerializeField] private float jumpThreshold = 1f;

    private float chargeTime = 0f;
    private bool isCharging = false;
    private float moveInput = 0f;

    [SerializeField] PlayerControls controls;

	void Awake()
    {
        if (!(TryGetComponent<Rigidbody2D>(out rb) && TryGetComponent<Collider2D>(out col)))
        {
            Debug.LogError("Missing Rigidbody or Collider.");
            enabled = false;
        }

        controls = new();
        controls.gameplay.RollClockwise.performed += ctx => moveInput = 1f;
        controls.gameplay.RollCounterClockwise.performed += ctx => moveInput = -1f;
        controls.gameplay.RollClockwise.canceled += ctx => moveInput = 0f;
        controls.gameplay.RollCounterClockwise.canceled += ctx => moveInput = -0f;

        controls.gameplay.Jump.performed += ctx => Jump();
        controls.gameplay.Charge.performed += ctx => Charge();
    }

	void OnEnable() {
        controls.Enable();	
	}

	void FixedUpdate()
    {
        Roll();
        if (isCharging)
        {
            chargeTime += Time.fixedDeltaTime;
            ApplyAngularDamping(spacebarDampingFactor);
        }
    }

    private void Charge()
    {
        isCharging = true;
        chargeTime = 0f;
    }

    private void Jump()
    {
        isCharging = false;
        float t = Mathf.Clamp(chargeTime / maxChargeTime, 0f, 1f);
        float actualForce = Mathf.Lerp(3f, jumpForce, t);
        if (rb.angularVelocity < jumpThreshold) {
            rb.AddForce(Vector2.up * actualForce, ForceMode2D.Impulse);
        }
    }

    private void Roll()
    {
        float chargeMultiplier = isCharging ? 0f : 1f;
        float torqueAmount = -moveInput * torque * chargeMultiplier;
        rb.AddTorque(torqueAmount);
	}

    private void ApplyAngularDamping(float dampingFactor) {
        rb.angularVelocity = Mathf.Lerp(rb.angularVelocity, 0f, Time.fixedDeltaTime * dampingFactor);
    }


    private void OnDisable()
	{
        controls.Disable();
	}
}
