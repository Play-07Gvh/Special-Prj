using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorialPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void Awake()
    {
    }

    void Start()
    {
        Time.timeScale = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void finishedTutorial()
    {
        Time.timeScale = 1.0f;
        tutorialPanel.SetActive(false);
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }
}
