using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum BodyPart
{
    Head = 0,
    Body = 1,
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Movement _move;
    [SerializeField] private OutlineObjects _outObj;

    [SerializeField] private Transform camView;
    [SerializeField] private Collider _detection;

    [SerializeField] private float bodyCamSens = 10;

    private float _horInput;
    private float _verInput;
    private Transform _transform;
    private Vector3 _finalMove;

    //private Collider[] _collider;
    [SerializeField] private ControlSchema _controlSchema;

    [SerializeField] private float sensitivity = 3f;
    private Vector2 rotation = Vector2.zero;

    [SerializeField] private Interaction interact;
    private bool isM1Held;
    private bool isM2Held;

    [SerializeField] private AttackHB atkHB;

    private bool _isNoClip = false;

    //[SerializeField] private SFXManager SFXMan;

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

        if (!camView)
            Debug.LogError("Camera Transform is not here!");
        //if (!SFXMan)
        //    SFXMan = GameObject.FindFirstObjectByType<SFXManager>().GetComponent<SFXManager>();
        //if (!SFXMan)
        //    Debug.LogError("SFXManager for Player is not found!");
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

        // Do not detect anything if not moving
        //_detection.enabled = !(_horInput == 0 && _verInput == 0);

        _finalMove = _horInput * _transform.right + _verInput * _transform.forward;
        _move.Move(_finalMove, _isNoClip);
    }

    private void lookInput()
    {
        if (_bp == BodyPart.Head)
        {
            rotation.y += Input.GetAxis("Mouse X") * sensitivity;
            rotation.x += -Input.GetAxis("Mouse Y") * sensitivity;
            rotation.x = Mathf.Clamp(rotation.x, -80f, 80f); // Limit vertical rotation
            transform.eulerAngles = new Vector3(rotation.x, rotation.y, 0);
            //camView.localEulerAngles = new Vector3(rotation.x, 0, 0);
        }
        else if (_bp == BodyPart.Body)
        {
            rotation.y -= Convert.ToInt32(Input.GetKey(_controlSchema.turnL)) * bodyCamSens;
            rotation.y += Convert.ToInt32(Input.GetKey(_controlSchema.turnR)) * bodyCamSens;
            transform.eulerAngles = new Vector3(0, rotation.y, 0);
        }
    }

    private void Update()
    {
        //if (_bp == BodyPart.Head)
        if (_bp == BodyPart.Head)
        {
            PickupAction();
            ThrowAction();
        }
        else if (_bp == BodyPart.Body)
        {
            attackInput();
        }
    }

    private void attackInput()
    {
        if (Input.GetKey(_controlSchema.Attack))
        {
            atkHB.enableAttack(true);
        }
    }

    private void PickupAction()
    {
        if (Mouse.current.leftButton.isPressed && !isM1Held)
        {
            Debug.Log("Interact Key Pressed");
            interact.Pickup();
        }
        isM1Held = Mouse.current.leftButton.isPressed;
    }

    private void ThrowAction()
    {
        if (Mouse.current.rightButton.isPressed && !isM2Held)
        {
            Debug.Log("Throw Key Pressed");
            interact.Throw();
        }
        isM2Held = Mouse.current.rightButton.isPressed;
    }

    private void FixedUpdate()
    {
        lookInput();
        moveInput();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (_bp == BodyPart.Head)
            return;
        if (_outObj != null)
            _outObj.RenderOject(collider.gameObject);
        //SFXMan.PlaySFX("Bump", transform.position + collider.ClosestPoint(transform.position));
    }

    //private void OnTriggerStay(Collider collider)
    //{
    //    if (_outObj != null)
    //        _outObj.RenderOject(collider.gameObject);
    //}

    // Debating on making it so that when it leaves the presence, it removes render.
    //private void OnTriggerExit(Collider collider)
    //{
    //    if (_outObj != null)
    //        _outObj.RenderOject(collider.gameObject);
    //}
}
