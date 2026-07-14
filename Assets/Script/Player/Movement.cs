using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private CharacterController _cc;
    // Values
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

    public void Move(Vector3 move, bool _noClip = false)
    {
        if (!_noClip)
            move.y -= 9.81f * Time.deltaTime * 50; // for gravity, if I ever implement vertical movement.
        _cc.Move(move * _mSpd * Time.deltaTime);
    }
}
