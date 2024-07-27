using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CapsuleSettings : MonoBehaviour
{
    [SerializeField] private Capsule _capsule;
    [SerializeField] private Slider _sidesSlider;

    private void Update()
    {
        _capsule.Sides = Convert.ToInt32(_sidesSlider.value);
    }
}
