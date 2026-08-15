using UnityEngine;

// ...ZZZ...
public class WatcherSleep : State
{
    private GameObject m_go;
    private HealthSystem _healthSystem;
    private StateMachine _sm;
    private UIManager UIMan;
    private float _sleepTime = 15;
    private Animator _animator;
    public WatcherSleep(string stateID, GameObject go, StateMachine sm) : base(stateID)
    {
        m_go = go;
        _healthSystem = m_go.GetComponent<HealthSystem>();
        UIMan = GameObject.FindFirstObjectByType<UIManager>();
        _sm = sm;
        _animator = m_go.GetComponent<Animator>();
    }

    public override void Enter()
    {
        _sleepTime = 2;
        _sleepTime = Random.Range(10, 30);
        UIMan.WatcherWarningDisplay(-1);
        _animator.SetTrigger("isSleeping");
        //m_go.transform.eulerAngles = new Vector3(0, 90, 0);
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
    private Animator _animator;

    public WatcherWaking(string stateID, GameObject go, StateMachine sm) : base(stateID)
    {
        m_go = go;
        _healthSystem = m_go.GetComponent<HealthSystem>();
        _sm = sm;
        UIMan = GameObject.FindFirstObjectByType<UIManager>();
        _animator = m_go.GetComponent<Animator>();
    }

    public override void Enter()
    {
        _dur = 3;
        // DO UI STUFF HERE
        UIMan.WatcherWarningDisplay(0);
        //m_go.transform.eulerAngles = new Vector3(0, 180, 0);
        _animator.SetTrigger("isWaking");
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
        _dur -= (float)dt;
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

    private SFXManager SFXMan;

    private Animator _animator;

    public WatcherAwake(string stateID, GameObject go, StateMachine sm) : base(stateID)
    {
        m_go = go;
        _healthSystem = m_go.GetComponent<HealthSystem>();
        _sm = sm;
        UIMan = GameObject.FindFirstObjectByType<UIManager>();
        SFXMan = GameObject.FindFirstObjectByType<SFXManager>();
        _animator = m_go.GetComponent<Animator>();
    }

    public override void Enter()
    {
        // SFX
        //SFXMan.PlaySFX("WatcherAwake", m_go.transform.position);

        _dur = 2;
        // Enable UI to warn player, maybe also have SFX?
        UIMan.WatcherWarningDisplay(1);
        //m_go.transform.eulerAngles = new Vector3(0, 270, 0);
        _animator.SetTrigger("isAwake");
    }

    public override void Update(double dt)
    {
        if (_dur <= 0)
        {
            _sm.SetNextState("WatcherSleep");
        }
        _dur -= (float)dt;
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
        UIMan.WatcherWarningDisplay(-1);
        m_go.SetActive(false);
        Debug.Log("Watcher Down!");
    }

    public override void Update(double dt)
    {
        Debug.Log("Watcher is dead");
    }

    public override void Exit()
    {
    }

}

