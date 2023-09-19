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
    
    [Header("Movement")]
    [SerializeField] private AnimationCurve m_momentumCurve;
    [SerializeField] private float m_curveMask, m_momentumMask;
    [SerializeField] private float m_speedForward, m_xSpeed;
    private float _momentumCurveTime;
    [SerializeField] private bool _isMoving;
    
    private bool _canMove;
    
    // Start is called before the first frame update
    void Start()
    {
        
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
            
        return InputsManager.Instance.GetXDirection() * m_momentumMask;
    }
    private void SetMovement()
    {
        SetMomentum();
        SetTheTurnRotation();
        var move = transform.right * (m_xSpeed * GetInputsValue()) + transform.forward * GetMovementSpeed();
        m_rigidbody.velocity = move;
    }
    
    private void SetMomentum()
    {
        _momentumCurveTime = m_momentumCurve[m_momentumCurve.length - 1].time;
        var mask = _isMoving ? m_curveMask + Time.deltaTime / _momentumCurveTime : m_curveMask - Time.deltaTime / _momentumCurveTime;
        m_curveMask = Mathf.Clamp(mask, 0, 1);
        m_momentumMask = m_momentumCurve.Evaluate(m_curveMask * m_curveMask);
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

    private float GetMovementSpeed()
    {
        return m_speedForward * m_momentumMask;
    }
}
