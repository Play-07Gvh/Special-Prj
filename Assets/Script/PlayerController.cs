using UnityEngine;

// Trying to use MVC (Model View Controller) system

/// <summary>
/// The Controller of the MVC system
/// </summary>
/// 

public enum BodyPart
{
    Head = 0,
    Body = 1,
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Movement _move;
    [SerializeField] private OutlineObjects _outObj;

    private float _horInput;
    private float _verInput;
    private Transform _transform;
    private Vector3 _finalMove;

    private Collider[] _collider;
    [SerializeField] private ControlSchema _controlSchema;

    [SerializeField] private float sensitivity = 3f;
    private Vector2 rotation = Vector2.zero;

    private bool _isNoClip = false;

    public BodyPart _bp { get; private set; }

    [SerializeField] private HealthSystem health;

    private void Awake()
    {
        switch(tag)
        {
            case "Head":
                _bp = BodyPart.Head;
                break;
            case "Body":
                _bp = BodyPart.Body;
                break;
            default: break;
        }
        if (_controlSchema == null)
            Debug.LogError("No Controls?");
        if (!health)
        {
            Debug.LogWarning(gameObject + "'s HealthSystem not found");
            return;
        }
        health.setHealth(100);
    }

    private void Start()
    {
        _transform = transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void moveInput()
    {
        if (_controlSchema == null)
            return;

        if (Input.GetKey(_controlSchema.left))
            _horInput = -1;
        else
            _horInput = 0;
        if (Input.GetKey(_controlSchema.right))
            _horInput = 1;
        if (Input.GetKey(_controlSchema.forward))
            _verInput = 1;
        else
            _verInput = 0;
        if (Input.GetKey(_controlSchema.backward))
            _verInput = -1;

        if (Input.GetKeyUp(KeyCode.BackQuote))
            _isNoClip = !_isNoClip;

        if(_isNoClip)
        {
            _finalMove = _horInput * _transform.right + _verInput * _transform.forward;
            _move.Move(_finalMove);
            return;
        }

        _finalMove = _horInput * _transform.right + _verInput * _transform.forward;
        _move.Move(_finalMove);
    }

    private void lookInput()
    {
        rotation.y += Input.GetAxis("Mouse X") * sensitivity;
        rotation.x += -Input.GetAxis("Mouse Y") * sensitivity;
        rotation.x = Mathf.Clamp(rotation.x, -80f, 80f); // Limit vertical rotation
        transform.eulerAngles = new Vector3(rotation.x, rotation.y, 0);
    }

    private void Update()
    {
        lookInput();
    }

    private void FixedUpdate()
    {
        moveInput();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (_outObj != null)
            _outObj.RenderOject(collider.gameObject);
    }
}
