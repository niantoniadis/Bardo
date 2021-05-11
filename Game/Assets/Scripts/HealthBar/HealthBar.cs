using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    int maxhealth;

    public void SetMaxHealth(int maxHealth)
    {
        maxhealth = maxHealth;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health/(float)maxhealth;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        fill.rectTransform.localScale = new Vector3(health / (float)maxhealth, 1, 1);
    }
}
