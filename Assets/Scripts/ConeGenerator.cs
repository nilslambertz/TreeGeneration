using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class ConeGenerator : MonoBehaviour
{
    private Mesh mesh;
    
    private Vector3[] vertices;
    private int[] triangles;

    private float bottomRadius = 0.5f;
    private float topRadius = 0.5f;
    private float height = 20;
    private int circlePoints = 20;
    
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        createCone();
        updateCone();
    }

    private void createCone()
    {
        mesh = createCircle(bottomRadius, height);

        vertices = mesh.vertices;
        triangles = mesh.triangles;
    }

    private Mesh createCircle(float radius, float height)
    {
        Mesh circleMesh = new Mesh();
        
        vertices = new Vector3[360 / circlePoints + 1];
        vertices[0] = new Vector3(0, height, 0);

        for (int i = 1; i <= vertices.Length / 2; i++)
        {
            float currentAngle = i * circlePoints * Mathf.Deg2Rad;
            float currentX = radius * Mathf.Cos(currentAngle);
            float currentZ = radius * Mathf.Sin(currentAngle);
            vertices[i] = new Vector3(currentX, 0, currentZ);
        }

        triangles = new int[(vertices.Length - 1) * 3];
        for (int i = 0; i <= triangles.Length - 3; i = i + 3)
        {
            triangles[i] = 0;
            triangles[i + 1] = i / 3 + 1;
            triangles[i + 2] = (i + 2 == (triangles.Length/2) - 1) ? 1 : i / 3 + 2;
        }

        return circleMesh;
    }

    private void updateCone()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        
        mesh.RecalculateNormals();
    }
}
