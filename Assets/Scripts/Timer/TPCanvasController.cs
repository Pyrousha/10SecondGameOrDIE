using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPCanvasController : Singleton<TPCanvasController>
{
    [SerializeField] private Animator anim;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform cameraTransform;
    private Vector3 playerTargPos;
    private Vector3 cameraTargPos;

    public void SetDestination(Vector3 playerPos, Vector3 camPos)
    {
        playerTargPos = playerPos;
        cameraTargPos = new Vector3(camPos.x, camPos.y, cameraTransform.position.z);
    }

    public void DoTeleport()
    {
        playerTransform.position = playerTargPos;
        cameraTransform.position =  cameraTargPos;
    }

    public void OnTriggerHit()
    {
        anim.SetTrigger("ClearToBlack");
    }
}
