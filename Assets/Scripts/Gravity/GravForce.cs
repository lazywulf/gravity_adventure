using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GravForce : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Collider2D col;
    protected HashSet<Rigidbody2D> trackedBodies = new();

    void Awake() 
    {
        if (!(TryGetComponent<Rigidbody2D>(out rb) && TryGetComponent<Collider2D>(out col))) {
            Debug.LogWarning($"{gameObject.name} is missing Rigidbody or Collider. Disabling GravForce.");
            enabled = false;
        }
    }

	private void FixedUpdate()
	{
        // force-update stationary objects
        foreach (var rb in trackedBodies) {
            if (rb.velocity == Vector2.zero && rb.angularVelocity == 0f) {
                ApplyGravitationalForce(rb);
			}
		}

    }

	private void OnTriggerEnter2D(Collider2D collider)
	{
        trackedBodies.Add(collider.attachedRigidbody);
	}

	private void OnTriggerStay2D(Collider2D collider) {
        ApplyGravitationalForce(collider.attachedRigidbody);
	}

	protected abstract void ApplyGravitationalForce(Rigidbody2D rb);

}
