using UnityEngine;
using UnityEngine.AI;

public class VillagerController : MonoBehaviour
{

    public Transform target;
    private NavMeshAgent agent;
    private bool _isFound = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //agent.SetDestination(target.position);
    }

    private void Update()
    {
        if (Vector3.Distance(target.position, transform.position) < 10)
            _isFound = true;
        if (_isFound)
            agent.SetDestination(target.position);
    }
}
