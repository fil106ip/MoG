using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public TMP_Text healthText;
    public CharacterStats charStats;
    public Gradient gradient;
    public Image fill;

    public void SetHealth(int health)
    {
        slider.value = health;
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

       fill.color = gradient.Evaluate(1f);

    }

    public void Update()
    {
        healthText.text = charStats.currentHealth + "/" + charStats.maxHealth;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}
