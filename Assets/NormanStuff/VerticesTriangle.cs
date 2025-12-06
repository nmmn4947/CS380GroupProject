using UnityEngine;
using System.Collections.Generic;

public class VerticesTriangle : MonoBehaviour
{
    public Transform agent;
    public Mesh mesh;
    public List<MeshRenderer> neighborsTriangle = new List<MeshRenderer>();
    MeshFilter meshF;
    MeshRenderer meshR;
    Material mat;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        meshF  = gameObject.GetComponent<MeshFilter>();
        //mesh = meshF.GetComponent<Mesh>();
        meshR = gameObject.GetComponent<MeshRenderer>();
        //mat = meshR.GetComponent<Material>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PointInTriangle(agent.transform.position, mesh.vertices[0], mesh.vertices[1], mesh.vertices[2]))
        {
            meshR.material.color = Color.red;
        }
        else
        {
            meshR.material.color = Color.white;
        }

        for (int i = 0; i < neighborsTriangle.Count; i++)
        {
            if (neighborsTriangle[i].material.color == Color.red)
            {
                meshR.material.color = Color.yellow;
            }
        }
    }
    
    bool PointInTriangle(Vector3 p, Vector3 a, Vector3 b, Vector3 c)
    {
        // Convert to 2D if triangle is horizontal (NavMesh)
        Vector2 p2 = new Vector2(p.x, p.z);
        Vector2 a2 = new Vector2(a.x, a.z);
        Vector2 b2 = new Vector2(b.x, b.z);
        Vector2 c2 = new Vector2(c.x, c.z);

        // Compute barycentric coordinates
        float denominator = ((b2.y - c2.y) * (a2.x - c2.x) + (c2.x - b2.x) * (a2.y - c2.y));
        float w1 = ((b2.y - c2.y) * (p2.x - c2.x) + (c2.x - b2.x) * (p2.y - c2.y)) / denominator;
        float w2 = ((c2.y - a2.y) * (p2.x - c2.x) + (a2.x - c2.x) * (p2.y - c2.y)) / denominator;
        float w3 = 1 - w1 - w2;

        return w1 >= 0 && w2 >= 0 && w3 >= 0;
    }

}
