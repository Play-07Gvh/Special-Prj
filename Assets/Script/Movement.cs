using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// The Model of the MVC 
/// </summary>

public class Movement : MonoBehaviour
{
    [SerializeField] private CharacterController _cc;
    [SerializeField] private float _mSpd;

    private void Start()
    {
        if (!_cc)
            _cc = GetComponent<CharacterController>();
        if (!_cc)
            Debug.LogError(gameObject.name + "'s CharacterController not found!");
    }

    private void Update()
    {
        
    }

    public void Move(Vector3 move)
    {
        _cc.Move(move * _mSpd * Time.deltaTime);
    }
}
