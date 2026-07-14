using UnityEngine;

public class AttackHB : MonoBehaviour
{
    [SerializeField] private string owner;
    [SerializeField] private int dmg;

    private float dur = 1f;
    private float cd = 0f;
    private bool ifDur = false;

    private void Start()
    {
        //disableAttack();
    }

    public void enableAttack(bool isDur = false)
    {
        gameObject.SetActive(true);
        ifDur = isDur;
        if (!ifDur)
            return;
        cd = dur;
    }

    public void disableAttack()
    {
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (!ifDur)
            return;
        if (cd > 0f)
        {
            cd -= Time.deltaTime;
        }
        else if (cd <= 0f)
        {
            disableAttack();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Body" && owner != "Body")
        {
            other.GetComponent<HealthSystem>().takeDamage(dmg, owner);
        }
        // Body
        else if ((other.tag == "Enemy" || other.tag == "Trap") && owner == "Body")
        {
            other.GetComponent<HealthSystem>().takeDamage(dmg, owner);
        }
    }
}
