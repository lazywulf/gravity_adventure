using UnityEngine;

public class BorderSide : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            BorderManager.Instance.OnSideEntered(this.name, other);
    }
}