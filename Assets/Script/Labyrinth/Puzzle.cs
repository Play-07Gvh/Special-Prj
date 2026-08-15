using UnityEngine;
using System.Collections.Generic;
using System.Collections;

// Leave it as it is
public abstract class Puzzle : MonoBehaviour
{
    [SerializeField] protected List<GameObject> door;
    private float timeElapsed;
    [SerializeField] protected float lerpDuration;
    private float startValue;
    [SerializeField] protected float endValue;
    [SerializeField] protected VFXManager VFXMan;

    private List<GameObject> storedVFX = new List<GameObject>();

    private void Start()
    {
        if (!VFXMan) VFXMan = GameObject.FindFirstObjectByType<VFXManager>();
        if (!VFXMan) Debug.LogError("VFX Manager is missing from " + name);
    }

    protected virtual void OpenDoor(int sel)
    {
        storedVFX.Add(VFXMan.playVFX("OpenSesame", 
            door[sel].transform.position - new Vector3(0,0.5f,0), 
            Quaternion.Euler(new Vector3(door[sel].transform.localEulerAngles.x, door[sel].transform.localEulerAngles.y, -90))));

        StartCoroutine(doorSink(door[sel]));
    }

    IEnumerator doorSink(GameObject target)
    {
        timeElapsed = 0;
        startValue = target.transform.localPosition.y;
        while (timeElapsed < lerpDuration)
        {
            float t = timeElapsed / lerpDuration;
            t = t * t * (3f - 2f * t);
            target.transform.localPosition = new Vector3(target.transform.localPosition.x,
                Mathf.Lerp(startValue, endValue, t),
                target.transform.localPosition.z);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        target.transform.localPosition = new Vector3(transform.localPosition.x, endValue, transform.localPosition.z);
        target.SetActive(false);
        VFXMan.stopVFX(storedVFX[storedVFX.Count - 1]);
        storedVFX.Remove(storedVFX[storedVFX.Count - 1]);
    }
}
