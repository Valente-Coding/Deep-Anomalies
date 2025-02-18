using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HookCollider : MonoBehaviour
{
    public UnityEvent<GameObject> OnHook;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        OnHook.Invoke(other.gameObject);
    }
}
