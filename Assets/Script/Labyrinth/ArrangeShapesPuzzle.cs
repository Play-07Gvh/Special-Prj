using UnityEngine;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.Rendering.Universal;

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
// Spawn limit 1 shape per landmark
// After player picks up one / all shapes, it shows the number of the respective shapes on the head side
// For now if they succeed, the door that prevents the head from moving out is able to be accessed
// I'll just cheat a bit, Body can sense the mana in the puzzles.

public enum side
{
    head = 0,
    body = 1,
}

public class ArrangeShapesPuzzle : Puzzle
{
    //private bool triActive, sqrActive, cirActive;
    //private List<PuzzleShape> shapeList = new() { new PuzzleShape("Triangle", false), new PuzzleShape("Square", false), new PuzzleShape("Circle", false)};
    [SerializeField] private string[] shapeList = { "Triangle", "Square", "Circle"};
    [SerializeField] private TMP_Text[] numbers = new TMP_Text[3];

    [SerializeField] private GameObject[] shapes = new GameObject[3];
    [SerializeField] private Transform[] locations = new Transform[3];

    [SerializeField] private side puzzleSide;
    [SerializeField] private ArrangeShapesPuzzle secondPart;

    int order = 0;

    private void Start()
    {
        //triActive = false; sqrActive = false; cirActive = false;
        if (puzzleSide == side.head)
            Shuffle();

        // Checks
    }
    //public void OpenSesame()
    //{
    //    OpenDoor();
    //}

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

            //TMP_Text tempGO = numbers[i];
            //numbers[i] = numbers[randomIndex];
            //numbers[randomIndex] = tempGO;

            Vector3 tempPos = positions[i];
            positions[i] = positions[randomIndex];
            positions[randomIndex] = tempPos;
        }

        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i].transform.position = positions[i];
        }
    }

    private void Update()
    {
        // if all 3 shapes must be there for door to open
        //if (triActive && sqrActive && cirActive)
        //{
        //    OpenDoor(0);
        //}
        // if each shape open separate doors
        //if (triActive)
        //{
        //    OpenDoor(0);
        //}
        //if (sqrActive)
        //{
        //    OpenDoor(1);
        //}
        //if (cirActive)
        //{
        //    OpenDoor(2);
        //}
        // Phase 1
        if (order >= 3 && puzzleSide == side.head)
        {
            Clear();
            RespawnShapes();
            OpenDoor(0);
            OpenDoor(1);
            secondPart.setOrder(shapes);
        }
        else if (order >= 3 && puzzleSide == side.body)
        {
            order = 0;
            OpenDoor(0);
        }
    }

    public void setOrder(GameObject[] ordering)
    {
        // Simple method to sync up for both head and body puzzle order
        shapes = ordering;
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
        // old
        //switch(shp)
        //{
        //    case shape.triangle:
        //        triActive = true;
        //        break;
        //    case shape.square:
        //        sqrActive = true;
        //        break;
        //    case shape.circle:
        //        cirActive = true;
        //        break;
        //}

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

}
