using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StamainaBar : MonoBehaviour
{

    public Slider slider;
    public TMP_Text stamainaText;
    public ThirdPersonMovement thp;
    public Gradient gradient;
    public Image fill;

    public void SetStamaina(int stamaina)
    {
        slider.value = stamaina;
    }

    public void SetMaxStamaina(int stamaina)
    {
        slider.maxValue = stamaina;
        slider.value = stamaina;

        fill.color = gradient.Evaluate(1f);

    }

    public void Update()
    {
        stamainaText.text = thp.currentStamaina + "/" + thp.maxStamaina;
    }

}
