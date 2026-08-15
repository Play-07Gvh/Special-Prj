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
        DisableAttack();
        if (owner == "") owner = gameObject.GetComponentInParent<GameObject>().name;
    }

    //public void enableAttack(bool isDur = false)
    //{
    //    gameObject.SetActive(true);
    //    //ifDur = isDur;
    //    //if (!ifDur)
    //    //    return;
    //    //cd = dur;
    //}

    public void EnableAttack()
    {
        gameObject.SetActive(true);
    }

    public void DisableAttack()
    {
        gameObject.SetActive(false);
    }


    private void FixedUpdate()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        //if (other.tag == "Body" && owner != "Body")
        //{
        //    other.GetComponent<HealthSystem>().takeDamage(dmg, owner);
        //}
        //// Body
        //else if ((other.tag == "Enemy" || other.tag == "Trap") && owner == "Body")
        //{
        //    other.GetComponent<HealthSystem>().takeDamage(dmg, owner);
        //}
        if (other.gameObject.tag == gameObject.tag)
            return;
        if (other.TryGetComponent<HealthSystem>(out HealthSystem tempHealth))
        {
            tempHealth.takeDamage(dmg, owner);
        }
    }
}
