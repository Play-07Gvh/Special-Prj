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
        //startTutorial();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startTutorial()
    {
        Time.timeScale = 0.0f;
        tutorialPanel.SetActive(true);
    }

    public void finishedTutorial()
    {
        Time.timeScale = 1.0f;
        tutorialPanel.SetActive(false);
    }
}
