using System;
using System.Collections.Generic;
using UnityEngine;

// More akin to Body Sense
public class OutlineObjects : MonoBehaviour
{
    // Store current rendered objects and time they are rendered
    private Dictionary<GameObject, float> foundObjs;
    private List<GameObject> objs;
    [SerializeField] private float seenTimer = 3f;

    [SerializeField] private List<GameObject> warnObjs;

    [SerializeField] private UIManager UIMan;

    [SerializeField] private SFXManager SFXMan;

    void Start()
    {
        foundObjs = new Dictionary<GameObject, float>();
        objs = new List<GameObject>();
        warnObjs = new List<GameObject>();

        if (!SFXMan)
            SFXMan = GameObject.FindFirstObjectByType<SFXManager>().GetComponent<SFXManager>();
        if (!SFXMan)
            Debug.LogError("SFXManager for Player is not found!");

    }

    public void RenderOject(GameObject target)
    {
        if (target.layer == 6) return;
        if (target.tag == "Floor" || target.tag == "Wall" || target.tag == "Landmark")
        {
            target.layer = 6;
            //foundObjs.Add(target, seenTimer);
            //objs.Add(target);
        }
        // Uncomment when you done with the SFX finding
        SFXMan.PlaySFX("Bump", transform.position);
    }

    public void DisplayWarning(Vector3 target)
    {
        // THIS WORKS?
        float rot = transform.localEulerAngles.y;
        transform.LookAt(target);
        float dir = MathF.Abs(rot - MathF.Abs(transform.localEulerAngles.y));

        float dist = MathF.Max((1 - Vector3.Distance(transform.position, target) / 15), 0.1f);
        //Debug.Log("Distance " + dist);

        if (dir >= 315 || dir < 45)
        {
            UIMan.warningDisplay(warnDirection.Front, dist);
        }
        else if (dir >= 45 && dir < 135)
        {
            UIMan.warningDisplay(warnDirection.Right, dist);
        }
        else if (dir >= 135 && dir < 225)
        {
            UIMan.warningDisplay(warnDirection.Back, dist);
        }
        else if (dir >= 225 && dir < 315)
        {
            UIMan.warningDisplay(warnDirection.Left, dist);
        }
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        //Debug.Log("Enemy sensed at " + dir + " Degrees!");
    }

    // NOTE: EVEN IF THE PLAYER IS MOVING, THEY WILL UNRENDER BY COUNTDOWN
    // TO:DO : Add a function it only starts counting down when
    // a) Player gets out of range of the obj
    // b) Player is idle (Already did)
    public void FixedUpdate()
    {
        // Update the countdown of each rendered game obj
        //for(int i = 0; i < objs.Count; i++)
        //{
        //    GameObject obj = objs[i];
        //    // Time to derender them
        //    if (foundObjs[obj] <= 0)
        //    {
        //        obj.layer = 9;
        //        foundObjs.Remove(obj);
        //        objs.Remove(obj);
        //        continue;
        //    }
        //    foundObjs[obj] -= Time.deltaTime;
        //}
    }

    public void Update()
    {
        UIMan.warningRemove();
        for (int i = 0; i < warnObjs.Count; i++)
        {
            DisplayWarning(warnObjs[i].transform.position);
        }
        if (warnObjs.Count > 0)
        {
            // Warning, this may constantly tick, add a check for when it is playing.
            //SFXMan.PlaySFX("Heartbeat", transform.position);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        warnObjs.Add(other.gameObject);
        //DisplayWarning(other.gameObject);
        //Debug.Log(other.gameObject.name + " sensed");
    }

    public void OnTriggerExit(Collider other)
    {
        warnObjs.Remove(other.gameObject);
        Debug.Log("Lost " + other.gameObject.name);
    }
}
