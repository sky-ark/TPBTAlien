using System;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public float Distance = 10f;

    public float Angle = 30f;

    public float Height = 1f;
    public Color MeshColor = Color.red;

    private Mesh mesh;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
Mesh CreateWedgeMesh()
{
    Mesh mesh = new Mesh();

    int segments = 10;

    int vertCount = (segments * 4 + 2) * 3;
    Vector3[] vertices = new Vector3[vertCount];
    int[] triangles = new int[vertCount];

    float currentAngle = -Angle;
    float deltaAngle = (Angle * 2f) / segments;

    int vert = 0;

    Vector3 bottomCenter = Vector3.zero;
    Vector3 topCenter = Vector3.up * Height;

    for (int i = 0; i < segments; i++)
    {
        Vector3 bottomLeft = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * Distance;
        Vector3 bottomRight = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * Vector3.forward * Distance;

        Vector3 topLeft = bottomLeft + Vector3.up * Height;
        Vector3 topRight = bottomRight + Vector3.up * Height;

        // Face extérieure
        vertices[vert++] = bottomLeft;
        vertices[vert++] = topLeft;
        vertices[vert++] = topRight;

        vertices[vert++] = topRight;
        vertices[vert++] = bottomRight;
        vertices[vert++] = bottomLeft;

        // Face intérieure (vers le centre)
        vertices[vert++] = bottomCenter;
        vertices[vert++] = bottomRight;
        vertices[vert++] = bottomLeft;

        vertices[vert++] = topCenter;
        vertices[vert++] = topLeft;
        vertices[vert++] = topRight;

        currentAngle += deltaAngle;
    }

    for (int i = 0; i < vertices.Length; i++)
        triangles[i] = i;

    mesh.vertices = vertices;
    mesh.triangles = triangles;
    mesh.RecalculateNormals();
    mesh.RecalculateBounds();

    return mesh;
}

    private void OnValidate()
    {
        mesh = CreateWedgeMesh();
    }

    private void OnDrawGizmos()
    {
        if (mesh)
        {
            Gizmos.color = MeshColor;
            Gizmos.DrawMesh(mesh, transform.position, transform.rotation);
        }
    }
}
