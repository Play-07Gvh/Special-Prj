using UnityEngine;

public class SpikeIdle : State
{
    private GameObject m_go;
    private HealthSystem _health;
    private StateMachine _sm;
    public SpikeIdle(string stateID, GameObject go, StateMachine sm) : base(stateID)
    {
        m_go = go;
        _health = m_go.GetComponent<HealthSystem>();
        _sm = sm;
    }

    public override void Enter()
    {
        m_go.GetComponent<BoxCollider>().enabled = false;
    }

    public override void Update(double dt)
    {
        if (_health.getHealth() < 1)
        {
            _sm.SetNextState("SpikeDeactivated");
        }
    }

    public override void Exit()
    {

    }
}

public class SpikeActive : State
{
    private GameObject m_go;
    private HealthSystem _health;
    private StateMachine _sm;
    double internalcountdown;
    public SpikeActive(string stateID, GameObject go, StateMachine sm) : base(stateID)
    {
        m_go = go;
        _health = m_go.GetComponent<HealthSystem>();
        _sm = sm;
        internalcountdown = 5;
    }
    public override void Enter()
    {
        m_go.GetComponent<BoxCollider>().enabled = true;
        internalcountdown = 5;
        Debug.Log("Spike Trap activated");
    }

    public override void Update(double dt)
    {
        if (_health.getHealth() < 1)
        {
            _sm.SetNextState("SpikeDeactivated");
            return;
        }

        if (internalcountdown > 0)
        {
            internalcountdown -= dt;
            return;
        }
        else
        {
            _sm.SetNextState("SpikeIdle");
        }
    }

    public override void Exit()
    {
        Debug.Log("Spike Trap going to idle");
    }

}

public class SpikeDeactivated : State
{
    private GameObject m_go;
    private HealthSystem _health;
    private StateMachine _sm;
    public SpikeDeactivated(string stateID, GameObject go, StateMachine sm) : base(stateID)
    {
        m_go = go;
        _health = m_go.GetComponent<HealthSystem>();
        _sm = sm;
    }

    public override void Enter()
    {
        m_go.GetComponent<BoxCollider>().enabled = false;
    }

    public override void Update(double dt)
    {
        // if the trap somehow ever got reactivated, Events / Enemy
        if (_health.getHealth() > 0)
        {
            _sm.SetNextState("SpikeIdle");
            return;
        }
    }

    public override void Exit()
    {

    }

}

