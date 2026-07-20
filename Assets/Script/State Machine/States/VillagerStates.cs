using UnityEngine;
using UnityEngine.InputSystem.Android;

// Waiting for player presence
public class VillagerIdle : State
{
    private GameObject m_go;
    private HealthSystem _healthSystem;
    private StateMachine _sm;
    public VillagerIdle(string stateID, GameObject go, StateMachine sm) : base(stateID)
    {
        m_go = go;
        _healthSystem = m_go.GetComponent<HealthSystem>();
        _sm = sm;
    }

    public override void Enter()
    {
        Debug.Log("Entering Villager Idle");
    }

    public override void Update(double dt)
    {
        if (_healthSystem.getHealth() < 1)
        {
            _sm.SetNextState("VillagerDeath");
            return;
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting Villager Idle");
    }
}

// Pounce onto Player
public class VillagerAttack : State
{
    private GameObject m_go;
    private HealthSystem _healthSystem;
    private StateMachine _sm;
    public VillagerAttack(string stateID, GameObject go, StateMachine sm) : base(stateID)
    {
        m_go = go;
        _healthSystem = m_go.GetComponent<HealthSystem>();
        _sm = sm;
    }

    public override void Enter()
    {
        Debug.Log("Entering Villager Attack");
    }

    public override void Update(double dt)
    {
        if (_healthSystem.getHealth() < 1)
        {
            _sm.SetNextState("VillagerDeath");
            return;
        }

    }

    public override void Exit()
    {
        Debug.Log("Exitting Villager Attack");
    }
}

// Dead
public class VillagerDeath : State
{
    private GameObject m_go;
    private HealthSystem _healthSystem;
    private StateMachine _sm;
    private UIManager UIMan;
    public VillagerDeath(string stateID, GameObject go, StateMachine sm) : base(stateID)
    {
        m_go = go;
        _healthSystem = m_go.GetComponent<HealthSystem>();
        _sm = sm;
        UIMan = GameObject.FindFirstObjectByType<UIManager>();
    }

    public override void Enter()
    {
        Debug.Log("Entering Villager Attack");
        UIMan.SetSubtitleText("Your blade hit a Soft target.");
        m_go.SetActive(false);
    }

    public override void Update(double dt)
    {

    }

    public override void Exit()
    {
        Debug.Log("Exiting Villager Attack");
    }
}

