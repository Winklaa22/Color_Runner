using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    [SerializeField] private GameObject m_object;
    public GameObject DetectingObject => m_object;
    public bool IsDetecting
    {
        get
        {
            return m_object != null;
        }
    }
    [SerializeField] private string[] m_ignoreTags;
    public delegate void OnEnter(Collider other);
    public event OnEnter OnEnter_Entity;

    private void OnTriggerEnter(Collider other)
    {
        if (m_ignoreTags.Length > 0 && m_ignoreTags.Any(tag => other.CompareTag(tag)))
            return;


        m_object = other.gameObject;
        OnEnter_Entity?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        m_object = null;
    }
}
