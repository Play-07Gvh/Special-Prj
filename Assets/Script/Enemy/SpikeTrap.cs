using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [HideInInspector] public StateMachine _sm;
    [SerializeField] private GameObject target;
    [SerializeField] private HealthSystem _healthSystem;
    [SerializeField] private UIManager UIMan;

    private void Awake()
    {
        _sm = new StateMachine();
    }

    private void Start()
    {
        _sm.AddState(new SpikeIdle("SpikeIdle", gameObject, _sm));
        _sm.AddState(new SpikeActive("SpikeActive", gameObject, _sm));
        _sm.AddState(new SpikeDeactivated("SpikeDeactivated", gameObject, _sm));
        if (!UIMan)
        {
            Debug.LogError(gameObject.name + " does not have UIManager!");
        }
        if (!_healthSystem)
        {
            Debug.LogError(gameObject.name + "NO HEALTH SYSTEM");
            return;
        }
        _healthSystem.setHealth(1); // 1 is for active and 0 is for inactive
    }

    private void Update()
    {
        if (_sm.GetCurrentState() == "SpikeDeactivated")
            return;

        Vector3 direction = target.transform.position - transform.position;

        if (Vector3.Distance(target.transform.position, transform.position) < 10 && _sm.GetCurrentState() != "SpikeActive")
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, direction, out hit))
            {
                if (hit.collider.gameObject == target)
                {
                    _sm.SetNextState("SpikeActive");
                    Debug.Log("Player is in sight of " + gameObject.name);
                    UIMan.SetSubtitleText("You feel a click below your feet.");
                }
                else
                {
                    Debug.Log("Player is not in sight of " + gameObject.name);
                }
            }
        }
        _sm.Update(Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_sm.GetCurrentState() == "SpikeDeactivated")
            return;
        if (_sm.GetCurrentState() != "SpikeActive")
            return;
        if (other.gameObject != target)
            return;
        other.GetComponent<HealthSystem>().takeDamage(10, gameObject.name);
    }
}
