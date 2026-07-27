using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private UIManager UIMan;

    [SerializeField] private float ifDuration = 2;
    private float duration = 0;

    [SerializeField] private VFXManager VFXMan;
    [SerializeField] private SFXManager SFXMan;

    private void Start()
    {
        if (!VFXMan) VFXMan = GameObject.FindFirstObjectByType<VFXManager>();
        if (!VFXMan) Debug.LogError("VFX Manager not found in " + name);

        if (!SFXMan) SFXMan = GameObject.FindFirstObjectByType<SFXManager>();
        if (!SFXMan) Debug.LogError("SFX Manager not found in " + name);
    }

    public void takeDamage(int dmg, string from)
    {
        // Give "IFrames" to prevent a lot of hits from happening at once.
        if (duration <= 0)
        {
            health -= dmg;
            duration = ifDuration;
        }

        if (!UIMan)
        {
            Debug.LogWarning(gameObject.name + " has no UI manager for health");
            return;
        }
        if (gameObject.tag == "Body" || gameObject.tag == "Head")
        {
            if (health < 1)
            {
                UIMan.lose();
                return;
            }
            if (gameObject.tag == "Body")
            {
                VFXMan.hitVFX();
                // SFXMan.PlaySFX("PlayerHit", gameObject.transform.position);
                UIMan.SetSubtitleText("You've been hit by " + from);
                UIMan.UpdateHealthText(health, false);
            }
            else if (gameObject.tag == "Head")
            {
                VFXMan.hitVFX();
                //SFXMan.PlaySFX("PlayerHit", gameObject.transform.position);
                UIMan.UpdateHealthText(health, true);
            }
        }
    }

    public void testTakeDMG()
    {
        VFXMan.hitVFX();
    }

    public int getHealth()
    {
        return health;
    }

    public void setHealth(int val, bool BoH)
    {
        health = val;
        if (!UIMan)
            return;
        UIMan.UpdateHealthText(health, BoH);
    }

    public void FixedUpdate()
    {
        if (duration > 0)
        {
            duration -= Time.deltaTime;
        }
    }
}
