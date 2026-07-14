using UnityEngine;
using System.Collections.Generic;

// Leave it as it is
public abstract class Puzzle : MonoBehaviour
{
    [SerializeField] protected List<GameObject> door;



    protected virtual void OpenDoor(int sel)
    {
        door[sel].SetActive(false);
    }
}
