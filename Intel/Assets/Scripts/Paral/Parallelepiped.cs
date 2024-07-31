using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Parallelepiped : MeshFigure
{    
    public float Widht { set { _widht = value; CreateFigure(); } }
    public float Height { set { _height = value; CreateFigure(); } }
    public float Depth { set { _depth = value; CreateFigure(); } }

    private float _widht = 1, _height = 1, _depth = 1;

    /// <summary>
    /// ��������� ������ ���������������.
    /// </summary>
    /// <returns>������ ������.</returns>
    protected override Vector3[] SetVertices()
    {
        Vector3[] vertices = {
            // ������� ������� ������� ���������������
            new Vector3 (0, 0, 0),
            new Vector3 (_widht, 0, 0),
            new Vector3 (_widht, _height, 0),
            new Vector3 (0, _height, 0),
            // ������� ������ ������� ���������������
            new Vector3 (0, _height, _depth),
            new Vector3 (_widht, _height, _depth),
            new Vector3 (_widht, 0, _depth),
            new Vector3 (0, 0, _depth),
        };
        return vertices;
    }

    /// <summary>
    /// ��������� ������������� ���������������.
    /// </summary>
    /// <returns>������ �������� �������������.</returns>
    protected override int[] SetTriangles()
    {
        int[] triangles = {
            // ������������ ������� ������� ���������������
            0, 2, 1,
            0, 3, 2,
            // ������������ ������� ������� ���������������
            2, 3, 4,
            2, 4, 5,
            // ������������ ������ ������� ���������������
            1, 2, 5,
            1, 5, 6,
            // ������������ ����� ������� ���������������
            0, 7, 4,
            0, 4, 3,
            // ������������ ������ ������� ���������������
            5, 4, 7,
            5, 7, 6,
            // ������������ ������ ������� ���������������
            0, 6, 7,
            0, 1, 6
        };
        return triangles;
    }

    /// <summary>
    /// ��������� UV-��������� ��� ���������������.
    /// </summary>
    /// <returns>������ UV-���������.</returns>
    protected override Vector2[] SetUVs()
    {
        Vector2[] uvs = {
            // UV-���������� ��� ������� �������
            new Vector2(0, 0), 
            new Vector2(1, 0), 
            new Vector2(1, 1), 
            new Vector2(0, 1), 
            // UV-���������� ��� ������ �������
            new Vector2(0, 0), 
            new Vector2(1, 0), 
            new Vector2(1, 1), 
            new Vector2(0, 1)  
        };
        return uvs;
    }
}
