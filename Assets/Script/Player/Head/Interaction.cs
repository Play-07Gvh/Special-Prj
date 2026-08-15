using TMPro;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    private bool isHolding;
    private GameObject heldObj;
    [SerializeField] private float throwStr = 10f;

    [SerializeField] private GameObject headCam, mapCam, bodyCam;
    private bool isMapCam, isBodyCam;

    [SerializeField] private UIManager UIMan;

    private void Start()
    {
        if (!headCam)
            headCam = GameObject.Find("Main Camera");
        if (!headCam)
            Debug.LogError("Main Camera not found in " + name);
        if (!mapCam)
            mapCam = GameObject.Find("Map Camera");
        if (!mapCam)
            Debug.LogError("Map Camera not found in " + name);
        if (!bodyCam)
            bodyCam = GameObject.Find("BodyCam");
        if (!bodyCam)
            Debug.LogError("Body Camera not found in " + name);
        if (!UIMan)
            UIMan = GameObject.FindFirstObjectByType<UIManager>();
        if (!UIMan)
            Debug.LogError("UI Manager not found in " + name);
        isMapCam = isBodyCam = false;
    }

    // Maybe change the logic so that when pressing right control, it grants a state of where the player is in another camera
    public void changeCam()
    {
        if (!isMapCam && !isBodyCam)
        {
            isMapCam = true;
            mapCam.SetActive(true);
            headCam.SetActive(false);
            UIMan.HideShowCrosshair(true, false);
        }
        else if (!isBodyCam)
        {
            isBodyCam = true;
            isMapCam = false;
            bodyCam.SetActive(true);
            mapCam.SetActive(false);
            UIMan.HideShowCrosshair(true, false);
        }
        else 
        {
            isBodyCam = false;
            headCam.SetActive(true);
            bodyCam.SetActive(false);
            UIMan.HideShowCrosshair(true, true);
        }
    }

    public bool GetIsMapCam()
    {
        return isMapCam;
    }

    public bool GetIsBodyCam()
    {
        return isBodyCam;
    }

    public bool GetIsHolding()
    {
        return isHolding;
    }

    public bool Pickup()
    {
        if (isHolding)
        {
            Debug.LogWarning("You are already holding something!");
            return false;
        }
        RaycastHit hitInfo;
        // Direction calculation
        //Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo,10f,LayerMask.GetMask("Shape")))
        {
            Debug.Log("Shape HIT!");
            GrabObj(hitInfo.collider.gameObject);
            UIMan.ToggleHeadInteractUI(2);
            return true;
        }
        return false;
    }

    private void GrabObj(GameObject GO)
    {
        heldObj = GO;
        heldObj.transform.position = transform.position + (transform.forward * 0.25f);
        isHolding = true;
    }

    private void Update()
    {
        if (isHolding)
        {
            heldObj.transform.position = transform.position + transform.forward * 0.25f;
        }
        else if (Physics.Raycast(transform.position,transform.forward, 10f, LayerMask.GetMask("Shape")))
        {
            UIMan.ToggleHeadInteractUI(1);
        }
        else
        {
            UIMan.ToggleHeadInteractUI(0);
        }
    }

    public bool Throw()
    {
        if (!isHolding)
            return false;
        if (heldObj == null)
            return false;
        Vector3 force = (transform.forward) * throwStr;
        Rigidbody heldRigid = heldObj.GetComponent<Rigidbody>();
        heldRigid.linearVelocity = Vector3.zero;
        heldRigid.angularVelocity = Vector3.zero;
        heldRigid.AddForce(force,ForceMode.Impulse);
        isHolding = false;
        UIMan.ToggleHeadInteractUI(0);
        return true;
    }
}
