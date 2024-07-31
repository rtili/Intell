using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MeshFigure
{
    public int Grid { set { _gridSize = value; CreateFigure(); } }
    public float Radius { set { _radius = value; CreateFigure(); } }

    private int _gridSize = 3;
    private float _radius = 1;

    /// <summary>
    /// Установка вершин сферы.
    /// </summary>
    /// <returns>Массив вершин.</returns>
    protected override Vector3[] SetVertices()
    {
        // Вычисление количества вершин
        int numVertices = (_gridSize + 1) * (_gridSize + 1);
        // Создание массива вершин
        Vector3[] vertices = new Vector3[numVertices];
        // Индекс текущей вершины
        int index = 0;
        // Цикл по каждой строке сетки
        for (int y = 0; y <= _gridSize; y++)
        {
            // Цикл по каждому столбцу сетки
            for (int x = 0; x <= _gridSize; x++)
            {
                // Вычисление углов в сферических координатах
                float theta = (float)x / _gridSize * Mathf.PI * 2f;
                float phi = (float)y / _gridSize * Mathf.PI;
                phi += Mathf.PI;
                // Вычисление координат вершины в декартовой системе координат
                float xCoord = _radius * Mathf.Sin(phi) * Mathf.Cos(theta);
                float yCoord = _radius * Mathf.Cos(phi);
                float zCoord = _radius * Mathf.Sin(phi) * Mathf.Sin(theta);
                // Создание новой вершины
                vertices[index] = new Vector3(xCoord, yCoord, zCoord);
                // Увеличение индекса
                index++;
            }
        }
        return vertices;
    }

    /// <summary>
    /// Установка треугольников сферы.
    /// </summary>
    /// <returns>Массив индексов треугольников.</returns>
    protected override int[] SetTriangles()
    {
        // Вычисление количества треугольников
        int numTriangles = _gridSize * _gridSize * 6;
        // Создание массива индексов треугольников
        int[] triangles = new int[numTriangles];
        // Индекс текущего треугольника
        int index = 0;
        // Цикл по каждой строке сетки
        for (int y = 0; y < _gridSize; y++)
        {
            // Цикл по каждому столбцу сетки
            for (int x = 0; x < _gridSize; x++)
            {
                // Вычисление индексов вершин для текущей сферы
                int topLeft = y * (_gridSize + 1) + x;
                int topRight = topLeft + 1;
                int bottomLeft = (y + 1) * (_gridSize + 1) + x;
                int bottomRight = bottomLeft + 1;
                // Создание треугольников Верх->Низ части сферы
                triangles[index++] = topLeft;
                triangles[index++] = bottomLeft;
                triangles[index++] = topRight;
                // Создание треугольников Низ->Верх части сферы
                triangles[index++] = topRight;
                triangles[index++] = bottomLeft;
                triangles[index++] = bottomRight;
            }
        }
        return triangles;
    }

    /// <summary>
    /// Установка UV-развертки для сферы.
    /// </summary>
    /// <returns>Массив UV-координат.</returns>
    protected override Vector2[] SetUVs()
    {
        Vector2[] uvs = new Vector2[(_gridSize + 1) * (_gridSize + 1)];
        int index = 0;
        for (int y = 0; y <= _gridSize; y++)
        {
            for (int x = 0; x <= _gridSize; x++)
            {
                float u = (float)x / _gridSize;
                float v = (float)y / _gridSize;
                uvs[index++] = new Vector2(u, v);
            }
        }
        return uvs;
    }
}