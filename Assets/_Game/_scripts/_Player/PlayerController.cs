using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform m_characterTransform;
    [SerializeField] private Animator m_playerAnimator;
    
    [Header("Physics")]
    [SerializeField] private Rigidbody m_rigidbody;
    
    [Header("Turn")]
    [SerializeField, Range(0, 360)] private float _turnAngle = 45f;

    [Header("Jump")] 
    [SerializeField] private float m_jumpForce;
    [SerializeField] private float m_detectGroundRayLengh;
    
    [Header("Movement")]
    [SerializeField] private float m_xSpeed;

    private bool _canMove;
    private bool _isJumping;

    // Start is called before the first frame update
    void Start()
    {
        InputsManager.Instance.OnTouchEnd += ()=>
        {
            if(InputsManager.Instance.GetYDirection() == 1)
            {
                StartCoroutine(Jump());
            }
        };
        InitalizeAnimations();
    }

    private void InitalizeAnimations()
    {
        PlayerAnimationsManager.Instance.SetAnimationHandler(m_playerAnimator);
    }

    private IEnumerator Jump()
    {
        if (!GameManager.Instance.IsMoving || !IsGrounded())
            yield break;

        _isJumping = true;
        
        
        m_rigidbody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
        yield return new WaitForSeconds(.3f);

        while (!IsGrounded()){
            yield return null;
        }

        _isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        SetMovement();
        PlayerAnimationsManager.Instance.AnimationsHandler.SetBool(PlayerAnimationNames.FallingBool, !IsGrounded());
        PlayerAnimationsManager.Instance.AnimationsHandler.SetBool(PlayerAnimationNames.JumpingBool, _isJumping);
    }
    
    
    private float GetInputsValue()
    {
        if(InputsManager.Instance.GetXDirection().Equals(0))
            return 0;
            
        return InputsManager.Instance.GetXDirection() * GameManager.Instance.MomentumMask;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, out var hit, m_detectGroundRayLengh);
    }
    
    private void SetMovement()
    {
        PlayerAnimationsManager.Instance.AnimationsHandler.SetFloat(PlayerAnimationNames.MomentumFloat, GameManager.Instance.MomentumMask);
        SetTheTurnRotation();
        m_rigidbody.velocity = new Vector3(m_xSpeed * GetInputsValue(), m_rigidbody.velocity.y, m_rigidbody.velocity.z);
    }
    
    
    private void SetTheTurnRotation()
    {
        var angle = Mathf.Clamp(_turnAngle * GetInputsValue(), -_turnAngle, _turnAngle);
        SetModelRotateToAngle(angle);
    }
    
    public void SetModelRotateToAngle(float angle)
    {
        m_characterTransform.DOLocalRotate(new Vector3(0, angle, 0), 1, RotateMode.Fast);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, Vector3.down * m_detectGroundRayLengh);
    }
}
