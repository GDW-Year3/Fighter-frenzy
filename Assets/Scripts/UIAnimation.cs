using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimation : MonoBehaviour
{
    private Slider slider;
    private void Start()
    {
        slider = GetComponent<Slider>();
    }
    public void SliderBar(float maxValue, float currentValue)
    {
        slider.maxValue = maxValue;
        slider.value = currentValue;
    }
}
