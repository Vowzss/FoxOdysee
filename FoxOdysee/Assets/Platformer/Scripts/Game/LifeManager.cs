using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public Slider slider;
    public Gradient deltaColor;
    public Image fill;
    public Text text;

    private void Awake()
    {
        text = GetComponentInChildren<Text>();
    }

    public void SetText(string t)
    {
        text.text = t;
    }

    public void SetMaxLife(float life)
    {
        slider.maxValue = life;
        slider.value = life;

        fill.color = deltaColor.Evaluate(1f);
    }

    public void SetLife(float life)
    {
        slider.value = life;
        fill.color = deltaColor.Evaluate(slider.normalizedValue);
    }
}
