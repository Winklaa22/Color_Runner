using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExprosivePropan : MonoBehaviour
{
    [Header("Particles")]
    [SerializeField] private GameObject m_fireParticle;
    [SerializeField] private ParticleSystem m_explosionParticle;

    [SerializeField] private float m_distanceToDetect = 20.0f;
    [SerializeField] private float m_distanceOfDamage = 5.0f;
    [SerializeField] private float m_timeToDestroy;


    private void Update()
    {
        CheckForPlayer();
    }

    private void CheckForPlayer()
    {
        if (Physics.SphereCast(transform.position, m_distanceToDetect, Vector3.back, out var hit))
        {
            if (!hit.collider.CompareTag("Player"))
                return;
            
            StartCoroutine(StartProcess());
 
        }
    }

    private IEnumerator StartProcess()
    {
        m_fireParticle.SetActive(true);
        yield return new WaitForSeconds(m_timeToDestroy);
        Instantiate(m_explosionParticle, transform.position, Quaternion.identity, transform.parent);

        var colliders = Physics.OverlapSphere(transform.position, m_distanceOfDamage);

        foreach(var col in colliders)
        {
            if(col.TryGetComponent(out PlayerController player))
            {
                player.DeathByExplotion(transform);
            }
        }
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, m_distanceToDetect);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_distanceOfDamage);
    }
}
