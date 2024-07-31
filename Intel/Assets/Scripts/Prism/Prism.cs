using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prism : MeshFigure
{
    public int Sides
    { 
        set 
        {
            if (value < 3)            
                throw new ArgumentException("���������� ������ ������ ���� �� ����� 3");            
            _sides = value; 
            CreateFigure();
        } 
    }
    public float Radius
    { 
        set 
        {
            if (value <= 0)            
                throw new ArgumentException("������ ������ ���� ������ 0");            
            _radius = value; 
            CreateFigure();
        } 
    }
    public float Height { set { _height = value; CreateFigure(); } }

    private int _sides = 6;
    private float _radius = 1f; 
    private float _height = 2f;

    /// <summary>
    /// ��������� ������ ������.
    /// </summary>
    /// <returns>������ ������.</returns>
    protected override Vector3[] SetVertices()
    {
        // ���������� ���� ����� ��������� � ��������
        float angle = 360f / _sides;
        // ����������� ���������� ������
        int vertexCount = (_sides + 1) * 2;
        Vector3[] vertices = new Vector3[vertexCount];
        
        for (int i = 0; i <= _sides; i++)
        {
            // ���������� ���� � �������� ��� ������� �������
            float radianAngle = Mathf.Deg2Rad * i * angle;
            // ���������� ��������� X � Z �� ��������� �������
            float x = _radius * Mathf.Cos(radianAngle);
            float z = _radius * Mathf.Sin(radianAngle);
            // �������� ������ �������
            vertices[i] = new Vector3(x, 0, z);
            // �������� ������� �������
            vertices[i + _sides + 1] = new Vector3(x, _height, z);
        }
        return vertices;
    }

    /// <summary>
    /// ��������� ������������� ������.
    /// </summary>
    /// <returns>������ �������� �������������.</returns>
    protected override int[] SetTriangles()
    {
        // ���������� ���������� �������������, ����������� ��� ������������� ������
        int triangleCount = _sides * 12;
        int[] triangles = new int[triangleCount];
        int triangleIndex = 0;
        // �������� ������������� ��� ������ ������� ������
        for (int i = 0; i < _sides; i++)
        {
            // ������������ ��� ������� ������
            // ������� ������������ ��������� ������
            triangles[triangleIndex++] = i;
            triangles[triangleIndex++] = i + _sides + 1;
            triangles[triangleIndex++] = (i + 1) % _sides + _sides + 1;
            // ������ ������������ ������� ������
            triangles[triangleIndex++] = i;
            triangles[triangleIndex++] = (i + 1) % _sides + _sides + 1;
            triangles[triangleIndex++] = (i + 1) % _sides;
            // ������������ ��� �����
            triangles[triangleIndex++] = _sides + 1;
            triangles[triangleIndex++] = _sides + 1 + ((i + 1) % _sides);
            triangles[triangleIndex++] = _sides + 1 + i;
            // ������������ ��� ����
            triangles[triangleIndex++] = i;
            triangles[triangleIndex++] = (i + 1) % _sides;
            triangles[triangleIndex++] = _sides;
        }       
        return triangles;
    }

    /// <summary>
    /// ��������� UV-��������� ��� ������.
    /// </summary>
    /// <returns>������ UV-���������.</returns>
    protected override Vector2[] SetUVs()
    {
        Vector2[] uvs = new Vector2[(_sides + 1) * 2];
        float heightStep = 1f; // ������ ��� UV �� ������� � ������ ������

        // UV ��� ������� ������
        for (int i = 0; i <= _sides; i++)
        {
            float u = (float)i / _sides;
            float v = 0; // ������ �����
            uvs[i] = new Vector2(u, v);

            v = heightStep; // ������� �����
            uvs[i + _sides + 1] = new Vector2(u, v);
        }

        // UV ��� ������� ������
        for (int i = 0; i < _sides; i++)
        {
            float u0 = (float)i / _sides;
            float u1 = (float)(i + 1) / _sides;

            // ������� �����
            uvs[i] = new Vector2(u0, 0); // ������ �����
            uvs[i + _sides] = new Vector2(u0, heightStep); // ������� �����

            uvs[(i + 1) % _sides] = new Vector2(u1, 0); // ������ �����
            uvs[(i + 1) % _sides + _sides] = new Vector2(u1, heightStep); // ������� �����
        }

        return uvs;
    }
}