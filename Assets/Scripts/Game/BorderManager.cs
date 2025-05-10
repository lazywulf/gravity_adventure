using UnityEngine;

public class BorderManager : MonoBehaviour
{
    public static BorderManager Instance { get; private set; }

    private Transform left;
    private Transform right;
    private float centerX;

    public float LeftLimit => left.position.x;
    public float RightLimit => right.position.x;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
        Instance = this;

        left = transform.Find("Left");
        right = transform.Find("Right");

        if (left == null || right == null)
        {
            Debug.LogError("Left or Right child not found under Borders.");
            enabled = false;
            return;
        }

        centerX = (left.position.x + right.position.x) / 2f;
    }

    public void OnSideEntered(string sideName, Collider2D player)
    {
        Vector3 pos = player.transform.position;

        if (sideName == "Left")
            pos.x = right.position.x - 2f;
        else
            pos.x = left.position.x + 2f;

        player.transform.position = pos;
    }
}
