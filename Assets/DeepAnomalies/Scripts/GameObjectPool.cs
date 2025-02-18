using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject m_Prefab;
    [SerializeField] private int m_InitialSize = 10;
    private Queue<GameObject> m_Pool = new Queue<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < m_InitialSize; i++)
        {
            GameObject l_Obj = Instantiate(m_Prefab, transform);
            l_Obj.SetActive(false);
            m_Pool.Enqueue(l_Obj);
        }
    }

    public GameObject GetFromPool(Vector3 p_Position, Quaternion p_Rotation)
    {
        GameObject l_Obj;

        if (m_Pool.Count == 0)
        {
            l_Obj = Instantiate(m_Prefab);
        }
        else
        {
            l_Obj = m_Pool.Dequeue();
        }

        l_Obj.transform.position = p_Position;
        l_Obj.transform.rotation = p_Rotation;
        l_Obj.SetActive(true);

        return l_Obj;
    }

    public void ReturnToPool(GameObject p_Obj)
    {
        p_Obj.transform.SetParent(transform);
        p_Obj.transform.localPosition = Vector3.zero;
        p_Obj.SetActive(false);
        m_Pool.Enqueue(p_Obj);
    }
}
