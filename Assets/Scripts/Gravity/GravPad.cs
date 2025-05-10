using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravPad : GravForce
{
	[SerializeField] Vector2 gravity = new();
	[SerializeField] ForceMode2D mode = ForceMode2D.Force;
	[SerializeField] bool UseAutoCheeseGravity=false;
    private void Start()
    {
        if (UseAutoCheeseGravity) ChooseDirection();
    }
    public override void ApplyGravitationalForce(Rigidbody2D rb) {
		rb.AddForce(gravity, mode);
	}
	public void ChooseDirection()
	{
        float rad = (transform.eulerAngles.z+90) * Mathf.Deg2Rad;
        gravity = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
    }
}
