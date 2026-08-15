using UnityEngine;
using System.Collections.Generic;

// TO:DO Make it so that if they mess up either game over or give a strike (3 strikes = gameover) <- Nope not doing that
// Add spawn doors for the body
// remove spawn doors for the body when the head fits the shapes in order
// Change to Phase 2 for the body to act upon
// Have landmarks on the map
// Show landmark icons above the shapes
// Body interacts with the landmarks                                                        Spawn limit 1 shape per landmark (Cancel)
// Body senses the number at the landmark after interacting                                 After player picks up one / all shapes, it shows the number of the respective shapes on the head side (Cancel)
// For now if they succeed, the door that prevents the head from moving out is able to be accessed
// I'll just cheat a bit, Body can sense the mana in the puzzles.

//public enum side
//{
//    head = 0,
//    body = 1,
//}

public class ArrangeShapesPuzzle : Puzzle
{
    [SerializeField] private string[] shapeList = { "Triangle", "Square", "Circle"};
    [SerializeField] private GameObject[] numbers = new GameObject[3];
    [SerializeField] private GameObject[] shapes = new GameObject[3];
    [SerializeField] private Transform[] locations = new Transform[3];
    [SerializeField] private GameObject[] icons = new GameObject[3];
    [SerializeField] private Renderer[] wallShapeRender = new Renderer[3];

    [SerializeField] private LandmarkShape[] LandmarkArea = new LandmarkShape[3];
    private int[] landmarkOrder = new int[3] { 0, 1, 2 };

    private int landmarkCount = 0;

    [SerializeField] private Material correctMat;

    int order = 0;
    int phase = 0;

    private void Start()
    {
        Shuffle();
    }

    // https://docs.unity3d.com/2022.3/Documentation/Manual/class-Random.html
    private void Shuffle()
    {
        List<Vector3> positions = new();
        for (int i = 0; i < numbers.Length; i++)
        {
            positions.Add(numbers[i].transform.position);
        }

        for (int i = 0; i < shapeList.Length; i++)
        {
            string FinalShape = shapeList[i];
            int randomIndex = Random.Range(i, shapeList.Length);
            shapeList[i] = shapeList[randomIndex];
            shapeList[randomIndex] = FinalShape;

            Vector3 tempPos = positions[i];
            positions[i] = positions[randomIndex];
            positions[randomIndex] = tempPos;

            int temp = landmarkOrder[i];
            landmarkOrder[i] = landmarkOrder[randomIndex];
            landmarkOrder[randomIndex] = temp;
        }

        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i].transform.position = positions[i];
        }

        setOrder(landmarkOrder);

    }

    private void Update()
    {
        if (landmarkCount >= 3)
        {
            landmarkCount = 0;
            RespawnShapes();
        }

        // At the start
        if (order >= 3 && phase == 0)
        {
            phase++;
            Clear();
            // Hide the numbers
            HideOrShow(true);
            OpenDoor(0);
            OpenDoor(1);
        }
        // Done
        else if(order >= 3 && phase == 1)
        {
            phase = -1;
            order = -1;
            for (int i = 0; i < wallShapeRender.Length; i++)
            {
                wallShapeRender[i].material = correctMat;
            }
            OpenDoor(2);
        }
    }

    public void HideOrShow(bool isHide)
    {
        if (isHide)
        {
            for (int i = 0; i < numbers.Length;i++)
            {
                numbers[i].SetActive(false);
                icons[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                icons[i].SetActive(true);
                numbers[i].SetActive(true);
            }
        }
    }

    public void ShowIcon(string landmarkName)
    {
        switch (landmarkName)
        {
            case "Landmark#1":
                icons[0].SetActive(true);
                break;
            case "Landmark#2":
                icons[1].SetActive(true);
                break;
            case "Landmark#3":
                icons[2].SetActive(true);
                break;
            default:
                Debug.LogWarning("Name not found?");
                break;
        }
    }

    public void setOrder(int[] list)
    {
        // Simple method to sync up for both head and body puzzle order
        //shapes = ordering;
        // Change to having an order in the landmarks and icons.
        if (phase != 1)
            return;
        for (int i = 0; i < list.Length;i++)
        {
            LandmarkArea[list[i]].SetOrder(i + 1);
        }
    }

    public void RespawnShapes()
    {
        // Spawn at random specfiic areas
        // For now just spawn at set locations

        // Move all the shapes
        for (int i = 0; i <  shapes.Length; i++)
        {
            shapes[i].transform.position = locations[i].position;
            shapes[i].GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            // Set active for the shapes
            shapes[i].SetActive(true);
        }
    }

    public void insertPiece(shape shp)
    {
        if (shapeList[order] != shp.ToString())
        {
            Fail();
            return;
        }
        order++;
    }

    public void Fail()
    {
        RespawnShapes();
        order = 0;
    }

    public void Clear()
    {
        order = 0;
        Shuffle();
    }

    public void LandmarkInteractedWith(string landmarkName)
    {
        ShowIcon(landmarkName);
        landmarkCount++;
    }

}
