using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public abstract class MeshFigure : MonoBehaviour
{
    private Mesh mesh;
    
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        CreateFigure();
    }

    /// <summary>
    /// ���������� ���� ������ ��������� �����������.
    /// </summary>
    protected void CreateFigure()
    {
        mesh.Clear();
        try
        {
            // ��������� ������, ������������� � UV-���������
            mesh.vertices = SetVertices();
            mesh.triangles = SetTriangles();
            mesh.uv = SetUVs();
        }
        catch (OutOfMemoryException ex)
        {
            // ��������� ���������� ��� �������� ������ ��� ��������
            throw new InvalidOperationException("�� ������� �������� ������ ��� ������, ������������� ��� UV.", ex);
        }
        mesh.Optimize();
        mesh.RecalculateNormals();
    }
    /// <summary>
    /// ��������� ������ ������.
    /// </summary>
    /// <returns>������ ������.</returns>
    protected abstract Vector3[] SetVertices();
    /// <summary>
    /// ��������� ������������� ������.
    /// </summary>
    /// <returns>������ �������� �������������.</returns>
    protected abstract int[] SetTriangles();
    /// <summary>
    /// ��������� UV-��������� ������.
    /// </summary>
    /// <returns>������ UV-���������.</returns>
    protected abstract Vector2[] SetUVs();
}
