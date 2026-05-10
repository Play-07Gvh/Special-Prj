using UnityEngine;
using System.Collections;

public class ExceptionLogger : MonoBehaviour
{
    // Internal reference to stream writer object
    private System.IO.StreamWriter SW;

    // Filename to assign to log
    public string LogFileName = "log.txt";

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        SW = new System.IO.StreamWriter(Application.persistentDataPath + "/" + LogFileName);

        Debug.Log(Application.persistentDataPath + "/" + LogFileName);
    }

    private void OnEnable()
    {
        Application.RegisterLogCallback(HandleLog);
        Application.RegisterLogCallback(HandleLog);
    }

    private void OnDisable()
    {
        Application.RegisterLogCallback(null);
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        // If an exception or error, then log to file
        if (type == LogType.Exception || type == LogType.Error)
        {
            SW.WriteLine("Logged at: " + System.DateTime.Now.ToString() + " - Log Desc: " + logString + " - Trace" + stackTrace + " - Type: " + type.ToString());
        }
    }
    // Called when object is destroyed
    private void OnDestroy()
    {
        //Close File
        SW.Close();
    }

}
