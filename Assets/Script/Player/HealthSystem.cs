using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private UIManager UIMan;

    [SerializeField] private float ifDuration = 2;
    private float duration = 0;
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
            }
            UIMan.SetSubtitleText("You've been hit by " + from);
            UIMan.UpdateHealthText(health);
        }
    }

    public int getHealth()
    {
        return health;
    }

    public void setHealth(int val)
    {
        health = val;
        if (!UIMan)
            return;
        UIMan.UpdateHealthText(health);
    }

    public void FixedUpdate()
    {
        if (duration > 0)
        {
            duration -= Time.deltaTime;
        }
    }
}
