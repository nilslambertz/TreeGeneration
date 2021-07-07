using UnityEngine;

public class ConeGenerator : MonoBehaviour {
    private const float doublePi = Mathf.PI * 2f;
    private readonly int numberOfSides = 50;
    private int[] triangles;
    private Vector3[] vertices;

    public GameObject getCone(float bottomRadius, float topRadius, float height, Vector3 position,
        Quaternion rotation) {
        var gameObject = new GameObject();

        var meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = new Material(Shader.Find("Standard"));

        var filter = gameObject.AddComponent<MeshFilter>();
        var mesh = filter.mesh;
        mesh.Clear();

        /* Vertices */

        var nbVerticesCap = numberOfSides + 1;

        // bottom + top + sides
        vertices = new Vector3[(numberOfSides + 1) * 4];
        var vert = 0;

        // Bottom cap
        vertices[vert++] = new Vector3(0f, 0f, 0f);
        while (++vert <= numberOfSides) {
            var rad = (float) vert / numberOfSides * doublePi;
            vertices[vert] = new Vector3(Mathf.Cos(rad) * bottomRadius, 0f, Mathf.Sin(rad) * bottomRadius);
        }

        // Top cap
        vertices[vert] = new Vector3(0f, height, 0f);
        while (++vert <= numberOfSides * 2 + 1) {
            var rad = (float) (vert - numberOfSides - 1) / numberOfSides * doublePi;
            vertices[vert] = new Vector3(Mathf.Cos(rad) * topRadius, height, Mathf.Sin(rad) * topRadius);
        }

        // Sides
        var v = 0;
        while (vert <= vertices.Length - 4) {
            var rad = (float) v / numberOfSides * doublePi;
            vertices[vert] = new Vector3(Mathf.Cos(rad) * topRadius, height, Mathf.Sin(rad) * topRadius);
            vertices[vert + 1] = new Vector3(Mathf.Cos(rad) * bottomRadius, 0, Mathf.Sin(rad) * bottomRadius);
            vert += 2;
            v++;
        }

        vertices[vert] = vertices[numberOfSides * 2 + 2];
        vertices[vert + 1] = vertices[numberOfSides * 2 + 3];

        /* Normals */

        // bottom + top + sides
        var normales = new Vector3[vertices.Length];
        vert = 0;

        // Bottom cap
        while (vert <= numberOfSides) normales[vert++] = Vector3.down;

        // Top cap
        while (vert <= numberOfSides * 2 + 1) normales[vert++] = Vector3.up;

        // Sides
        v = 0;
        while (vert <= vertices.Length - 4) {
            var rad = (float) v / numberOfSides * doublePi;
            var cos = Mathf.Cos(rad);
            var sin = Mathf.Sin(rad);

            normales[vert] = new Vector3(cos, 0f, sin);
            normales[vert + 1] = normales[vert];

            vert += 2;
            v++;
        }

        normales[vert] = normales[numberOfSides * 2 + 2];
        normales[vert + 1] = normales[numberOfSides * 2 + 3];

        var uvs = new Vector2[vertices.Length];

        // Bottom cap
        var u = 0;
        uvs[u++] = new Vector2(0.5f, 0.5f);
        while (u <= numberOfSides) {
            var rad = (float) u / numberOfSides * doublePi;
            uvs[u] = new Vector2(Mathf.Cos(rad) * .5f + .5f, Mathf.Sin(rad) * .5f + .5f);
            u++;
        }

        // Top cap
        uvs[u++] = new Vector2(0.5f, 0.5f);
        while (u <= numberOfSides * 2 + 1) {
            var rad = (float) u / numberOfSides * doublePi;
            uvs[u] = new Vector2(Mathf.Cos(rad) * .5f + .5f, Mathf.Sin(rad) * .5f + .5f);
            u++;
        }

        // Sides
        var u_sides = 0;
        while (u <= uvs.Length - 4) {
            var t = (float) u_sides / numberOfSides;
            uvs[u] = new Vector3(t, 1f);
            uvs[u + 1] = new Vector3(t, 0f);
            u += 2;
            u_sides++;
        }

        uvs[u] = new Vector2(1f, 1f);
        uvs[u + 1] = new Vector2(1f, 0f);

        /* Triangles */

        var nbTriangles = numberOfSides + numberOfSides + numberOfSides * 2;
        triangles = new int[nbTriangles * 3 + 3];

        // Bottom cap
        var tri = 0;
        var i = 0;
        while (tri < numberOfSides - 1) {
            triangles[i] = 0;
            triangles[i + 1] = tri + 1;
            triangles[i + 2] = tri + 2;
            tri++;
            i += 3;
        }

        triangles[i] = 0;
        triangles[i + 1] = tri + 1;
        triangles[i + 2] = 1;
        tri++;
        i += 3;

        // Top cap
        //tri++;
        while (tri < numberOfSides * 2) {
            triangles[i] = tri + 2;
            triangles[i + 1] = tri + 1;
            triangles[i + 2] = nbVerticesCap;
            tri++;
            i += 3;
        }

        triangles[i] = nbVerticesCap + 1;
        triangles[i + 1] = tri + 1;
        triangles[i + 2] = nbVerticesCap;
        tri++;
        i += 3;
        tri++;

        // Sides
        while (tri <= nbTriangles) {
            triangles[i] = tri + 2;
            triangles[i + 1] = tri + 1;
            triangles[i + 2] = tri + 0;
            tri++;
            i += 3;

            triangles[i] = tri + 1;
            triangles[i + 1] = tri + 2;
            triangles[i + 2] = tri + 0;
            tri++;
            i += 3;
        }

        mesh.vertices = vertices;
        mesh.normals = normales;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        mesh.RecalculateBounds();
        mesh.Optimize();

        filter.mesh = mesh;

        gameObject.transform.position = position;
        gameObject.transform.rotation = rotation;

        return gameObject;
    }
}