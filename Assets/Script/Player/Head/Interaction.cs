using TMPro;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    private bool isHolding;
    private GameObject heldObj;
    [SerializeField] private float throwStr = 10f;

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
            return true;
        }
        Debug.DrawRay(transform.position, transform.forward);
        return false;
    }

    private void GrabObj(GameObject GO)
    {
        heldObj = GO;
        heldObj.transform.position = transform.position;
        isHolding = true;
    }

    private void Update()
    {
        if (isHolding)
        {
            heldObj.transform.position = transform.position;
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
        return true;
    }
}
