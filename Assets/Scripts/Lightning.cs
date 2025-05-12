using UnityEngine;


public class Lightning : MonoBehaviour
{
    public Transform startPoint;      // Hand or origin point
    public Transform targetPoint;     // Target to strike (optional)
    public int segments = 10;         // Number of points in the lightning
    public float jitterAmount = 0.2f; // How jagged the lightning is
    public float duration = 0.1f;     // How long the lightning lasts

    public LineRenderer lineRenderer;
    private float timer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segments;
        lineRenderer.enabled = false;
    }

    void Update()
    {
        // Example trigger (replace this with your own input/logic)
        if (Input.GetKeyDown(KeyCode.N))
        {
            FireLightning();
        }

        if (lineRenderer.enabled)
        {
            timer += Time.deltaTime;
            if (timer >= duration)
            {
                lineRenderer.enabled = false;
                timer = 0f;
            }
        }
    }

    void FireLightning()
    {
        if (startPoint == null || targetPoint == null)
            return;

        Vector3 start = startPoint.position;
        Vector3 end = targetPoint.position;

        lineRenderer.enabled = true;

        for (int i = 0; i < segments; i++)
        {
            float t = (float)i / (segments - 1);
            Vector3 point = Vector3.Lerp(start, end, t);

            // Add some random jitter
            point += new Vector3(
                Random.Range(-jitterAmount, jitterAmount),
                Random.Range(-jitterAmount, jitterAmount),
                Random.Range(-jitterAmount, jitterAmount)
            );

            lineRenderer.SetPosition(i, point);
        }
    }
}
