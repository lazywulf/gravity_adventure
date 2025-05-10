using System.Collections.Generic;
using UnityEngine;

public class RenderFake : MonoBehaviour
{
    public static RenderFake Instance { get; private set; }

    private readonly List<GameObject> fakePlanets = new List<GameObject>();
    private float worldWidth;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
        Instance = this;
    }

    private void Start()
    {
        if (BorderManager.Instance == null)
        {
            Debug.LogError("BorderManager instance not found.");
            return;
        }

        worldWidth = Mathf.Abs(BorderManager.Instance.RightLimit - BorderManager.Instance.LeftLimit);

        GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");
        foreach (GameObject planet in planets)
        {
            Vector3 pos = planet.transform.position;

            fakePlanets.Add(CreateVisualClone(planet, pos + Vector3.left * worldWidth));
            fakePlanets.Add(CreateVisualClone(planet, pos + Vector3.right * worldWidth));
        }
    }

    private GameObject CreateVisualClone(GameObject original, Vector3 newPos)
    {
        GameObject clone = Instantiate(original, newPos, original.transform.rotation);

        foreach (var col in clone.GetComponents<Collider2D>())
            Destroy(col);

        foreach (var rb in clone.GetComponents<Rigidbody2D>())
            Destroy(rb);

        foreach (var comp in clone.GetComponents<MonoBehaviour>())
        {
            if (!(comp is SpriteRenderer))
                Destroy(comp);
        }

        clone.name = original.name + "_Fake";
        clone.transform.SetParent(original.transform);

        return clone;
    }
}
