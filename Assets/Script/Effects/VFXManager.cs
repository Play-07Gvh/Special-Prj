using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[System.Serializable]
public struct VFX
{
    public string name;
    public GameObject VFXPrefab;
}

public class VFXManager : MonoBehaviour
{
    [SerializeField] private List<VFX> VFXList;

    [SerializeField] private Volume vol;
    private Vignette hitEffect;
    private float dur = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!vol) vol = GameObject.Find("Global Volume").GetComponent<Volume>();
        if (!vol) Debug.LogError("Volume not found in " + name);

        if (!hitEffect)
            if (!(vol.profile.TryGet<Vignette>(out hitEffect)))
                Debug.LogError("hitEffect not found in " + name);
    }

    // Update is called once per frame
    void Update()
    {
        if (dur > 0)
        {
            dur -= Time.deltaTime * 0.5f;
            hitEffect.intensity.value = dur;
        }
        else
        {
            if (hitEffect.intensity.value != 0)
                hitEffect.intensity.value = 0;
        }
    }

    public GameObject playVFX(string name, Vector3 pos, Quaternion rot)
    {
        for (int i = 0; i < VFXList.Count; i++)
        {
            if (VFXList[i].name == name)
            {
                return Instantiate(VFXList[i].VFXPrefab, pos, rot);
            }
        }
        return null;
    }

    public void stopVFX(GameObject target)
    {
        Destroy(target);
    }

    public void hitVFX()
    {
        dur = 0.5f;
    }
}
