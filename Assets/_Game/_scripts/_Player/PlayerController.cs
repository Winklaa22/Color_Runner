using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform m_characterTransform;
    [SerializeField] private Animator m_playerAnimator;
    
    [Header("Camera")]
    [SerializeField] private Transform m_cameraTranform;
    private Vector3 _cameraPrimatyPosition;

    [Header("Physics")]
    [SerializeField] private Rigidbody m_rigidbody;
    [SerializeField] private CapsuleCollider m_playerCollider;
    [SerializeField] private TriggerController m_collisionDetector;
    [SerializeField] private RagdollController m_ragdollController;
    
    [Header("Turn")]
    [SerializeField, Range(0, 360)] private float _turnAngle = 45f;

    [Header("Jump")] 
    [SerializeField] private float m_jumpForce;
    [SerializeField] private float m_detectGroundRayLengh;
    [SerializeField] private float m_jumpAnimationTime;
    
    [Header("Movement")]
    [SerializeField] private float m_xSpeed;
    [SerializeField] private Vector3 m_deathCameraPosition;

    [Header("Sliding")]
    [SerializeField] private float m_slidingDuration;
    [SerializeField] private Vector3 m_cameraSlidePosition;
    [SerializeField] private float m_slideCameraAnimDuration = .1f;

    private bool _canMove = true;
    private bool _isSliding;
    private bool _isJumping;

    // Start is called before the first frame update
    void Start()
    {
        InputsManager.Instance.OnTouchEnd += OnTouchEnded;
        _cameraPrimatyPosition = m_cameraTranform.localPosition;
        InitalizeAnimations();
    }

    private void InitalizeAnimations()
    {
        PlayerAnimationsManager.Instance.SetAnimationHandler(m_playerAnimator);
    }

    private void OnTouchEnded()
    {
        if (InputsManager.Instance.GetYDirection() == 1)
        {
            if (!_isSliding)
                StartCoroutine(Jump());
            else
                EndSlide();
        }

        if (InputsManager.Instance.GetYDirection() == -1)
        {
            StartCoroutine(Slide());
        }
    }

    private IEnumerator Slide()
    {
        if (!GameManager.Instance.IsMoving || !IsGrounded() || _isJumping)
            yield break;

        _isSliding = true;
        _canMove = false;
        m_cameraTranform.DOLocalMove(m_cameraSlidePosition, m_slideCameraAnimDuration);
        PlayerAnimationsManager.Instance.SetAction(AnimatorActionType.BOOL, PlayerAnimationNames.SlidingBool, true);
        yield return new WaitForSeconds(m_slidingDuration);
        EndSlide();
    }

    private void EndSlide()
    {
        PlayerAnimationsManager.Instance.SetAction(AnimatorActionType.BOOL, PlayerAnimationNames.SlidingBool, false);
        m_cameraTranform.DOLocalMove(_cameraPrimatyPosition, m_slideCameraAnimDuration);
        _isSliding = false;
        _canMove = true;
    }

    private IEnumerator Jump()
    {
        if (!GameManager.Instance.IsMoving || !IsGrounded())
            yield break;

        if (_isSliding)
            EndSlide();

        _isJumping = true;
        PlayerAnimationsManager.Instance.SetAction(AnimatorActionType.BOOL, PlayerAnimationNames.JumpingBool, true);
        
        
        m_rigidbody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
        yield return new WaitForSeconds(m_jumpAnimationTime - 0.3f);

        PlayerAnimationsManager.Instance.SetAction(AnimatorActionType.BOOL, PlayerAnimationNames.JumpingBool, false);
        _isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        SetMovement();
        PlayerAnimationsManager.Instance.SetAction(AnimatorActionType.BOOL, PlayerAnimationNames.FallingBool, !IsGrounded());
    }
    
    
    private float GetInputsValue()
    {
        if(InputsManager.Instance.GetXDirection().Equals(0))
            return 0;
            
        return InputsManager.Instance.GetXDirection() * GameManager.Instance.MomentumMask;
    }

    private bool IsGrounded()
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
            StartCoroutine(Death(false, null));
        }
    }

    public void DeathByExplotion(Transform explotionObject)
    {
        StartCoroutine(Death(true, explotionObject));
    }

    private IEnumerator Death(bool byExplotion, Transform explotionObject)
    {
        m_cameraTranform.DOLocalMove( m_deathCameraPosition, 2).SetEase(Ease.InOutExpo);

        m_playerAnimator.enabled = false;
        m_playerCollider.enabled = false;
        m_rigidbody.isKinematic = true;
        m_ragdollController.SetActive(true);

        if(byExplotion)
            m_ragdollController.Explosion(explotionObject);

        ScreensManager.Instance.CloseLastScreen();

        yield return new WaitForSeconds(2f);
        ScreensManager.Instance.OpenScreen(ScreenType.DEATH_SCREEN);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, Vector3.down * m_detectGroundRayLengh);
    }
}
