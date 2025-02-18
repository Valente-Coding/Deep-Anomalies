using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [Header("Spawner")]
    [SerializeField] private Camera m_Camera;
    [Range(0,1)][SerializeField] private float m_SpawnHeight = 1f;
    [Range(-1,1)][SerializeField] private float m_HeightMargin = 0f;
    [SerializeField] private int m_FishesAmount = 10;
    [SerializeField] private float m_SpawnMargin = 10f;

    [Header("Fish")]
    [SerializeField] private GameObjectPool m_FishGOPool;

    public GameObjectPool FishGOPool { get => m_FishGOPool; set => m_FishGOPool = value; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject SpawnFish(FishSO p_FishData, int p_Edge)
    {
        GameObject l_Fish = FishGOPool.GetFromPool(GetSpawnPosition(p_Edge), Quaternion.identity);

        FishHandler l_FishHandler = l_Fish.GetComponent<FishHandler>();
        l_FishHandler.FishData = p_FishData;
        l_FishHandler.Direction = p_Edge == 0 ? 1f : -1f;
        l_FishHandler.Speed = p_FishData.Speed;
        l_FishHandler.enabled = true;

        SpriteRenderer l_FishSprite = l_Fish.GetComponent<SpriteRenderer>();
        l_FishSprite.sprite = p_FishData.FishSprite;
        l_FishSprite.flipX = p_Edge == 1 ? true : false;
        l_FishSprite.enabled = true;

        BoxCollider2D l_FishCollider = l_Fish.GetComponent<BoxCollider2D>();
        l_FishCollider.size = l_FishSprite.sprite.bounds.size;
        l_FishCollider.enabled = true;

        return l_Fish;
    }



    private Vector3 GetSpawnPosition(int p_Edge)
    {
        // p_Edge = (0 = left, 1 = right)
        Vector3 l_SpawnAreaPosition = GetSpawnAreaPosition();
        Vector3 l_SpawnAreaSize = GetSpawnAreaSize();

        float l_HorizontalSpawnPos = p_Edge == 1 ? l_SpawnAreaPosition.x + l_SpawnAreaSize.x/2 : l_SpawnAreaPosition.x - l_SpawnAreaSize.x/2;
        float l_VerticalSpawnPos = Random.Range(l_SpawnAreaPosition.y - l_SpawnAreaSize.y/2, l_SpawnAreaPosition.y + l_SpawnAreaSize.y/2);
        Vector3 l_SpawnPos = new Vector3(l_HorizontalSpawnPos, l_VerticalSpawnPos, 0f);
        l_SpawnPos.x += p_Edge == 0 ? -m_SpawnMargin : m_SpawnMargin; 
        
        return l_SpawnPos;
    }

    private Vector3 GetSpawnAreaPosition() 
    {
        Vector3 l_OffsetInWorldPos = m_Camera.ViewportToWorldPoint(new Vector3(1f, -m_HeightMargin, 0f)) - transform.position;
        Vector3 l_CameraTopInWorldPos = m_Camera.ViewportToWorldPoint(new Vector3(0f, 1f, 0f));

        return l_CameraTopInWorldPos + l_OffsetInWorldPos;
    }

    private Vector3 GetSpawnAreaSize() 
    {
        Vector3 l_CameraSize = m_Camera.ViewportToWorldPoint(new Vector3(1f, 1f, 0f)) - transform.position;
        Vector3 l_SquareSize = new Vector3(l_CameraSize.x*2, l_CameraSize.y*2 * m_SpawnHeight, 0f);

        return l_SquareSize;
    }

    void OnDrawGizmosSelected()
    {
        Vector3 l_SpawnAreaPosition = GetSpawnAreaPosition();
        Vector3 l_SpawnAreaSize = GetSpawnAreaSize();

        // Draw a yellow cube at the transform position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(l_SpawnAreaPosition, l_SpawnAreaSize);
    }
}
