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
    [SerializeField] private float maxAngularVelocity = 1000f;
    [SerializeField] private LayerMask planetLayer;
    [SerializeField] private float planetDetectRange = 50f;
    [SerializeField] private float groundCheckDistance = 0.05f;

    private float chargeTime = 0f;
    private bool isCharging = false;
    private float moveInput = 0f;

    [SerializeField] PlayerControls controls;

    [SerializeField] private bool isFlying = false;

    [SerializeField] private AudioClip jumpSfx;
    [SerializeField] private AudioClip chargeSfx;
    [SerializeField] private AudioClip fallToGroundSfx;
    [SerializeField] private AudioSource _audioSource;

    void Awake()
    {
        if (!(TryGetComponent<Rigidbody2D>(out rb) && TryGetComponent<Collider2D>(out col)))
        {
            Debug.LogError("Missing Rigidbody or Collider.");
            enabled = false;
        }

        _audioSource = GetComponent<AudioSource>();

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
        hitGround();
        if (isCharging)
        {
            chargeTime += Time.fixedDeltaTime;
            ApplyAngularDamping(spacebarDampingFactor);

            if (jumpSfx != null)
                _audioSource.PlayOneShot(jumpSfx);

        }
    }
    private void hitGround()
    {
        Transform nearestPlanet = GetNearestPlanet();
        if (nearestPlanet != null && IsGrounded(nearestPlanet))
        {
            if (isFlying)
            {
                if (fallToGroundSfx != null)
                    _audioSource.PlayOneShot(fallToGroundSfx, 3.0f);
                isFlying = false;
            }
        }
        else
        {
            isFlying = true;
        }
    }

    private void Charge()
    {
        isCharging = true;
        chargeTime = 0f;
    }

    private void Jump()
    {
        Transform nearestPlanet = GetNearestPlanet();
        
        isCharging = false;

        if (nearestPlanet == null || !IsGrounded(nearestPlanet)) return;

        float t = Mathf.Clamp(chargeTime / maxChargeTime, 0f, 1f);
        float actualForce = Mathf.Lerp(3f, jumpForce, t);
        Vector2 jumpDirection = (transform.position - nearestPlanet.position).normalized;

        if (rb.angularVelocity < jumpThreshold)
        {
            rb.AddForce(jumpDirection * actualForce, ForceMode2D.Impulse);
            if (chargeSfx != null)
                _audioSource.PlayOneShot(chargeSfx);
        }
    }

    private void Roll()
    {
        float chargeMultiplier = isCharging ? 0f : 1f;
        float torqueAmount = -moveInput * torque * chargeMultiplier;
        if(Mathf.Abs(rb.angularVelocity)<maxAngularVelocity)rb.AddTorque(torqueAmount);
	}

    private void ApplyAngularDamping(float dampingFactor) {
        rb.angularVelocity = Mathf.Lerp(rb.angularVelocity, 0f, Time.fixedDeltaTime * dampingFactor);
    }

    private Transform GetNearestPlanet()
    {
        Collider2D[] planets = Physics2D.OverlapCircleAll(transform.position, planetDetectRange, planetLayer);
        Transform nearest = null;
        float minDistance = float.MaxValue;

        foreach (Collider2D planet in planets)
        {
            float dist = Vector2.Distance(transform.position, planet.transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                nearest = planet.transform;
            }
        }

        return nearest;
    }

    private bool IsGrounded(Transform planet)
    {
        Vector2 directionToPlanet = (planet.position - transform.position).normalized;
        CircleCollider2D circle;

        if (!TryGetComponent<CircleCollider2D>(out circle)) throw new MissingComponentException();

        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlanet, circle.radius + groundCheckDistance, planetLayer);

        Debug.DrawRay(transform.position, directionToPlanet * planetDetectRange, Color.green);
        return hit.collider != null;
    }

    private void OnDisable()
	{
        controls.Disable();
	}
}
