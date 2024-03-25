using UnityEngine;

[System.Serializable]
public class Platform
{
    [SerializeField] private float m_probilityNumber;
    [SerializeField] private GameObject m_model;

    public float ProbabilityNumber => m_probilityNumber;
    public GameObject Model => m_model;
    public string ID { get; set; }
}
