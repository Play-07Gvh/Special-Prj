using UnityEngine;

public class AnimationEventTrigger : MonoBehaviour
{
    [SerializeField] private AttackHB _atkHB;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableAttack()
    {
        _atkHB.EnableAttack();
    }

    public void DisableAttack()
    {
        _atkHB.DisableAttack();
    }
}
