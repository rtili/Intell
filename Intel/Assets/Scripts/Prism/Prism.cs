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
                throw new ArgumentException("Количество граней должно быть не менее 3");            
            _sides = value; 
            CreateFigure();
        } 
    }
    public float Radius
    { 
        set 
        {
            if (value <= 0)            
                throw new ArgumentException("Радиус должен быть больше 0");            
            _radius = value; 
            CreateFigure();
        } 
    }
    public float Height { set { _height = value; CreateFigure(); } }

    private int _sides = 6;
    private float _radius = 1f; 
    private float _height = 2f;

    /// <summary>
    /// Установка вершин призмы.
    /// </summary>
    /// <returns>Массив вершин.</returns>
    protected override Vector3[] SetVertices()
    {
        // Вычисление угла между вершинами в градусах
        float angle = 360f / _sides;
        // Определение количества вершин
        int vertexCount = (_sides + 1) * 2;
        Vector3[] vertices = new Vector3[vertexCount];
        
        for (int i = 0; i <= _sides; i++)
        {
            // Вычисление угла в радианах для текущей вершины
            float radianAngle = Mathf.Deg2Rad * i * angle;
            // Вычисление координат X и Z на основании радиана
            float x = _radius * Mathf.Cos(radianAngle);
            float z = _radius * Mathf.Sin(radianAngle);
            // Создание нижней вершины
            vertices[i] = new Vector3(x, 0, z);
            // Создание верхней вершины
            vertices[i + _sides + 1] = new Vector3(x, _height, z);
        }
        return vertices;
    }

    /// <summary>
    /// Установка треугольников призмы.
    /// </summary>
    /// <returns>Массив индексов треугольников.</returns>
    protected override int[] SetTriangles()
    {
        // Вычисление количества треугольников, необходимых для представления призмы
        int triangleCount = _sides * 12;
        int[] triangles = new int[triangleCount];
        int triangleIndex = 0;
        // Создание треугольников для каждой стороны призмы
        for (int i = 0; i < _sides; i++)
        {
            // Треугольники для боковых граней
            // Верхние треугольники бокововых граней
            triangles[triangleIndex++] = i;
            triangles[triangleIndex++] = i + _sides + 1;
            triangles[triangleIndex++] = (i + 1) % _sides + _sides + 1;
            // Нижние треугольники боковых граней
            triangles[triangleIndex++] = i;
            triangles[triangleIndex++] = (i + 1) % _sides + _sides + 1;
            triangles[triangleIndex++] = (i + 1) % _sides;
            // Треугольники для верха
            triangles[triangleIndex++] = _sides + 1;
            triangles[triangleIndex++] = _sides + 1 + ((i + 1) % _sides);
            triangles[triangleIndex++] = _sides + 1 + i;
            // Треугольники для низа
            triangles[triangleIndex++] = i;
            triangles[triangleIndex++] = (i + 1) % _sides;
            triangles[triangleIndex++] = _sides;
        }       
        return triangles;
    }

    /// <summary>
    /// Установка UV-развертки для призмы.
    /// </summary>
    /// <returns>Массив UV-координат.</returns>
    protected override Vector2[] SetUVs()
    {
        Vector2[] uvs = new Vector2[(_sides + 1) * 2];
        float heightStep = 1f; // Высота для UV на верхней и нижней гранях

        // UV для боковых граней
        for (int i = 0; i <= _sides; i++)
        {
            float u = (float)i / _sides;
            float v = 0; // Нижняя грань
            uvs[i] = new Vector2(u, v);

            v = heightStep; // Верхняя грань
            uvs[i + _sides + 1] = new Vector2(u, v);
        }

        // UV для боковых граней
        for (int i = 0; i < _sides; i++)
        {
            float u0 = (float)i / _sides;
            float u1 = (float)(i + 1) / _sides;

            // Боковые грани
            uvs[i] = new Vector2(u0, 0); // Нижняя грань
            uvs[i + _sides] = new Vector2(u0, heightStep); // Верхняя грань

            uvs[(i + 1) % _sides] = new Vector2(u1, 0); // Нижняя грань
            uvs[(i + 1) % _sides + _sides] = new Vector2(u1, heightStep); // Верхняя грань
        }

        return uvs;
    }
}