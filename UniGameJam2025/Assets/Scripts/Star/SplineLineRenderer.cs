using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(SplineContainer))]
public class SplineLineRenderer : MonoBehaviour
{
    public int resolution = 50; // Number of points on the curve

    private LineRenderer lineRenderer;
    private SplineContainer splineContainer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        splineContainer = GetComponent<SplineContainer>();

        if (splineContainer == null || splineContainer.Spline.Count < 2)
        {
            Debug.LogWarning("SplineContainer missing or not enough points.");
            return;
        }

        lineRenderer.positionCount = resolution;

        for (int i = 0; i < resolution; i++)
        {
            float t = i / (float)(resolution - 1);
            Vector3 point = splineContainer.EvaluatePosition(t);
            lineRenderer.SetPosition(i, point);
        }
    }
}