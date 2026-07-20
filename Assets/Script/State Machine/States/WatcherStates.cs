using UnityEngine;
using UnityEngine.InputSystem.Android;

// ...ZZZ...
public class WatcherSleep : State
{
    private GameObject m_go;
    private HealthSystem _healthSystem;
    private StateMachine _sm;
    private float _sleepTime = 30; // at the start is 30 seconds
    public WatcherSleep(string stateID, GameObject go, StateMachine sm) : base(stateID)
    {
        m_go = go;
        _healthSystem = m_go.GetComponent<HealthSystem>();
        _sm = sm;
    }

    public override void Enter()
    {
        _sleepTime = Random.Range(10, 30);
    }

    public override void Update(double dt)
    {
        if (_healthSystem.getHealth() < 1)
        {
            _sm.SetNextState("WatcherDead");
            return;
        }
        _sleepTime -= (float)dt;
        if (_sleepTime <= 0)
        {
            _sm.SetNextState("WatcherWaking");
        }
    }

    public override void Exit()
    {
    }
}

// ...Wuh?
public class WatcherWaking : State
{
    private GameObject m_go;
    private HealthSystem _healthSystem;
    private StateMachine _sm;
    private UIManager UIMan;
    private float _dur;

    public WatcherWaking(string stateID, GameObject go, StateMachine sm) : base(stateID)
    {
        m_go = go;
        _healthSystem = m_go.GetComponent<HealthSystem>();
        _sm = sm;
        UIMan = GameObject.FindFirstObjectByType<UIManager>();
    }

    public override void Enter()
    {
        _dur = 3;
        // DO UI STUFF HERE
    }

    public override void Update(double dt)
    {
        if (_healthSystem.getHealth() < 1)
        {
            _sm.SetNextState("WatcherDead");
        }

        if (_dur <= 0)
        {
            _sm.SetNextState("WatcherAwake");
        }
    }

    public override void Exit()
    {
    }
}

// Oh yeah I was supposed to watch over em...
public class WatcherAwake : State
{
    private GameObject m_go;
    private HealthSystem _healthSystem;
    private StateMachine _sm;
    private UIManager UIMan;
    private float _dur;
    public WatcherAwake(string stateID, GameObject go, StateMachine sm) : base(stateID)
    {
        m_go = go;
        _healthSystem = m_go.GetComponent<HealthSystem>();
        _sm = sm;
        UIMan = GameObject.FindFirstObjectByType<UIManager>();
    }

    public override void Enter()
    {
        // Enable UI to warn player, maybe also have SFX?
    }

    public override void Update(double dt)
    {

    }

    public override void Exit()
    {
    }
}

public class WatcherDead : State
{
    private GameObject m_go;
    private StateMachine _sm;
    private UIManager UIMan;
    public WatcherDead(string stateID, GameObject go, StateMachine sm) : base(stateID)
    {
        m_go = go;
        _sm = sm;
        UIMan = GameObject.FindFirstObjectByType<UIManager>();
    }

    public override void Enter()
    {
        m_go.SetActive(false);
    }

    public override void Update(double dt)
    {

    }

    public override void Exit()
    {
    }

}

