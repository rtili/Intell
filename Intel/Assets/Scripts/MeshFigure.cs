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
    /// Обновление меша фигуры заданными параметрами.
    /// </summary>
    protected void CreateFigure()
    {
        mesh.Clear();
        try
        {
            // Установка вершин, треугольников и UV-развертки
            mesh.vertices = SetVertices();
            mesh.triangles = SetTriangles();
            mesh.uv = SetUVs();
        }
        catch (OutOfMemoryException ex)
        {
            // Обработка исключения при нехватке памяти для массивов
            throw new InvalidOperationException("Не удалось выделить память для вершин, треугольников или UV.", ex);
        }
        mesh.Optimize();
        mesh.RecalculateNormals();
    }
    /// <summary>
    /// Установка вершин фигуры.
    /// </summary>
    /// <returns>Массив вершин.</returns>
    protected abstract Vector3[] SetVertices();
    /// <summary>
    /// Установка треугольников фигуры.
    /// </summary>
    /// <returns>Массив индексов треугольников.</returns>
    protected abstract int[] SetTriangles();
    /// <summary>
    /// Установка UV-развертки фигуры.
    /// </summary>
    /// <returns>Массив UV-координат.</returns>
    protected abstract Vector2[] SetUVs();
}
