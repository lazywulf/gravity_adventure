using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravPad : GravForce
{
	[SerializeField] ForceMode2D mode = ForceMode2D.Force;
	[SerializeField] bool useAutoDirection=false;
    [SerializeField] float gravityScale = 1f;
    [SerializeField] float gravityDirection;
    [SerializeField] public bool gravityEnable= true;

    void Awake()
    {
        if (useAutoDirection) AutoDirection();
    }
    public override void ApplyGravitationalForce(Rigidbody2D rb) {
        Vector2 addGravity=Vector2.zero;
        if (gravityEnable) addGravity = TransformToVector(gravityDirection) * gravityScale;
        Debug.Log(TransformToVector(gravityDirection) * gravityScale);
        rb.AddForce(addGravity, mode);
	}
	public Vector2 TransformToVector(float direction)
	{
        float rad =  direction* Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
    }
    public void AutoDirection()
    {
        gravityDirection= (transform.eulerAngles.z + 90);
    }
}
