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
        maxWidth = barFill.size.x > 0 ? barFill.size.x : 1f;
    }

    public void SetHealth(float current, float max)
    {
        float percent = Mathf.Clamp01(current / max);
        barFill.size = new Vector2(maxWidth * percent, barFill.size.y);
    }
}
