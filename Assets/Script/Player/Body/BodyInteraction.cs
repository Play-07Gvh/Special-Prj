using UnityEngine;

public class BodyInteraction : MonoBehaviour
{
    [SerializeField] private UIManager UIMan;

    private LandmarkShape landmark;

    private bool isInteractable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!UIMan)
            UIMan = GameObject.FindFirstObjectByType<UIManager>();
        if (!UIMan)
            Debug.LogError("UIManager not found at " + gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool interact()
    {
        if (isInteractable)
        {
            // Interact with the thing
            Debug.Log("Start interaction with " + landmark.gameObject.name);
            landmark.InteractWith();
            isInteractable = false;
            UIMan.HideOrShowInteract(false);
            return true;
        }
        return false;
    }

    private bool RaycastCheck(GameObject target)
    {
        Vector3 dir = target.transform.position - transform.position;
        RaycastHit hit;
        if (Physics.Raycast(transform.position,dir, out hit))
        {
            if (hit.collider.gameObject != target)
                return false;
            return true;
        }
        return false;
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (isInteractable)
            return;
        // In case I forget to set the collider to ignore the other stuff
        if (other.tag != "Landmark")
            return;
        //if (!RaycastCheck(other.gameObject))
        //    return;
        if (!other.TryGetComponent<LandmarkShape>(out landmark))
            return;
        if (landmark.getInteractivity())
        {
            landmark = null;
            return;
        }
        isInteractable = true;
        UIMan.HideOrShowInteract(true);
    }

    
    public void OnTriggerExit(Collider other)
    {
        if (other.tag != "Landmark")
            return;
        isInteractable = false;
        landmark = null;
        UIMan.HideOrShowInteract(false);
    }
}
