using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Linq.Expressions;

[System.Serializable]
public struct SFX
{
    public string name;
    //public float volume;
    public AudioClip audio;
}

[System.Serializable]
public struct BGM
{
    public string name;
    public AudioClip audio;
}


public class SFXManager : MonoBehaviour
{
    [SerializeField] private List<SFX> _SFXList = new List<SFX>();
    [SerializeField] private List<BGM> _BGMList = new List<BGM>();
    [SerializeField] private BGM loseBGM;
    [SerializeField] private BGM winBGM;

    private int _BGMIndex = 0;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _BGMSource;

    private bool isStop;

    void Start()
    {
        if (_SFXList.Count <= 0)
        {
            Debug.LogWarning("No SFX in manager");
        }
        if (_BGMList.Count <= 0)
        {
            Debug.LogWarning("No BGM in manaegr");
        }
        ShuffleBGM();
    }

    // Update is called once per frame
    void Update()
    {
        if (isStop) return;
        if (!_BGMSource.isPlaying) PlayBGM();
    }

    private void ShuffleBGM()
    {
        for (int i = 0; i < _BGMList.Count; i++)
        {
            BGM tempBGM = _BGMList[i];
            int randomIndex = Random.Range(i, _BGMList.Count);
            _BGMList[i] = _BGMList[randomIndex];
            _BGMList[randomIndex] = tempBGM;
        }
    }

    public void PlaySFX(string audioName, Vector3 pos)
    {
        if (isStop) return;
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

    public void PlayBGM()
    {
        if (_BGMList.Count <= 0)
        {
            Debug.LogError("No BGM in manager");
            return;
        }
        _BGMSource.clip = _BGMList[_BGMIndex].audio;
        _BGMSource.Play();
        if (_BGMIndex >= _BGMList.Count - 1) _BGMIndex = 0;
        else _BGMIndex++;
    }

    public void PlayLoseBGM()
    {
        isStop = true;
        _BGMSource.Stop();
        _BGMSource.clip = loseBGM.audio;
        _BGMSource.Play();
    }

    public void PlayWinBGM()
    {
        isStop = true;
        _BGMSource.Stop();
        _BGMSource.clip = winBGM.audio;
        _BGMSource.Play();
    }

    public void toggleHeartBeat(bool trig, float vol)
    {
        // Add distance effect, further away less loud
        // closer loud
        _audioSource.loop = trig;
        _audioSource.volume = vol;
        if (trig && !_audioSource.isPlaying) _audioSource.Play();
    }
}
