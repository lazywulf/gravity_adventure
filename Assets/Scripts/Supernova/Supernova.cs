using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supernova : MonoBehaviour 
{
    [SerializeField] private GameEvent @event;

    private Collider2D col;
    private Rigidbody2D rb;

	[SerializeField] private float dilationForce = 1f;

    void Awake()
    {
        if (!(TryGetComponent<Rigidbody2D>(out rb) && TryGetComponent<Collider2D>(out col))) {
            Debug.LogError("Rigidbody and Collider missing!!!");
		}
    }

	private void FixedUpdate()
	{
		rb.AddForce(new Vector2(0f, dilationForce), ForceMode2D.Force);
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.CompareTag("Player")) {
            @event.Raise();
		}
	}
}
