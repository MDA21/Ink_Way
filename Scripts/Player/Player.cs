using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("ºËÐÄ×é¼þ")]
    public PlayerMovement movement;
    public Renderer meshRenderer;
    public Rigidbody rb;
    public Collider col;

    public PlayerStateMachine stateMachine { get; private set; }
    public Player_NormalState normalState { get; private set; }
    public Player_SinkState sinkState { get; private set; }

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();
        normalState = new Player_NormalState(stateMachine, this, "Normal");
        sinkState = new Player_SinkState(stateMachine, this, "Sink");

        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        meshRenderer = GetComponentInChildren<Renderer>();
        movement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        stateMachine.Initialize(normalState);
    }

    private void Update()
    {
        stateMachine.activateCurrentState();
    }


}
