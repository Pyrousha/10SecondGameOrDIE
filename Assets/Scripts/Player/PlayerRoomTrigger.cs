using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoomTrigger : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float cameraSpeed;

    private Collider2D currRoom;
    private Coroutine currLerp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == currRoom)
            return;

        currRoom = collision;
        if (currLerp != null)
            StopCoroutine(currLerp);
        currLerp = StartCoroutine(CameraLerp(collision.transform.position));
    }

    private IEnumerator CameraLerp(Vector3 targPos)
    {
        targPos.z = cameraTransform.position.z;

        float dist;
        Vector3 toMove;

        while(true)
        {
            dist = Vector3.Distance(cameraTransform.position, targPos);

            toMove = (targPos - cameraTransform.position) * cameraSpeed * Time.deltaTime;

            if ((toMove.magnitude >= dist) || (dist <= 0.01f)) //close enough
            {
                cameraTransform.position = targPos;
                break;
            }

            //Still got a ways to go
            cameraTransform.position += toMove;

            yield return null;
        }

        yield return null;
    }
}
