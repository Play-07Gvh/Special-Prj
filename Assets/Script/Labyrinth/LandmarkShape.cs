using UnityEngine;

public class LandmarkShape : MonoBehaviour
{
    [SerializeField] private int order = 0;
    [SerializeField] private UIManager UIMan;
    [SerializeField] private ArrangeShapesPuzzle ASP;
    private bool interactedWith = false;

    [SerializeField] private Material foundMat;
    private MeshRenderer _meshRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!ASP)
            ASP = GameObject.FindFirstObjectByType<ArrangeShapesPuzzle>();
        if (!ASP)
            Debug.Log("ArrangeShapesPuzzle Object not in " + name);

        if (!UIMan)
            UIMan = GameObject.FindFirstObjectByType<UIManager>();
        if (!UIMan)
            Debug.Log("UIMan is missing in " + name);

        _meshRenderer = GetComponent<MeshRenderer>();
        if (!_meshRenderer)
            Debug.LogError("Mesh Renderer variable not found at " + name);
        if (!foundMat)
            Debug.LogWarning("Found material not found at " + name);
    }

    public void SetOrder(int o) 
    {
        order = o;
    }

    public void InteractWith()
    {
        if (interactedWith)
            return;
        ASP.LandmarkInteractedWith(name);
        interactedWith = true;
        UIMan.SetSubtitleText("Number " + order + "...");
        _meshRenderer.material = foundMat;
    }

    public bool getInteractivity()
    {
        return interactedWith;
    }
}
