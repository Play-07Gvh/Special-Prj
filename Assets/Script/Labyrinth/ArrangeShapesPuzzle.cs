using UnityEngine;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.Rendering.Universal;
using UnityEditor;

public struct PuzzleShape
{
    public string Name;
    public bool Active;
    public Transform Position;
    public PuzzleShape(string name, bool act, Transform pos)
    {
        Name = name;
        Active = act;
        Position = pos;
    }
}

// TO:DO Make it so that if they mess up either game over or give a strike (3 strikes = gameover)
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
    //private bool triActive, sqrActive, cirActive;
    //private List<PuzzleShape> shapeList = new() { new PuzzleShape("Triangle", false), new PuzzleShape("Square", false), new PuzzleShape("Circle", false)};
    [SerializeField] private string[] shapeList = { "Triangle", "Square", "Circle"};
    [SerializeField] private GameObject[] numbers = new GameObject[3];
    [SerializeField] private GameObject[] shapes = new GameObject[3];
    [SerializeField] private Transform[] locations = new Transform[3];
    [SerializeField] private GameObject[] icons = new GameObject[3];

    [SerializeField] private LandmarkShape[] LandmarkArea = new LandmarkShape[3];
    private int[] landmarkOrder = new int[3] { 0, 1, 2 };
    //private bool landmark1Interacted, landmark2Interacted, landmark3Interacted;

    private int landmarkCount = 0;

    //[SerializeField] private side puzzleSide;
    //[SerializeField] private ArrangeShapesPuzzle secondPart;

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
            //RespawnShapes();
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
            OpenDoor(2);
        }
    }

    public void HideOrShow(bool isHide)
    {
        if (isHide)
        {
            for (int i = 0; i < numbers.Length;i++)
            {
                //icons[i].transform.position = numbers[i].transform.position;
                numbers[i].SetActive(false);
                icons[i].SetActive(false);
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
            // Set active for the shapes
            shapes[i].SetActive(true);
        }
    }

    public void insertPiece(shape shp)
    {
        // if list is better
        //if (shapeList[order].Name != shp.ToString())
        //{
        //    Fail();
        //    return;
        //}
        //PuzzleShape tempShp = shapeList[order];
        //tempShp.Active = true;

        if (shapeList[order] != shp.ToString())
        {
            Fail();
            return;
        }
        order++;
    }

    public void Fail()
    {
        //order = 0;
        //Shuffle();
        // DO SOMETHING. GAME OVER? OR A STRIKE?
        Debug.Log("Failure.");
    }

    public void Clear()
    {
        //for (int i = 0;i < shapeList.Count;i++)
        //{
        //    PuzzleShape tempShape = shapeList[i];
        //    tempShape.Active = false; //...?
        //}
        order = 0;
        Shuffle();
    }

    public void LandmarkInteractedWith(string landmarkName)
    {
        ShowIcon(landmarkName);
        landmarkCount++;
    }
    //public bool LandmarkInteractedWith(int no)
    //{
    //    switch (no)
    //    {
    //        case 1:
    //            if (landmark1Interacted) return false;
    //            landmark1Interacted = true;
    //            break;
    //        case 2:
    //            if (landmark2Interacted) return false;
    //            landmark2Interacted = true;
    //            break;
    //        case 3:
    //            if (landmark3Interacted) return false;
    //            landmark3Interacted = true;
    //            break;
    //    }

    //    landmarkCount++;
    //    return true;
    //}

}
