using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TeleportTrigger : MonoBehaviour
{
    [SerializeField] private Vector3 playerPos;
    [SerializeField] private Vector3 camPos;
    [SerializeField] private UnityEvent optionalEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DoTeleport();
    }

    public void DoTeleport()
    {
        TPCanvasController.Instance.SetDestination(playerPos, camPos);
        TPCanvasController.Instance.OnTriggerHit();

        if (optionalEvent != null)
            optionalEvent.Invoke();
    }
}
