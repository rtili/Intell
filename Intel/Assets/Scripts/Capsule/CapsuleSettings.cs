using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CapsuleSettings : MonoBehaviour, IMeshSettings
{
    [SerializeField] private Capsule _capsule;
    [SerializeField] private Slider _sidesSlider;
  
    public void ChangeValue()
    {
        _capsule.Sides = Convert.ToInt32(_sidesSlider.value);
    }
}
