using UnityEngine;
using UnityEngine.UIElements;

public enum shape
{
    Triangle = 0,
    Square = 1,
    Circle = 2,
}

public class Shape : MonoBehaviour
{
    [SerializeField] private shape shp;

    [SerializeField] private ArrangeShapesPuzzle ASP;

    private void Awake()
    {
        // In case somehow there isn't one
        if (ASP == null)
            ASP = FindFirstObjectByType<ArrangeShapesPuzzle>();
        if (ASP == null)
            Debug.LogError("ArrangeShapesPuzzle NOT FOUND!");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetNewPuzzleTarget(ArrangeShapesPuzzle next)
    {
        ASP = next;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(name + " Collided with " + collision.gameObject.name);
        if (shp.ToString() == collision.gameObject.name)    
        {
            ASP.insertPiece(shp);
            gameObject.SetActive(false);
        }
    }
}
