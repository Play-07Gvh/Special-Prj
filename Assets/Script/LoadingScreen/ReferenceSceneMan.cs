using UnityEngine;

// Did this because the brought over SceneManager is unable to be referenced for the buttons.
public class ReferenceSceneMan : MonoBehaviour
{
    private SceneMan sceneMan;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sceneMan = GameObject.FindFirstObjectByType<SceneMan>();
        if (!sceneMan) Debug.LogError("Scene Man can't be found!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void tempChangeScenes(string sceneName)
    {
        sceneMan.ChangeScene(sceneName);
    }

    public void tempExit()
    {
        Application.Quit();
    }
}
