using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TerrainAnalyze : MonoBehaviour
{
    NavMeshTriangulation triangulation;
    public Transform agent;
    public struct Vertex
    {
        public List<Vector3> eachVertices;
    };

    List<Vertex> allNavMeshVertices = new List<Vertex>();
    
    List<Mesh> allMeshes = new List<Mesh>();

    private VerticesTriangle verticesTriangle;

    private List<GameObject> createdVertext = new List<GameObject>();
    void Start()
    {
        triangulation = NavMesh.CalculateTriangulation();

        Vector3[] vertices = triangulation.vertices;
        int[] indices = triangulation.indices;

        for (int i = 0; i + 2 < indices.Length; i += 3)
        {
            Vertex v = new Vertex();
            v.eachVertices = new List<Vector3>();

            int a = indices[i];
            int b = indices[i + 1];
            int c = indices[i + 2];

            v.eachVertices.Add(vertices[a]);
            v.eachVertices.Add(vertices[b]);
            v.eachVertices.Add(vertices[c]);

            allNavMeshVertices.Add(v);

            Debug.Log($"Triangle {i/3}: A:{vertices[a]} B:{vertices[b]} C:{vertices[c]}");
        }
        for (int i = 0; i < allNavMeshVertices.Count; i++)
        {
            CreateFilledTriangle(allNavMeshVertices[i].eachVertices[0], allNavMeshVertices[i].eachVertices[1], allNavMeshVertices[i].eachVertices[2], Color.white, i);
        }
        //Debug.Log(allNavMeshVertices.Count);

        for (int i = 0; i < createdVertext.Count; i++) {
            VerticesTriangle triangle = createdVertext[i].GetComponent<VerticesTriangle>();
            for (int j = 0; j < createdVertext.Count; j++)
            {
                VerticesTriangle triangle2 = createdVertext[j].GetComponent<VerticesTriangle>();
                int count = 0;
                for (int k = 0; k < 3; k++)
                {
                    for (int e = 0; e < 3; e++)
                    {
                        if (allNavMeshVertices[i].eachVertices[k] == allNavMeshVertices[j].eachVertices[e])
                        {
                            count++;
                        }
                    }
                }

                if (count == 2)
                {
                    triangle.neighborsTriangle.Add(triangle2.GetComponent<MeshRenderer>());
                }
            }
        }
    }

    private void Update()
    {
        
    }
    
    void CreateTriangle(Vector3 a, Vector3 b, Vector3 c)
    {
        GameObject go = new GameObject("TriangleLines");
        var lr = go.AddComponent<LineRenderer>();

        lr.positionCount = 4;
        lr.loop = false;
        lr.startWidth = 0.02f;
        lr.endWidth = 0.02f;

        lr.useWorldSpace = true;
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = Color.red;
        lr.endColor = Color.red;

        lr.SetPosition(0, a);
        lr.SetPosition(1, b);
        lr.SetPosition(2, c);
        lr.SetPosition(3, a);
    }
    
    void CreateFilledTriangle(Vector3 a, Vector3 b, Vector3 c, Color color, int index)
    {
        GameObject go = new GameObject("Triangle");
        MeshFilter mf = go.AddComponent<MeshFilter>();
        MeshRenderer mr = go.AddComponent<MeshRenderer>();
        VerticesTriangle triangle = go.AddComponent<VerticesTriangle>();
        triangle.agent = agent;
        
        Mesh mesh = new Mesh();
        
        // Set vertices
        mesh.vertices = new Vector3[] { a, b, c };
        
        // Define triangle (0,1,2)
        mesh.triangles = new int[] { 0, 1, 2 };
        
        // Optional: generate normals (for lighting)
        mesh.RecalculateNormals();
        
        mf.mesh = mesh;
        triangle.mesh = mesh;
        // Create a simple unlit color material
        Material mat = new Material(Shader.Find("Unlit/Color"));
        mat.color = color;
        mr.material = mat;
        
        allMeshes.Add(mesh);
        createdVertext.Add(go);
    }


}