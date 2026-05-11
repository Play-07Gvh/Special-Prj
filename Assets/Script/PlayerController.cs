using UnityEngine;

// Trying to use MVC (Model View Controller) system

/// <summary>
/// The Controller of the MVC system
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Movement _move;
    private void Start()
    {

    }

    private void Update()
    {
        _move.Move(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
    }
}
