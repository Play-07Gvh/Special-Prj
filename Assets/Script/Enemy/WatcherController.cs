using UnityEngine;
using UnityEngine.AI;

public class WatcherController : MonoBehaviour
{
    [SerializeField] private HealthSystem _health;
    private StateMachine _sm;

    [SerializeField] private UIManager UIMan;

    [SerializeField] private PlayerController head;

    private void Awake()
    {
        _sm = new StateMachine();
    }

    void Start()
    {
        if (!head)
            head = GameObject.Find("Head").GetComponent<PlayerController>();
        if (!head)
            Debug.LogError("Unable to find Head at " + name);
        _sm.AddState(new WatcherSleep("WatcherSleep",gameObject, _sm));
        _sm.AddState(new WatcherWaking("WatcherWaking",gameObject, _sm));
        _sm.AddState(new WatcherAwake("WatcherAwake",gameObject, _sm));
        _sm.AddState(new WatcherDead("WatcherDead",gameObject, _sm));
        if (!UIMan) Debug.LogError(gameObject.name + " does not have UIManager!");
        if (!_health)
            _health = GetComponent<HealthSystem>();
        if (!_health)
        {
            Debug.LogError(gameObject.name + "NO HEALTH SYSTEM");
            return;
        }
        _health.setHealth(1,false); // 1 is for active and 0 is for inactive
    }

    private void Update()
    {
        if (_sm.GetCurrentState() == "WatcherAwake")
        {
            if (head.isInAction)
            {
                head.gameObject.GetComponent<HealthSystem>().takeDamage(1,name);
            }
        }
        _sm.Update(Time.deltaTime);
    }
}
