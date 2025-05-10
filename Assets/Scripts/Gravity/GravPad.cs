using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravPad : GravForce
{
    [SerializeField] private ForceMode2D mode = ForceMode2D.Force;
    [SerializeField] private bool usePointingDirOrigin = false;
    [SerializeField] private bool enableGravity = true;
    [SerializeField] private float gravityScale = 1f;
    [SerializeField] private float gravityDirection;
    public bool GravOn {
        get { return enableGravity; }
        set { enableGravity = value; }
    }

    protected override void ApplyGravitationalForce(Rigidbody2D rb) {
        Vector2 force = Vector2.zero;

        if (usePointingDirOrigin) gravityDirection = (transform.eulerAngles.z + 90);
        if (enableGravity) force = TransformToVector(gravityDirection) * gravityScale;
        rb.AddForce(force, mode);
	}

	private  Vector2 TransformToVector(float direction)
	{
        float rad = direction* Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
    }

}
