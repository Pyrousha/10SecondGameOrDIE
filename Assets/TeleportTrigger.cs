using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{
    [SerializeField] private Vector3 playerPos;
    [SerializeField] private Vector3 camPos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TPCanvasController.Instance.SetDestination(playerPos, camPos);
        TPCanvasController.Instance.OnTriggerHit();
    }
}
