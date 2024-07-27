using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    public int Grid { set { _gridSize = value; } }
    public float Radius { set { _radius = value; } }

    private int _gridSize = 3;
    private float _radius = 1;
    private Mesh mesh;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;        
        mesh.RecalculateNormals(); 
    }

    private void Update()
    {
        mesh.vertices = CreateVertices();
        mesh.triangles = CreateTriangles();
    }

    Vector3[] CreateVertices()
    {
        int numVertices = (_gridSize + 1) * (_gridSize + 1);
        Vector3[] vertices = new Vector3[numVertices];
        Vector3[] normals = new Vector3[numVertices];
        int index = 0;

        for (int y = 0; y <= _gridSize; y++)
        {
            for (int x = 0; x <= _gridSize; x++)
            {
                float theta = (float)x / _gridSize * Mathf.PI * 2f;
                float phi = (float)y / _gridSize * Mathf.PI;
                phi += Mathf.PI; 

                float xCoord = _radius * Mathf.Sin(phi) * Mathf.Cos(theta);
                float yCoord = _radius * Mathf.Cos(phi);
                float zCoord = _radius * Mathf.Sin(phi) * Mathf.Sin(theta);

                vertices[index] = new Vector3(xCoord, yCoord, zCoord);
                normals[index] = vertices[index].normalized;
                index++;
            }
        }
        return vertices;
    }

    int[] CreateTriangles()
    {     
        int numTriangles = _gridSize * _gridSize * 6;  
        int[] triangles = new int[numTriangles];
        int index = 0;

        for (int y = 0; y < _gridSize; y++)
        {
            for (int x = 0; x < _gridSize; x++)
            {
                
                int topLeft = y * (_gridSize + 1) + x;
                int topRight = topLeft + 1;
                int bottomLeft = (y + 1) * (_gridSize + 1) + x;
                int bottomRight = bottomLeft + 1;

                
                triangles[index++] = topLeft;
                triangles[index++] = bottomLeft;
                triangles[index++] = topRight;

                
                triangles[index++] = topRight;
                triangles[index++] = bottomLeft;
                triangles[index++] = bottomRight;
            }
        }
        return triangles;
    }
}