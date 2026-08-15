using NUnit.Framework.Constraints;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [HideInInspector] public StateMachine _sm;
    [SerializeField] private GameObject target;
    [SerializeField] private HealthSystem _healthSystem;
    [SerializeField] private UIManager UIMan;

    [SerializeField] private string _stateName;

    [SerializeField] private GameObject _icon;
    private bool isDisabled = false;

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
        if (!_icon) Debug.LogError("Icon is missing from " + name);
        _healthSystem.setHealth(1,false); // 1 is for active and 0 is for inactive
        _stateName = "";
    }

    private void Update()
    {
        _stateName = _sm.GetCurrentState();
        if (_sm.GetCurrentState() == "SpikeDeactivated")
        {
            if (_icon.activeSelf) _icon.SetActive(false);
            if (!isDisabled)
            {
                target.GetComponentInChildren<OutlineObjects>().HostileDown(gameObject);
                isDisabled = true;
            }
            _sm.Update(Time.deltaTime);
            return;
        }
        else isDisabled = false;
            Vector3 direction = target.transform.position - transform.position;

        if (Vector3.Distance(target.transform.position, transform.position) < 10)
        {
            if (!_icon.activeSelf) _icon.SetActive(true);
            RaycastHit hit;
            if (_sm.GetCurrentState() == "SpikeActive")
            {
            }
            else if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity, LayerMask.GetMask("Body")))
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
        else
        {
            if (_icon.activeSelf) _icon.SetActive(false);
        }
        _sm.Update(Time.deltaTime);
        //_isActive = (_sm.GetCurrentState() == "SpikeActive");
        //_icon.SetActive(_sm.GetCurrentState() == "SpikeActive");
    }

    private void OnTriggerStay(Collider other)
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
