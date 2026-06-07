using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [HideInInspector] public StateMachine _sm;
    [SerializeField] private GameObject target;
    [SerializeField] private HealthSystem _healthSystem;

    private void Awake()
    {
        _sm = new StateMachine();
    }

    private void Start()
    {
        _sm.AddState(new SpikeIdle("SpikeIdle", gameObject, _sm));
        _sm.AddState(new SpikeActive("SpikeActive", gameObject, _sm));
        _sm.AddState(new SpikeDeactivated("SpikeDeactivated", gameObject, _sm));
        _healthSystem.setHealth(1); // 1 is for active and 0 is for inactive
    }

    private void Update()
    {
        if (_sm.GetCurrentState() == "SpikeDeactivated")
            return;
        if (Vector3.Distance(target.transform.position, transform.position) < 10 && _sm.GetCurrentState() != "SpikeActive")
        {
            _sm.SetNextState("SpikeActive");
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
        other.GetComponent<HealthSystem>().takeDamage(5);
    }
}
