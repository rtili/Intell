using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigate : MonoBehaviour
{
    [SerializeField] private GameObject[] _objectsOff;
    [SerializeField] private GameObject[] _objectsOn;

    public void PressButton()
    {
        gameObject.SetActive(false);
        foreach (var item in _objectsOff)
        {
            item.SetActive(false);
        }
        foreach (var item in _objectsOn)
        {
            item.SetActive(true);
        }
    }
}
