using System.Net;
using UnityEngine;

/// <summary>
/// The View of the MVC (Model View Controller)
/// </summary>
public class OutlineObjects : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RenderOject(GameObject target)
    {
        if (target.layer == 6) return;
        if (target.tag == "Floor" || target.tag == "Wall")
        {
            target.layer = 6;
        }
    }
}
