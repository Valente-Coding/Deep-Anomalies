using UnityEngine;

public class FishManager : MonoBehaviour
{
    [SerializeField] private Camera m_Camera;
    [SerializeField] private FishSpawner m_FishSpawner;
    [SerializeField] private GameObject m_BorderPrefab;
    [SerializeField] private float m_BorderSize = 5f;
    [SerializeField] private float m_BorderMargin = 10f;

    // Start is called before the first frame update
    void Start()
    {
        PlaceBorders();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlaceBorders()
    {
        Vector3 l_LeftBorderPos = m_Camera.ViewportToWorldPoint(new Vector3(0f, 1f, 0f));
        Vector3 l_RightBorderPos = m_Camera.ViewportToWorldPoint(new Vector3(1f, 1f, 0f));

        GameObject l_LeftBorder = Instantiate(m_BorderPrefab, transform);
        l_LeftBorder.transform.position = new Vector3(l_LeftBorderPos.x - m_BorderMargin, 0f, 0f);
        l_LeftBorder.transform.localScale = new Vector3(m_BorderSize, l_LeftBorderPos.y*2, 1f);

        GameObject l_RightBorder = Instantiate(m_BorderPrefab, transform);
        l_RightBorder.transform.position = new Vector3(l_RightBorderPos.x + m_BorderMargin, 0f, 0f);
        l_RightBorder.transform.localScale = new Vector3(m_BorderSize, l_RightBorderPos.y*2, 1f);

        l_LeftBorder.GetComponent<TriggerColliderEvent>().TriggerEvents.AddListener(m_FishSpawner.FishGOPool.ReturnToPool);
        l_RightBorder.GetComponent<TriggerColliderEvent>().TriggerEvents.AddListener(m_FishSpawner.FishGOPool.ReturnToPool);
    }
}
