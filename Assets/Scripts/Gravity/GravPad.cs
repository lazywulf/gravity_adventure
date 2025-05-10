using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravPad : GravForce
{
	[SerializeField] Vector2 gravity = new();
	[SerializeField] ForceMode2D mode = ForceMode2D.Force;

	public override void ApplyGravitationalForce(Rigidbody2D rb) {
		rb.AddForce(gravity, mode);
	}
}
