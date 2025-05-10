using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGrav : GravForce
{
	[SerializeField] private float mass;
	[SerializeField] private bool useThresholdBoundary = false; // if true, calculate gravfield using mass and alpha
	[SerializeField] private float gravLowBound = .05f;
	//[SerializeField] ForceMode2D mode = ForceMode2D.Force;

	private void LateUpdate()
	{
		if (useThresholdBoundary)
		{
			float gravEffectiveRange = mass / gravLowBound;
			transform.localScale = new Vector2(gravEffectiveRange, gravEffectiveRange);
		}
	}

	protected override void ApplyGravitationalForce(Rigidbody2D rb)
	{
		Vector2 pull = this.rb.position - rb.position;
		float dist = pull.magnitude;
		rb.AddForce(pull * (mass / dist)); // F_g is proportional to m/r
	}
}
