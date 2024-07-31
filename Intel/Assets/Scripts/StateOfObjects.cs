using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateOfObjects : MonoBehaviour
{
    public void SetState(GameObject figure)
    {
        if (figure.activeInHierarchy)         
            figure.SetActive(false);        
        else
            figure.SetActive(true);        
    }
}
