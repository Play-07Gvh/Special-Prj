using System.Collections;
using UnityEngine;

public class Plate : MonoBehaviour
{
    [SerializeField] private PressurePlates _PP;
    [SerializeField] private int trapIndex;

    // Sinking platforms https://gamedevbeginner.com/the-right-way-to-lerp-in-unity-with-examples/
    private float timeElapsed;
    [SerializeField] private float lerpDuration;
    private float startValue;
    private float endValueA = -0.15f;
    private float endValueB = 0f;

    private bool isPressed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!_PP) _PP = GameObject.FindFirstObjectByType<PressurePlates>();
        if (!_PP) Debug.LogError("Pressure Plate Script is missing from " + name);
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator PlateResponse()
    {
        timeElapsed = 0;
        startValue = transform.localPosition.y;

        while (timeElapsed < lerpDuration)
        {
            float t = timeElapsed / lerpDuration;
            t = t * t * (3f - 2f * t);
            transform.localPosition = new Vector3(transform.localPosition.x,
                Mathf.Lerp(startValue, (isPressed) ? endValueA : endValueB, t), 
                transform.localPosition.z);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = new Vector3(transform.localPosition.x, (isPressed) ? endValueA : endValueB, transform.localPosition.z);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Head")
        {
            _PP.disableTrap(trapIndex);
            isPressed = true;
            StopCoroutine("PlateResponse");
            StartCoroutine(PlateResponse());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Head")
        {
            _PP.enableTrap(trapIndex);
            isPressed = false;
            StopCoroutine("PlateResponse");
            StartCoroutine(PlateResponse());
        }
    }

    public void SetIndex(int i)
    {
        trapIndex = 0;
    }
}
