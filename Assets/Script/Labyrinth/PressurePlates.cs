using UnityEngine;

// Simple last puzzle
// Instead of Body player being able to attack the trap (Doesn't make sense)
// Why not I just allow for more interaction for the Head Player?

/// <summary>
/// Head "steps" on the pressure plate to disable a trap on the map.
/// Each trap has a separate icon above them.
/// Body can sense the icon of it
/// Head has to "step" on the respective icon to disable
/// SFX played when the trap is disable infront of them. (Feedback)
/// </summary>

public class PressurePlates : MonoBehaviour
{
    //[SerializeField] private Plate[] _plates = new Plate[4];
    [SerializeField] private HealthSystem[] _traps = new HealthSystem[4];
    [SerializeField] private GameObject[] _icons = new GameObject[4];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //if (_plates.Length <= 0) Debug.LogError("Plates array is missing from " + name);
        //for (int i = _plates.Length; i >= 0; i--)
        //{
        //    if (!_plates[i]) Debug.LogError("A plate is null in " + name);
        //}
        if (_traps.Length <= 0) Debug.LogError("Traps array is missing from " + name);
        for (int i = _traps.Length - 1; i >= 0; i--)
        {
            if (!_traps[i]) Debug.LogError("A trap is null in " + name);
        }
    }

    public void disableTrap(int i)
    {
        _traps[i].setHealth(-1);
    }

    public void enableTrap(int i)
    {
        _traps[i].setHealth(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
