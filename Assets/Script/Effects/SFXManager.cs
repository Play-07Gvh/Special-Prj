using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct SFX
{
    public string name;
    public float volume;
    public AudioClip audio;
}

public class SFXManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private List<SFX> _SFXList = new List<SFX>();

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySFX(string audioName, Vector3 pos)
    {
        if (_SFXList.Count <= 0)
        {
            Debug.LogWarning("No SFX in manager");
        }
        //AudioClip tempClip;
        for (int i = 0; i < _SFXList.Count; i++)
        {
            if (_SFXList[i].name == audioName)
            {
                //tempClip = _SFXList[i].audio;
                AudioSource.PlayClipAtPoint(_SFXList[i].audio, pos);
                return;
            }
        }
        Debug.LogError("AudioClip not found!");
    }
}
