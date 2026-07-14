using UnityEngine;
using UnityEngine.AI;

public class VillagerController : MonoBehaviour
{
    public GameObject target;
    private NavMeshAgent agent;
    [SerializeField] private BoxCollider atkHB;

    [SerializeField] private HealthSystem _health;
    private StateMachine _sm;

    [SerializeField] private UIManager UIMan;
    private void Awake()
    {
        _sm = new StateMachine();
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        _sm.AddState(new VillagerIdle("VillagerIdle",gameObject, _sm));
        _sm.AddState(new VillagerAttack("VillagerAttack",gameObject, _sm));
        _sm.AddState(new VillagerDeath("VillagerDeath",gameObject, _sm));
        if (!UIMan) Debug.LogError(gameObject.name + " does not have UIManager!");
        if (!atkHB) Debug.LogError(gameObject.name + " does not attack Hitbox!");
        if (!_health)
            _health = GetComponent<HealthSystem>();
        if (!_health)
        {
            Debug.LogError(gameObject.name + "NO HEALTH SYSTEM");
            return;
        }
        _health.setHealth(1); // 1 is for active and 0 is for inactive
    }

    private void Update()
    {
        // External triggers for when Idle state
        // Idle state: check for inRange. If in range Attack
        if (_sm.GetCurrentState() == "VillagerIdle")
        { 
            if (Vector3.Distance(target.transform.position, transform.position) < 11)
            {
                RaycastHit hit;
                Vector3 direction = target.transform.position - transform.position;

                if (Physics.Raycast(transform.position, direction, out hit))
                {
                    if (hit.collider.gameObject == target)
                    {
                        _sm.SetNextState("VillagerAttack");
                        Debug.Log("Player is in sight of " + gameObject.name);
                        UIMan.SetSubtitleText("You sense an unfriendly presence approaching.");
                    }
                    else
                    {
                        Debug.Log("Player is in sight of " + gameObject.name);
                    }
                }
                _sm.SetNextState("VillagerAttack");
            }
        }
        // External triggers for when Attacking State
        // Attack state: Run at player and hit them.
        // Possibly add Chase state
        else if (_sm.GetCurrentState() == "VillagerAttack")
        {
            agent.SetDestination(target.transform.position);
            if (Vector3.Distance(target.transform.position, transform.position) > 15)
            {
                _sm.SetNextState("VillagerIdle");
                UIMan.SetSubtitleText("You no longer sense an unfriendly presence chasing you.");
            }
        }
        atkHB.enabled = (_sm.GetCurrentState() == "VillagerAttack");
        _sm.Update(Time.deltaTime);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("HELLO?");
    //    if (collision.gameObject == target)
    //    {
    //        collision.gameObject.GetComponent<HealthSystem>().takeDamage(5, gameObject.name);
    //    }
    //}
}
