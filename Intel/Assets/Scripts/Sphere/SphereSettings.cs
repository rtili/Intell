using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SphereSettings : MonoBehaviour, IMeshSettings
{
    [SerializeField] private Sphere _sphere;
    [SerializeField] private Slider _gridSlider;
  
    public void ChangeValue()
    {
        _sphere.Grid = Convert.ToInt32(_gridSlider.value);
    }
}
