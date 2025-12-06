using UnityEngine;
using UnityEngine.AI;

public class MoveRandom : MonoBehaviour
{
    //[SerializeField] Transform target;
    NavMeshAgent agent;

    [SerializeField] private float radius;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //agent.SetDestination(target.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.hasPath || agent.pathStatus == NavMeshPathStatus.PathInvalid)
        {
            Vector3 randomPos = Random.insideUnitSphere * radius;
            randomPos += transform.position;
            agent.SetDestination(randomPos);
        }
    }
}
