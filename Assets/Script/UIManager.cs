using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText;

    private void Start()
    {
    }

    public void UpdateHealthText(int hp)
    {
        healthText.text = "HP: " + hp.ToString();
    }
}
