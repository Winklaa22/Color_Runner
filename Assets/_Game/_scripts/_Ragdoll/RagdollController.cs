using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RagdollController : MonoBehaviour
{
    [SerializeField] private Rigidbody[] _partsOfRagdoll;
    [SerializeField] private List<Collider> _colliders;

    [Header("Part of ragdoll")]
    [SerializeField] private RagdollPart[] m_parts;

    [SerializeField] private float m_shootForce = 30;

    private void Start()
    {
        _partsOfRagdoll = GetComponentsInChildren<Rigidbody>();
        foreach(var part in m_parts)
        {
            _colliders.Add(part.PhysicPart.GetComponent<Collider>());
        }
        SetActive(false);
    }


    public void SetActive(bool active)
    {
        for (var i = 1; i < _partsOfRagdoll.Length; i++)
        {
            _partsOfRagdoll[i].isKinematic = !active;
            foreach(var collider in _colliders)
            {
                collider.enabled = active;
            }
        }
    }

    public RagdollPart GetRagdollPart(string partName)
    {
        foreach (var part in m_parts)
        {
            if (!part.Name.Equals(partName))
                continue;

            return part;
        }

        return null;
    }

    public void SetShootPart(RagdollPart ragdollPart)
    {
        ragdollPart.PhysicPart.AddForce(Vector3.left * m_shootForce);
    }

    public void Explosion(Transform explotionObject)
    {
        foreach (var part in _partsOfRagdoll)
        {
            part.AddExplosionForce(500, explotionObject.position, 8.0f);
        }
    }


}

[System.Serializable]
public class RagdollPart
{
    public string Name;
    public Rigidbody PhysicPart;
}

