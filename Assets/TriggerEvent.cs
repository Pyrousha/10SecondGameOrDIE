using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent eventToDo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        eventToDo.Invoke();
    }
}
