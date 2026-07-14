using UnityEngine;
using UnityEngine.InputSystem;

// https://discussions.unity.com/t/beginner-how-to-switch-between-wasd-arrow-keys-controls-for-specific-player-gameobject/328715/2 

[CreateAssetMenu(fileName = "ControlSchema", menuName = "Scriptable Objects/ControlSchema")]
public class ControlSchema : ScriptableObject
{
    public KeyCode forward;
    public KeyCode backward;
    public KeyCode left;
    public KeyCode right;

    public KeyCode turnL;
    public KeyCode turnR;

    public KeyCode Attack;
}
