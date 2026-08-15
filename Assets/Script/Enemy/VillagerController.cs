using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder.MeshOperations;

public class VillagerController : MonoBehaviour
{
    public GameObject target;
    private NavMeshAgent agent;
    [SerializeField] private BoxCollider atkHB;

    [SerializeField] private HealthSystem _health;
    private StateMachine _sm;

    [SerializeField] private UIManager UIMan;

    private OutlineObjects _outliner;

    private bool _isDead = false;

    private void Awake()
    {
        _sm = new StateMachine();
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        _sm.AddState(new VillagerIdle("VillagerIdle",gameObject, _sm));
        _sm.AddState(new VillagerChase("VillagerChase", gameObject, _sm));
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

        _outliner = GameObject.FindFirstObjectByType<OutlineObjects>();
        if (!_outliner) Debug.LogError("Outline Objects missing in " + name);

        _health.setHealth(1,false); // 1 is for active and 0 is for inactive
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

                if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity, LayerMask.GetMask("Body")))
                {
                    if (hit.collider.gameObject == target)
                    {
                        _sm.SetNextState("VillagerChase");
                        Debug.Log("Player is in sight of " + gameObject.name);
                        UIMan.SetSubtitleText("You sense a hostile presence approaching.");
                    }
                    else
                    {
                        Debug.Log("Player is in sight of " + gameObject.name);
                    }
                }
                //_sm.SetNextState("VillagerAttack");
            }
        }
        // External triggers for when Chasing State
        // Chasing State: Run towards the body.
        else if (_sm.GetCurrentState() == "VillagerChase")
        {
            agent.SetDestination(target.transform.position);
            if (Vector3.Distance(target.transform.position, transform.position) > 15)
            {
                _sm.SetNextState("VillagerIdle");
                UIMan.SetSubtitleText("You no longer sense a hostile presence chasing you.");
            }
            else if (Vector3.Distance(target.transform.position, transform.position) <= 3)
            {
                _sm.SetNextState("VillagerAttack");
            }
        }
        // External triggers for when Attacking State
        // Attack state: Run at player and hit them.
        else if (_sm.GetCurrentState() == "VillagerAttack")
        {
            if (Vector3.Distance(target.transform.position, transform.position) > 3)
            {
                _sm.SetNextState("VillagerChase");
            }
        }
        else if (_sm.GetCurrentState() == "VillagerDeath" && !_isDead)
        {
            // Prevent sending the same message A LOT of times
            _isDead = true;
            _outliner.HostileDown(gameObject);
        }
        _sm.Update(Time.deltaTime);
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        // In case it didn't remove before
        _outliner.HostileDown(gameObject);
    }
}
