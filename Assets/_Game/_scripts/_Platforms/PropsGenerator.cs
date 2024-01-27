using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsGenerator : MonoBehaviour
{
    [SerializeField] private int[] m_sizeOfMatrix = new int[2];
    private List<List<GameObject>> matrix = new List<List<GameObject>>();


    private void OnDrawGizmos()
    {
        for (int x = -m_sizeOfMatrix[0]/2; x < m_sizeOfMatrix[0]/2; x++)
        {
            for (int y = -m_sizeOfMatrix[1]/2; y < m_sizeOfMatrix[1]/2; y++)
            {
                Gizmos.DrawWireCube(new Vector3(transform.position.x + x, 0, transform.position.z + y), Vector3.one);
            }
        }


    }
}
