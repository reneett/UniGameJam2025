using UnityEngine;
using UnityEngine.Splines;

public class Star : MonoBehaviour
{
    public SplineContainer splineContainer;
    public float speed = 2f;
    private float currentSplineProgress = 0f;

    void Start()
    {
        if (splineContainer)
        {
            transform.position = splineContainer.Spline.EvaluatePosition(0f);
        }
    }
  void Update()
    {
        if (splineContainer == null) return;

        float length = splineContainer.Spline.GetLength();
        float step = speed * Time.deltaTime / length;
        currentSplineProgress += step;
        currentSplineProgress = Mathf.Clamp01(currentSplineProgress); // progress is between 0 & 1, so don't let it go over

        Vector3 position = splineContainer.Spline.EvaluatePosition(currentSplineProgress);
        transform.position = position;
    }
}
