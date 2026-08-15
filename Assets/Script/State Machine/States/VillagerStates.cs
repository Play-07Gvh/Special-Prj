using UnityEngine;

// Waiting for player presence
public class VillagerIdle : State
{
    private GameObject m_go;
    private HealthSystem _healthSystem;
    private StateMachine _sm;
    private Animator _animator;
    public VillagerIdle(string stateID, GameObject go, StateMachine sm) : base(stateID)
    {
        m_go = go;
        _healthSystem = m_go.GetComponent<HealthSystem>();
        _sm = sm;
        _animator = m_go.GetComponent<Animator>();
    }

    public override void Enter()
    {
        //Debug.Log("Entering Villager Idle");
        _animator.SetBool("isIdle", true);
        _animator.SetBool("isChase", false);
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
        //Debug.Log("Exiting Villager Idle");
    }
}

public class VillagerChase : State
{
    private GameObject m_go;
    private HealthSystem _healthSystem;
    private StateMachine _sm;
    private Animator _animator;
    public VillagerChase(string stateID, GameObject go, StateMachine sm) : base(stateID)
    {
        m_go = go;
        _healthSystem = m_go.GetComponent<HealthSystem>();
        _sm = sm;
        _animator = m_go.GetComponent<Animator>();
    }

    public override void Enter()
    {
        //Debug.Log("Entering Villager Chase");
        _animator.SetBool("isChase", true);
        _animator.SetBool("isIdle", false);
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
        //Debug.Log("Exiting Villager Chase");
    }
}


// Attacking Player
public class VillagerAttack : State
{
    private GameObject m_go;
    private HealthSystem _healthSystem;
    private StateMachine _sm;
    private Animator _animator;
    public VillagerAttack(string stateID, GameObject go, StateMachine sm) : base(stateID)
    {
        m_go = go;
        _healthSystem = m_go.GetComponent<HealthSystem>();
        _sm = sm;
        _animator = m_go.GetComponent<Animator>();
    }

    public override void Enter()
    {
        //Debug.Log("Entering Villager Attack");
        _animator.SetBool("isAttack", true);
        _animator.SetBool("isChase", false);
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
        //Debug.Log("Exitting Villager Attack");
    }
}

// Dead
public class VillagerDeath : State
{
    private GameObject m_go;
    private HealthSystem _healthSystem;
    private StateMachine _sm;
    private UIManager UIMan;
    private Animator _animator;
    public VillagerDeath(string stateID, GameObject go, StateMachine sm) : base(stateID)
    {
        m_go = go;
        _healthSystem = m_go.GetComponent<HealthSystem>();
        _sm = sm;
        UIMan = GameObject.FindFirstObjectByType<UIManager>();
        _animator = m_go.GetComponent<Animator>();
    }

    public override void Enter()
    {
        //Debug.Log("Entering Villager Attack");
        UIMan.SetSubtitleText("Your blade hit a Soft target.");
        //m_go.SetActive(false);
        _animator.SetTrigger("isDead");
    }

    public override void Update(double dt)
    {

    }

    public override void Exit()
    {
        //Debug.Log("Exiting Villager Attack");
    }
}

