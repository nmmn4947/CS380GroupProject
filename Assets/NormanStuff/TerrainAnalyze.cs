using UnityEngine;
using UnityEngine.AI;

public class TerrainAnalyze : MonoBehaviour
{
    NavMeshTriangulation triangulation;
    public NavMeshData nmd;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NavMesh.CalculateTriangulation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
