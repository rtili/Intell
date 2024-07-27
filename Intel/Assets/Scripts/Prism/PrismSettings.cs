using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrismSettings : MonoBehaviour
{
    [SerializeField] private Prism _prism;
    [SerializeField] private Slider _sidesSlider;

    private void Update()
    {
        _prism.Sides = Convert.ToInt32(_sidesSlider.value);
    }
}
