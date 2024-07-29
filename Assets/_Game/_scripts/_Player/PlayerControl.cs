using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Transform m_characterTransform;
    public Transform CharacterTransform => m_characterTransform;
    [SerializeField] private Animator m_playerAnimator;
    public Animator PlayerAnimator => m_playerAnimator;

    [Header("Camera")]
    [SerializeField] private Transform m_cameraTranform;
    public Transform CameraTranform => m_cameraTranform;

    private Vector3 _cameraPrimatyPosition;
    public Vector3 CameraPrimatyPosition => _cameraPrimatyPosition;

    [Header("Physics")]
    [SerializeField] private Rigidbody m_rigidbody;
    public Rigidbody PlayerRigidbody => m_rigidbody;
    [SerializeField] private PlayerColliderController m_playerCollider;
    public PlayerColliderController PlayerCollider => m_playerCollider;
    [SerializeField] private TriggerController m_collisionDetector;
    [SerializeField] private RagdollController m_ragdollController;
    public RagdollController RagdollControl => m_ragdollController;

    [Header("Turn")]
    [SerializeField, Range(0, 360)] private float _turnAngle = 45f;

    [Header("Jump")]
    [SerializeField] private float m_jumpForce;
    public float JumpForce => m_jumpForce;
    [SerializeField] private float m_detectGroundRayLengh;
    public float DetectGroundRayLengh => m_detectGroundRayLengh;
    [SerializeField] private float m_jumpAnimationTime;
    public float JumpAnimationTime => m_jumpAnimationTime;

    [Header("Movement")]
    [SerializeField] private float m_xSpeed;
    [SerializeField] private Vector3 m_deathCameraPosition;
    public Vector3 DeathCameraPosition => m_deathCameraPosition;

    [Header("Sliding")]
    [SerializeField] private float m_slidingDuration;
    public float SlidingDuration => m_slidingDuration;
    [SerializeField] private Vector3 m_cameraSlidePosition;
    public Vector3 CameraSlidePosition => m_cameraSlidePosition;
    [SerializeField] private float m_slideCameraAnimDuration = .1f;
    public float SlideCameraAnimDuration => m_slideCameraAnimDuration;

    // Death
    public delegate void OnPlayerDied(DeathType cause, object felonObject = null);
    public OnPlayerDied Entity_OnPlayerDied;

    private bool _canMove = true;
    private PlayerStateMachine _stateMachine;
    public bool CanMove
    {
        get => _canMove;
        set => _canMove = value;
    }


    private void Start()
    {
        _stateMachine = new PlayerStateMachine(this);
        _stateMachine.ChangeState(_stateMachine.RunningState);
        _cameraPrimatyPosition = m_cameraTranform.localPosition;
        InitalizeAnimations();
    }

    private void InitalizeAnimations()
    {
        PlayerAnimationsManager.Instance.SetAnimationHandler(m_playerAnimator);
    }

    private void Update()
    {
        SetMovement();
    }

    private float GetInputsValue()
    {
        if (InputsManager.Instance.GetXDirection().Equals(0))
            return 0;

        return InputsManager.Instance.GetXDirection() * GameManager.Instance.MomentumMask;
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, out _, m_detectGroundRayLengh);
    }

    private void SetMovement()
    {
        PlayerAnimationsManager.Instance.SetAction(AnimatorActionType.FLOAT, PlayerAnimationNames.MomentumFloat, GameManager.Instance.MomentumMask);
        SetTheTurnRotation();
        var verticalSpeed = _canMove ? m_xSpeed : 0;
        m_rigidbody.velocity = new Vector3(verticalSpeed * GetInputsValue(), m_rigidbody.velocity.y, m_rigidbody.velocity.z);
    }


    private void SetTheTurnRotation()
    {
        var angle = _canMove ? Mathf.Clamp(_turnAngle * GetInputsValue(), -_turnAngle, _turnAngle) : 0;
        SetModelRotateToAngle(angle);
    }

    public void SetModelRotateToAngle(float angle)
    {
        m_characterTransform.DOLocalRotate(new Vector3(0, angle, 0), 1, RotateMode.Fast);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (m_collisionDetector.IsDetecting && collision.collider.gameObject.Equals(m_collisionDetector.DetectingObject))
        {
            Entity_OnPlayerDied?.Invoke(DeathType.BY_COLLISION);
        }
    }

    public void DeathByExplotion(Transform explotionObject)
    {
        Entity_OnPlayerDied?.Invoke(DeathType.BY_EXPLOSION, explotionObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, Vector3.down * m_detectGroundRayLengh);
    }
}
