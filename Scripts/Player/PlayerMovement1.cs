using System;
using UnityEngine;
using UnityEngine.InputSystem;

// 3D专属：Input System移动控制 + 速度关联果冻抖动效果
public class PlayerMovement1 : MonoBehaviour
{
    [Header("核心组件")]
    public Rigidbody rb;
    public Renderer meshRenderer;

    [Header("3D移动参数")]
    public float moveSpeed = 3f;  // 玩家移动速度
    [Tooltip("是否忽略相机旋转的Y轴，强制水平移动（建议勾选）")]
    public bool keepHorizontalMove = true;

    [Header("果冻抖动视觉反馈")] public float minWobble = 0.01f;
    public float maxWobble = 0.05f;         // 最高速时的最大抖动强度
    public float maxSpeedForWobble = 3f;  // 达到该速度时触发最大抖动
    
    private PlayerInputAction _inputActions;
    private InputAction _moveAction;
    
    private int _wobbleIntensityID;
    
    private Camera _mainCamera;

    private void Awake()
    {
        InitInputSystem();

        InitRigidbody3D();

        _mainCamera = Camera.main;
    }

    private void Start()
    {
        InitJellyEffect();
    }

    private void OnEnable()
    {
        _moveAction?.Enable();
    }

    private void OnDisable()
    {
        _moveAction?.Disable();
    }

    private void Update()
    {
        UpdateJellyWobble();
    }

    private void FixedUpdate()
    {
        //Update3DMovement();
    }

    #region 初始化方法
    private void InitInputSystem()
    {
        if (_inputActions == null)
        {
            _inputActions = new PlayerInputAction();
        }
        _moveAction = _inputActions.Player.Move;
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
    
    private void InitJellyEffect()
    {
 
        _wobbleIntensityID = Shader.PropertyToID("_WobbleIntensity");

        if (meshRenderer == null)
        {
            meshRenderer = GetComponent<Renderer>();
        }
        // 容错：如果没有渲染器，关闭抖动效果并打印警告
        if (meshRenderer == null)
        {
            Debug.LogWarning($"[{gameObject.name}] 未找到MeshRenderer，果冻抖动效果将失效，请手动拖入！", this);
        }
    }
    #endregion

    #region 3D移动逻辑

    public void Update3DMovement(float moveSpeed)
    {
        if (_moveAction == null || rb == null || _mainCamera == null) return;


        Vector2 moveInput = _moveAction.ReadValue<Vector2>();

        if (moveInput.sqrMagnitude > 1)
        {
            moveInput.Normalize();
        }
        
        Vector3 cameraForward = _mainCamera.transform.forward;
        Vector3 cameraRight = _mainCamera.transform.right;
        
        if (keepHorizontalMove)
        {
            cameraForward.y = 0;
            cameraRight.y = 0;
        }

        cameraForward.Normalize();
        cameraRight.Normalize();
        
        Vector3 moveDir = cameraForward * moveInput.y + cameraRight * moveInput.x;

        rb.velocity = new Vector3(moveDir.x * moveSpeed, rb.velocity.y, moveDir.z * moveSpeed);
    }
    #endregion

    #region 果冻抖动效果

    private void UpdateJellyWobble()
    {

        if (meshRenderer == null || rb == null) return;


        Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        float currentSpeed = horizontalVelocity.magnitude;
        
        float speedPercent = Mathf.Clamp01(currentSpeed / maxSpeedForWobble);
        
        float targetWobble = Mathf.Lerp(minWobble, maxWobble, speedPercent);
        
        meshRenderer.material.SetFloat(_wobbleIntensityID, targetWobble);
    }
    #endregion


    private void OnDestroy()
    {
        if (_inputActions != null)
        {
            _inputActions.Dispose();
        }
    }
}