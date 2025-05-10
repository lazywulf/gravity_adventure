using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGrav : GravForce
{
	[SerializeField] float mass;
	//[SerializeField] ForceMode2D mode = ForceMode2D.Force;

	public override void ApplyGravitationalForce(Rigidbody2D rb)
	{
		Vector2 pull = this.rb.position - rb.position;
		float dist = pull.magnitude;
		rb.AddForce(pull * (mass / dist)); // F_g is proportional to m/r
	}
}
