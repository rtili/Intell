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
                throw new ArgumentException("Количество граней должно быть не менее 3");
            _sides = value;
            UpdateCapsule();
        }
    }
    public float Radius
    {
        set
        {
            if (value <= 0)
                throw new ArgumentException("Радиус должен быть больше 0");
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
    /// Обновление параметров капсулы
    /// </summary>
    private void UpdateCapsule()
    {
        mf.mesh = CreateCapsule();
    }

    /// <summary>
    /// Создаёт сетку капсулы с указанными параметрами.
    /// </summary>
    /// <returns>Созданный меш</returns>
    private Mesh CreateCapsule()
    {
        // Создание нового экземпляра Mesh
        Mesh mesh = new();
        // Определение размеров капсулы на основании радиуса
        Vector3 dimensions = new Vector3(_radius, _radius, _radius);
        // Списки для хранения вершин, индексов треугольников и UV меша капсулы
        List<Vector3> verts = new List<Vector3>();
        List<int> tris = new List<int>();
        List<Vector2> uvs = new List<Vector2>();
        // Определение индекса экваториального меридиана
        int equatorialMeridian = _rings / 2;

        try
        {
            // Перебор всех вертикальных секторов (долгота)
            for (int i = 0; i <= _sides; i++)
            {
                // Вычисление угла долготы в радианах
                float longitude = (Mathf.PI * 2 * i) / _sides;
                // Начальное смещение по вертикали
                float verticalOffset = -_height / 2;
                // Количество дополнительных колец для обработки
                int extraRings = 4;
                int createEquator = extraRings - 1;
                // Перебор всех горизонтальных секторов (широта)
                for (int j = 0; j <= _rings; j++)
                {
                    // Флаг для определения необходимости создания треугольников
                    bool emitTriangles = true;
                    int effectiveJ = j;
                    // Соответствие индекса экваториальному меридиану
                    if (j == equatorialMeridian)
                    {
                        // Обработка дополнительных колец
                        if (createEquator > 0)
                        {
                            // Отключение создания треугольников для некоторых экваториальных колец
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
                    // Получение текущего количества вершин
                    int n = verts.Count;
                    // Вычисление угла широты в радианах
                    float latitude = (Mathf.PI * effectiveJ) / _rings - Mathf.PI / 2;
                    // Вычисление координат вершины в сферических координатах
                    Vector3 sphericalPoint = new Vector3(
                        Mathf.Cos(longitude) *
                            Mathf.Cos(latitude) * dimensions.x,
                        Mathf.Sin(latitude) * dimensions.y + verticalOffset,
                        Mathf.Sin(longitude) *
                            Mathf.Cos(latitude) * dimensions.z);
                    // Добавление вычисленной вершины в список вершин
                    verts.Add(sphericalPoint);
                    // Создание UV-развертки
                    float v = sphericalPoint.y / (dimensions.y * 2 + _height) + 0.5f;
                    Vector2 uvPoint = new Vector2((float)i / _sides, v);
                    uvs.Add(uvPoint);
                    // Создание треугольников, если необходимо
                    if (emitTriangles)
                    {
                        // Создание треугольников для боковых граней капсулы
                        if (i > 0 && j > 0)
                        {
                            // Количество колец с дополнительными кольцами
                            int effectiveRings = _rings + extraRings;
                            // Добавление индексов для первого треугольника
                            tris.Add(n);
                            tris.Add(n - effectiveRings - 1);
                            tris.Add(n - effectiveRings);
                            // Добавление индексов для второго треугольника
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
            // Обработка исключения при нехватке памяти для списков вершин и треугольников
            throw new InvalidOperationException("Не удалось выделить память для списков вершин и треугольников.", ex);
        }
        // Очистка существующих данных в меше
        mesh.Clear();
        // Присвоение вершин, треугольников и UV мешу
        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.uv = uvs.ToArray();
        // Оптимизация и перерасчёт нормалей для улучшения производительности и качества
        mesh.Optimize();
        mesh.RecalculateNormals();
        // Возврат созданного меша
        return mesh;
    }
}
