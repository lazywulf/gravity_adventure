using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GravForce : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Collider2D col;
    void Start() 
    {
        if (!(TryGetComponent<Rigidbody2D>(out rb) && TryGetComponent<Collider2D>(out col))) {
            Debug.LogWarning($"{gameObject.name} is missing Rigidbody or Collider. Disabling GravForce.");
            enabled = false;
        }
    }

	private void OnTriggerStay2D(Collider2D collider) {
        ApplyGravitationalForce(collider.attachedRigidbody);
	}

	public abstract void ApplyGravitationalForce(Rigidbody2D rb);

}
