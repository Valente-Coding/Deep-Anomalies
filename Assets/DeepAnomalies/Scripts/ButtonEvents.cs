using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private UnityEvent m_OnHoverEnter;
    [SerializeField] private UnityEvent m_OnHoverExit;

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_OnHoverEnter.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_OnHoverExit.Invoke();
    }
}
