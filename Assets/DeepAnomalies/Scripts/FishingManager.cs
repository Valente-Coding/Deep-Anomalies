using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FishingManager : MonoBehaviour
{
    [SerializeField] private GameObject m_Line;
    [SerializeField] private GameObject m_Hook;
    [SerializeField] private float m_HookSpeed = 1f;
    [SerializeField] private float m_WireMaxLength = 5f;

    private Vector3 m_InitialPosition;
    private GameObject m_HookedFish;

    public UnityEvent<GameObject> OnFishCollected;

    private void Start() {
        m_InitialPosition = m_Hook.transform.position;
        m_Hook.GetComponent<HookCollider>().OnHook.AddListener(FishHooked);
    }

    public void MoveHook(float p_MoveAmount)
    {
        Vector3 l_NewMovement = new Vector3(0f, m_HookSpeed * p_MoveAmount * Time.deltaTime, 0f);
        Vector3 l_NewPosition = m_Hook.transform.position + l_NewMovement;

        if (Mathf.Abs(m_InitialPosition.y - l_NewPosition.y) > m_WireMaxLength) return;
        if (m_InitialPosition.y < l_NewPosition.y) 
        {
            if (m_HookedFish)
                CollectFish();
                
            return;
        }

        m_Hook.transform.Translate(l_NewMovement);
        
        m_Line.transform.Translate(l_NewMovement/2);
        m_Line.transform.localScale = new Vector3(m_Line.transform.localScale.x, m_Line.transform.localScale.y - l_NewMovement.y, m_Line.transform.localScale.z);
    }

    private void FishHooked(GameObject p_FishGO)
    {
        if (m_HookedFish != null) return;

        m_HookedFish = p_FishGO;
        m_HookedFish.GetComponent<FishHandler>().enabled = false;
        m_HookedFish.GetComponent<BoxCollider2D>().enabled = false;
        m_HookedFish.transform.SetParent(m_Hook.transform);
        m_HookedFish.transform.localEulerAngles = new Vector3(0f, 0f, 90f);

        SpriteRenderer l_FishSprite = m_HookedFish.GetComponent<SpriteRenderer>();
        l_FishSprite.flipX = false;
        m_HookedFish.transform.localPosition = new Vector3(0f, -l_FishSprite.bounds.size.x, 0f);

    }

    private void CollectFish()
    {
        OnFishCollected.Invoke(m_HookedFish);
        m_HookedFish = null;
    }
}
