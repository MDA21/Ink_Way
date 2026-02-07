using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("ºËÐÄ×é¼þ")]
    public PlayerMovement movement;
    public Renderer meshRenderer;
    public Rigidbody rb;
    public Collider col;

    [Header("Ink Check")]
    [SerializeField] private Transform inkCheck;
    [SerializeField] private float inkCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckRadius;
    [SerializeField] private LayerMask whatIsInk;
    [SerializeField] private LayerMask whatIsWall;

    [Header("Sink InputAction")]
    private PlayerInputAction _inputActions;
    private InputAction _sinkAction;

    private bool isInInk { get; set; }
    public PlayerStateMachine playerStateMachine { get; private set; }
    public Player_NormalState normalState { get; private set; }
    public Player_SinkState sinkState { get; private set; }

    private void Awake()
    {
        playerStateMachine = new PlayerStateMachine();
        normalState = new Player_NormalState(playerStateMachine, this, "Normal");
        sinkState = new Player_SinkState(playerStateMachine, this, "Sink");

        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        meshRenderer = GetComponentInChildren<Renderer>();
        movement = GetComponent<PlayerMovement>();

        InitRigidbody3D();        
    }

    private void OnEnable()
    {    

    }

    private void Start()
    {
        playerStateMachine.Initialize(normalState);
    }

    private void Update()
    {
        playerStateMachine.activateCurrentState();
    }

    private void CheckCollision()
    {

    }



    public bool IsInkDetected()
    {
        return Physics.Raycast(inkCheck.position, Vector3.down, inkCheckDistance, whatIsInk);
    }

    public bool IsWallDetected()
    {
        return Physics.CheckSphere(wallCheck.position, wallCheckRadius, whatIsWall);
    }

    private void InitRigidbody3D()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
            if (rb == null) rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.useGravity = true;
        rb.freezeRotation = true;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }


    private void OnDrawGizmosSelected()
    {
        if (inkCheck != null)
        {
            Gizmos.DrawLine(inkCheck.position, inkCheck.position + Vector3.down * inkCheckDistance);
            Gizmos.DrawWireSphere(wallCheck.position, wallCheckRadius);
        }
    }
}
