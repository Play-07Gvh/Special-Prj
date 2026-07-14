using UnityEngine;
using System.Collections.Generic;

public struct VFX
{
    public string name;
    public GameObject VFXPrefab;
}

public class VFXManager : MonoBehaviour
{
    [SerializeField] private List<VFX> VFXList;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool playVFX(string name, Vector3 pos)
    {
        for (int i = 0; i < VFXList.Count; i++)
        {
            if (VFXList[i].name == name)
            {
                Instantiate(VFXList[i].VFXPrefab, pos, new Quaternion());
                return true;
            }
        }
        return false;
    }
}
