using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prism : MonoBehaviour
{
    public int Sides { set { _sides = value;} }
    public float Radius { set { _radius = value;} }
    public float Height { set { _height = value;} }

    private int _sides = 6;
    private float _radius = 1f; 
    private float _height = 2f;
    private Mesh mesh;


    private void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

    private void Update()
    {
        mesh.vertices = CalculateVertices();
        mesh.triangles = CalculateTriangles();
    }

    private Vector3[] CalculateVertices()
    {
        float angle = 360f / _sides;
        int vertexCount = (_sides + 1) * 2;
        Vector3[] vertices = new Vector3[vertexCount];

        for (int i = 0; i <= _sides; i++)
        {
            float radianAngle = Mathf.Deg2Rad * i * angle;
            float x = _radius * Mathf.Cos(radianAngle);
            float z = _radius * Mathf.Sin(radianAngle);
            vertices[i] = new Vector3(x, 0, z);
        }

        for (int i = 0; i <= _sides; i++)
        {
            float radianAngle = Mathf.Deg2Rad * i * angle;
            float x = _radius * Mathf.Cos(radianAngle);
            float z = _radius * Mathf.Sin(radianAngle);
            vertices[i + _sides + 1] = new Vector3(x, _height, z);
        }
        return vertices;
    }

    private int[] CalculateTriangles()
    {
        int triangleCount = _sides * 12;
        int[] triangles = new int[triangleCount];
        int triangleIndex = 0;

        for (int i = 0; i < _sides; i++)
        {
            //side 1
            triangles[triangleIndex++] = i;
            triangles[triangleIndex++] = i + _sides + 1;
            triangles[triangleIndex++] = (i + 1) % _sides + _sides + 1;

            //side 2
            triangles[triangleIndex++] = i;
            triangles[triangleIndex++] = (i + 1) % _sides + _sides + 1;
            triangles[triangleIndex++] = (i + 1) % _sides;
        }
        //top
        for (int i = 0; i < _sides; i++)
        {
            triangles[triangleIndex++] = _sides + 1;
            triangles[triangleIndex++] = _sides + 1 + ((i + 1) % _sides);
            triangles[triangleIndex++] = _sides + 1 + i;
        }

        //bottom
        for (int i = 0; i < _sides; i++)
        {
            triangles[triangleIndex++] = i;
            triangles[triangleIndex++] = (i + 1) % _sides;
            triangles[triangleIndex++] = _sides; 
        }
        return triangles;
    }
}