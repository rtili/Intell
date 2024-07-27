using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsule : MonoBehaviour
{
    public int Sides { set { _sides = value; } }
    public float Radius { set { _radius = value; } }
    public float Height { set { _height = value; } }

    private float _radius = 1f;
    private float _height = 2f;
    private int _sides = 32;
    private int rings = 16;
    private MeshFilter mf;

    private void Start()
    {
        mf = GetComponent<MeshFilter>();
        mf.mesh = CreateCapsuleMesh();
    }

    private void Update()
    {
        mf.mesh = CreateCapsuleMesh();
    }

    private Mesh CreateCapsuleMesh()
    {
        Mesh mesh = new ();
        Mesh cylinderMesh = CreateCylinderMesh(_radius, _height, _sides, rings);
        Mesh topSphereMesh = CreateSphereMesh(_radius, _sides, rings);
        Mesh bottomSphereMesh = CreateSphereMesh(_radius, _sides, rings);

        CombineInstance[] combineInstances = new CombineInstance[3];
        combineInstances[0].mesh = cylinderMesh;
        combineInstances[0].transform = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one);
        combineInstances[1].mesh = topSphereMesh;
        combineInstances[1].transform = Matrix4x4.TRS(new Vector3(0, _height / 2, 0), Quaternion.identity, Vector3.one);
        combineInstances[2].mesh = bottomSphereMesh;
        combineInstances[2].transform = Matrix4x4.TRS(new Vector3(0, -_height / 2, 0), Quaternion.identity, Vector3.one);

        mesh.CombineMeshes(combineInstances, true, true);
        return mesh;
    }

    private Mesh CreateCylinderMesh(float _radius, float _height, int _sides, int rings)
    {
        Mesh mesh = new ();
        Vector3[] vertices = new Vector3[(_sides + 1) * (rings + 1)];
        int vertexIndex = 0;

        for (int r = 0; r <= rings; r++)
        {
            for (int s = 0; s <= _sides; s++)
            {
                float angle = Mathf.Deg2Rad * s * 360f / _sides;
                float x = _radius * Mathf.Cos(angle);
                float z = _radius * Mathf.Sin(angle);
                float y = (float)r / rings * _height - _height / 2;
                vertices[vertexIndex++] = new Vector3(x, y, z);
            }
        }

        int[] triangles = new int[_sides * rings * 6];
        int triangleIndex = 0;

        for (int r = 0; r < rings; r++)
        {
            for (int s = 0; s < _sides; s++)
            {
                int topLeft = (r + 1) * (_sides + 1) + s;
                int topRight = (r + 1) * (_sides + 1) + (s + 1);
                int bottomLeft = r * (_sides + 1) + s;
                int bottomRight = r * (_sides + 1) + (s + 1);

                //first tri
                triangles[triangleIndex++] = bottomLeft;
                triangles[triangleIndex++] = topLeft;
                triangles[triangleIndex++] = topRight;

                //second tri
                triangles[triangleIndex++] = bottomLeft;
                triangles[triangleIndex++] = topRight;
                triangles[triangleIndex++] = bottomRight;
            }
        }
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        return mesh;
    }

    private Mesh CreateSphereMesh(float _radius, int _sides, int rings)
    {
        Mesh mesh = new ();
        Vector3[] vertices = new Vector3[(_sides + 1) * (rings + 1)];
        int vertexIndex = 0;

        for (int r = 0; r <= rings; r++)
        {
            for (int s = 0; s <= _sides; s++)
            {
                float angle = Mathf.Deg2Rad * s * 360f / _sides;
                float x = _radius * Mathf.Cos(angle) * Mathf.Sin((float)r / rings * Mathf.PI);
                float z = _radius * Mathf.Sin(angle) * Mathf.Sin((float)r / rings * Mathf.PI);
                float y = _radius * Mathf.Cos((float)r / rings * Mathf.PI);
                vertices[vertexIndex++] = new Vector3(x, y, z);
            }
        }
        int[] triangles = new int[_sides * rings * 6];
        int triangleIndex = 0;

        for (int r = 0; r < rings; r++)
        {
            for (int s = 0; s < _sides; s++)
            {
                int topLeft = (r + 1) * (_sides + 1) + s;
                int topRight = (r + 1) * (_sides + 1) + (s + 1);
                int bottomLeft = r * (_sides + 1) + s;
                int bottomRight = r * (_sides + 1) + (s + 1);

                //first tri
                triangles[triangleIndex++] = topRight;
                triangles[triangleIndex++] = topLeft;
                triangles[triangleIndex++] = bottomLeft;

                //second tri
                triangles[triangleIndex++] = bottomRight;
                triangles[triangleIndex++] = topRight;
                triangles[triangleIndex++] = bottomLeft;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        return mesh;
    }
}
