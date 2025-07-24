using UnityEngine;

public class FrostWave : MonoBehaviour
{
    public float maxRadius = 5f;
    public float expandDuration = 1f;
    public float fadeDuration = 0.5f;

    private float timer = 0f;
    private Vector3 startScale;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        startScale = transform.localScale;
    }

    void Update()
    {
        timer += Time.deltaTime;

        float progress = Mathf.Clamp01(timer / expandDuration);
        float radius = Mathf.Lerp(0f, maxRadius, progress);

        transform.localScale = new Vector3(radius, radius, 1f);

        // Fade out after wave goes off
        if (timer > expandDuration)
        {
            float fadeProgress = (timer - expandDuration) / fadeDuration;
            Color c = sr.color;
            c.a = Mathf.Lerp(0.5f, 0f, fadeProgress);
            sr.color = c;

            if (fadeProgress >= 1f)
                Destroy(gameObject);
        }
    }
}
