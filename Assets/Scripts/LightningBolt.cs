using UnityEngine;

public class LightningBolt : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;

    private LineRenderer lr;
    public float lifeTime = 0.75f;
    public int segments = 37;
    public float jitterAmount = 0.5f;

    private float timer;
    private bool initialized = false;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = segments;
        timer = lifeTime;
    }

    public void Initialize(Transform start, Transform end)
    {
        startPoint = start;
        endPoint = end;
        initialized = true;
        timer = lifeTime;
    }

    void Update()
    {
        if (!initialized || startPoint == null || endPoint == null) return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            lr.enabled = false;
            Destroy(gameObject); // auto-destroy when done
            return;
        }

        DrawLightning();
    }

    void DrawLightning()
    {
        Vector3 start = startPoint.position;
        Vector3 end = endPoint.position;

        for (int i = 0; i < segments; i++)
        {
            float t = (float)i / (segments - 1);
            Vector3 point = Vector3.Lerp(start, end, t);

            if (i > 0 && i < segments - 1)
            {
                point += new Vector3(
                    Random.Range(-jitterAmount, jitterAmount),
                    Random.Range(-jitterAmount, jitterAmount),
                    Random.Range(-jitterAmount, jitterAmount)
                );
            }

            lr.SetPosition(i, point);
        }
    }
}
