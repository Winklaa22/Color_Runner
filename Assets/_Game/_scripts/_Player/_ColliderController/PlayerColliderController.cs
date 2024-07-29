using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerColliderController : MonoBehaviour
{
    [SerializeField] private CapsuleCollider m_playerCollider;
    [SerializeField] private ColliderData[] m_colliderDataStates;

    public void SetActive(bool active) => m_playerCollider.enabled = active;

    public void ChangeState(PlayersColliderState state)
    {
        if (!m_colliderDataStates.Any(x => x.State == state))
            return;

        var data = m_colliderDataStates.First(x => x.State == state);
        m_playerCollider.center = data.Center;
        m_playerCollider.height = data.Height;
        m_playerCollider.radius = data.Radius;
    }

}
