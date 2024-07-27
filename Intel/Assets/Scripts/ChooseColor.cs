using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseColor : MonoBehaviour
{
    [SerializeField] private MeshRenderer mesh;

    public void ChangeColor(Material mat)
    {
        mesh.material = mat;
    }
}
