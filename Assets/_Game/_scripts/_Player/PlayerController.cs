using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform m_characterTransform;
    
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
    
    // Start is called before the first frame update
    void Start()
    {
        InputsManager.Instance.OnDoubleTapAction += Jump;
    }

    private void Jump()
    {
        if (!GameManager.Instance.IsMoving || !IsGrounded()) 
            return;
        
        m_rigidbody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        SetMovement();
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
