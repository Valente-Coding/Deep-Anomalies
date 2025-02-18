using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishHandler : MonoBehaviour
{
    private FishSO m_FishData;
    private float m_Direction = 1f;
    private float m_Speed = 1f;

    public float Speed { get => m_Speed; set => m_Speed = value; }
    public float Direction { get => m_Direction; set => m_Direction = value; }
    public FishSO FishData { get => m_FishData; set => m_FishData = value; }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.Translate(new Vector3(m_Speed * m_Direction * Time.fixedDeltaTime, 0, 0));
    }
}
