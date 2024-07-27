using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallelepiped : MonoBehaviour
{
    
    public float Widht { set { _widht = value; } }
    public float Height { set { _height = value; } }
    public float Depth { set { _depth = value; } }

    private float _widht = 1, _height = 1, _depth = 1;
    private MeshFilter mf;
    private Mesh mesh;

    void Start()
    {
        mf = GetComponent<MeshFilter>();
        mesh = mf.mesh;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds(); 
    }

    void Update()
    {
        mesh.vertices = SetVerts();
        mesh.triangles = SetTris();
    }

    private Vector3[] SetVerts()
    {
        Vector3[] vert = new Vector3[]
        {
            //front
            new Vector3(0,0,0),
            new Vector3(_widht,0,0),
            new Vector3(0,_height,0),
            new Vector3(_widht,_height,0),
            //back
            new Vector3(0,0,_depth),
            new Vector3(_widht,0,_depth),
            new Vector3(0,_height,_depth),
            new Vector3(_widht,_height,_depth),
            //left
            new Vector3(_depth,0,0),
            new Vector3(_depth,_height,0),
            new Vector3(0,0,0),
            new Vector3(_depth,_height,0),
            //right
            new Vector3(_widht,0,_depth),
            new Vector3(_widht,0,0),
            new Vector3(_widht,_height,0),
            new Vector3(_widht,_height,_depth),
            //bottom
            new Vector3(0,0,0),
            new Vector3(0,0,_depth),
            new Vector3(_widht,0,0),
            new Vector3(_widht,0,_depth),
            //top
            new Vector3(0,_height,0),
            new Vector3(0,_height,_depth),
            new Vector3(_widht,_height,0),
            new Vector3(_widht,_height,_depth)
        };
        return vert;
    }

    private int[] SetTris()
    {
        int[] tris = new int[]
        {
            //front
            0,2,1,
            2,3,1,
            //back
            6,4,5,
            7,6,5,
            //left
            0,4,2,
            6,2,4,
            //right
            1,3,5,
            7,5,3,
            //bottom
            0,1,4,
            5,4,1,
            //top
            2,6,3,
            6,7,3
        };
        return tris;
    }
}
