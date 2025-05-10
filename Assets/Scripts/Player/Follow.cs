using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private bool dependsOnX = false;
    [SerializeField] private bool dependsOnY = false;
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, 0f);

    void LateUpdate()
    {
        if (target != null) {
            transform.position = new Vector3(
                        (dependsOnX)? target.position.x + offset.x: transform.position.x,
                        (dependsOnY) ? target.position.y + offset.y : transform.position.y, 
                        transform.position.z);
        }
    }
}
