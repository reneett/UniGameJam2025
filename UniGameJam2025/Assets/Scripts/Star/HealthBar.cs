using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public SpriteRenderer backgroundBar;
    public SpriteRenderer barFill;
    private float maxWidth;

    void Start()
    {
        maxWidth = barFill.size.x;
        barFill.size = new Vector2(maxWidth, barFill.size.y);
    }

    public void SetHealth(float current, float max)
    {
        float percent = Mathf.Clamp01(current / max);
        barFill.transform.localScale = new Vector3(percent, 1f, 1f);
        // barFill.size = new Vector2(percent * maxWidth, barFill.size.y);
    }
}
