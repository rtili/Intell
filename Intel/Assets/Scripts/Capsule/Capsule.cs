using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class Capsule : MonoBehaviour
{
    public int Sides
    {
        set
        {
            if (value < 3)
                throw new ArgumentException("���������� ������ ������ ���� �� ����� 3");
            _sides = value;
            UpdateCapsule();
        }
    }
    public float Radius
    {
        set
        {
            if (value <= 0)
                throw new ArgumentException("������ ������ ���� ������ 0");
            _radius = value;
            UpdateCapsule();
        }
    }
    public float Height { set { _height = value; UpdateCapsule(); } }

    private float _radius = 1f;
    private float _height = 2f;
    private int _sides = 32;
    private int _rings = 16;

    private MeshFilter mf;

    private void Start()
    {
        mf = GetComponent<MeshFilter>();
        UpdateCapsule();
    }

    /// <summary>
    /// ���������� ���������� �������
    /// </summary>
    private void UpdateCapsule()
    {
        mf.mesh = CreateCapsule();
    }

    /// <summary>
    /// ������ ����� ������� � ���������� �����������.
    /// </summary>
    /// <returns>��������� ���</returns>
    private Mesh CreateCapsule()
    {
        // �������� ������ ���������� Mesh
        Mesh mesh = new();
        // ����������� �������� ������� �� ��������� �������
        Vector3 dimensions = new Vector3(_radius, _radius, _radius);
        // ������ ��� �������� ������, �������� ������������� � UV ���� �������
        List<Vector3> verts = new List<Vector3>();
        List<int> tris = new List<int>();
        List<Vector2> uvs = new List<Vector2>();
        // ����������� ������� ��������������� ���������
        int equatorialMeridian = _rings / 2;

        try
        {
            // ������� ���� ������������ �������� (�������)
            for (int i = 0; i <= _sides; i++)
            {
                // ���������� ���� ������� � ��������
                float longitude = (Mathf.PI * 2 * i) / _sides;
                // ��������� �������� �� ���������
                float verticalOffset = -_height / 2;
                // ���������� �������������� ����� ��� ���������
                int extraRings = 4;
                int createEquator = extraRings - 1;
                // ������� ���� �������������� �������� (������)
                for (int j = 0; j <= _rings; j++)
                {
                    // ���� ��� ����������� ������������� �������� �������������
                    bool emitTriangles = true;
                    int effectiveJ = j;
                    // ������������ ������� ��������������� ���������
                    if (j == equatorialMeridian)
                    {
                        // ��������� �������������� �����
                        if (createEquator > 0)
                        {
                            // ���������� �������� ������������� ��� ��������� �������������� �����
                            if (createEquator == 2)
                                emitTriangles = false;
                            if (createEquator == 1)
                                verticalOffset = -verticalOffset;
                            createEquator--;
                            j--;
                        }
                        else
                            emitTriangles = false;
                    }
                    // ��������� �������� ���������� ������
                    int n = verts.Count;
                    // ���������� ���� ������ � ��������
                    float latitude = (Mathf.PI * effectiveJ) / _rings - Mathf.PI / 2;
                    // ���������� ��������� ������� � ����������� �����������
                    Vector3 sphericalPoint = new Vector3(
                        Mathf.Cos(longitude) *
                            Mathf.Cos(latitude) * dimensions.x,
                        Mathf.Sin(latitude) * dimensions.y + verticalOffset,
                        Mathf.Sin(longitude) *
                            Mathf.Cos(latitude) * dimensions.z);
                    // ���������� ����������� ������� � ������ ������
                    verts.Add(sphericalPoint);
                    // �������� UV-���������
                    float v = sphericalPoint.y / (dimensions.y * 2 + _height) + 0.5f;
                    Vector2 uvPoint = new Vector2((float)i / _sides, v);
                    uvs.Add(uvPoint);
                    // �������� �������������, ���� ����������
                    if (emitTriangles)
                    {
                        // �������� ������������� ��� ������� ������ �������
                        if (i > 0 && j > 0)
                        {
                            // ���������� ����� � ��������������� ��������
                            int effectiveRings = _rings + extraRings;
                            // ���������� �������� ��� ������� ������������
                            tris.Add(n);
                            tris.Add(n - effectiveRings - 1);
                            tris.Add(n - effectiveRings);
                            // ���������� �������� ��� ������� ������������
                            tris.Add(n);
                            tris.Add(n - 1);
                            tris.Add(n - effectiveRings - 1);
                        }
                    }
                }
            }
        }
        catch (OutOfMemoryException ex)
        {
            // ��������� ���������� ��� �������� ������ ��� ������� ������ � �������������
            throw new InvalidOperationException("�� ������� �������� ������ ��� ������� ������ � �������������.", ex);
        }
        // ������� ������������ ������ � ����
        mesh.Clear();
        // ���������� ������, ������������� � UV ����
        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.uv = uvs.ToArray();
        // ����������� � ���������� �������� ��� ��������� ������������������ � ��������
        mesh.Optimize();
        mesh.RecalculateNormals();
        // ������� ���������� ����
        return mesh;
    }
}
