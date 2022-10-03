using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D heroRB;
    [SerializeField] private Animator anim;
    [SerializeField] private Animator heartAnim;
    [SerializeField] private float maxMoveSpeed;
    [SerializeField] private float accelSpeed;
    [SerializeField] private float frictionSpeed;

    [SerializeField] private GameObject mainCam;
    [SerializeField] private GameObject endCam;
    private Vector3 velocity;

    [SerializeField] private Transform targPos;

    public bool canMove { get; set; }

    Vector2 inputVect;

    float scale = 0.5f;

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Vector3 dirToTarg = targPos.position - transform.position;
            inputVect = new Vector2(dirToTarg.x/scale, dirToTarg.y/scale);
            if(inputVect.magnitude > 1)
            {
                inputVect.Normalize();
            }
            else
            {
                if(inputVect.magnitude <= 0.1f)
                {
                    canMove = false;
                    heartAnim.enabled = true;

                    StartCoroutine(HeroReached());
                }
            }
        }
        else
            inputVect = Vector2.zero;
    }

    IEnumerator HeroReached()
    {
        yield return new WaitForSeconds(3f);

        heartAnim.SetTrigger("ByeHeart");

        anim.SetTrigger("Die");
        PlayerController.Instance.Anim.SetTrigger("Die");
        PlayerController.Instance.transform.localScale = new Vector3(-1, 1, 1);

        yield return new WaitForSeconds(2);

        TPCanvasController.Instance.Anim.SetTrigger("ClearToBlack");

        yield return new WaitForSeconds(10f / 60f);

        endCam.SetActive(true);
        mainCam.SetActive(false);
    }

    private void FixedUpdate()
    {
        //rb.velocity = InputHandler.Instance.Direction * moveSpeed;

        ApplyFrictionAndAcceleration();

        anim.SetFloat("HSpeed", heroRB.velocity.x + 0.2f * heroRB.velocity.y);
    }

    public void ApplyFrictionAndAcceleration()
    {
        //This is only called when the player is grounded

        float currSpeedX = velocity.x;
        float currSpeedZ = velocity.z;

        float newSpeedX = currSpeedX;
        float newSpeedZ = currSpeedZ;

        #region Apply Friction
        //X-Friction
        if (currSpeedX < 0) //moving left
        {
            newSpeedX = Mathf.Min(0, currSpeedX + frictionSpeed);
        }
        else
        {
            if (currSpeedX > 0) //moving right
            {
                newSpeedX = Mathf.Max(0, currSpeedX - frictionSpeed);
            }
        }

        //Z-Friction
        if (currSpeedZ < 0) //moving left
        {
            newSpeedZ = Mathf.Min(0, currSpeedZ + frictionSpeed);
        }
        else
        {
            if (currSpeedZ > 0) //moving right
            {
                newSpeedZ = Mathf.Max(0, currSpeedZ - frictionSpeed);
            }
        }
        #endregion

        #region Apply Acceleration
        //X-Acceleration
        if (inputVect.x < 0) //pressing left
        {
            if (currSpeedX > maxMoveSpeed * inputVect.x) //can accelerate more left
            {
                //accelerate left
                newSpeedX = Mathf.Max(currSpeedX - accelSpeed, maxMoveSpeed * inputVect.x);
            }
        }
        else
        {
            if (inputVect.x > 0) //pressing right
            {
                if (currSpeedX < maxMoveSpeed * inputVect.x) //can accelerate more right
                {
                    //accelerate right
                    newSpeedX = Mathf.Min(currSpeedX + accelSpeed, maxMoveSpeed * inputVect.x);
                }
            }
        }

        //Z-Acceleration
        if (inputVect.y < 0) //pressing down
        {
            if (currSpeedZ > maxMoveSpeed * inputVect.y) //can accelerate more down
            {
                //accelerate down
                newSpeedZ = Mathf.Max(currSpeedZ - accelSpeed, maxMoveSpeed * inputVect.y);
            }
        }
        else
        {
            if (inputVect.y > 0) //pressing up
            {
                if (currSpeedZ < maxMoveSpeed * inputVect.y) //can accelerate more up
                {
                    //accelerate up
                    newSpeedZ = Mathf.Min(currSpeedZ + accelSpeed, maxMoveSpeed * inputVect.y);
                }
            }
        }
        #endregion

        velocity.x = newSpeedX;
        velocity.z = newSpeedZ;

        heroRB.velocity = new Vector2(velocity.x, velocity.z);
    }
}
