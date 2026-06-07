using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private UIManager UIMan;

    public void takeDamage(int dmg)
    {
        health -= dmg;
        if (!UIMan)
            return;
        UIMan.UpdateHealthText(health);
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
}
