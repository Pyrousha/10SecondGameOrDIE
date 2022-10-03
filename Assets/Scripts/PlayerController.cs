using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] private Rigidbody2D heroRB;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform interactTransform;
    [SerializeField] private float maxMoveSpeed;
    [SerializeField] private float accelSpeed;
    [SerializeField] private float frictionSpeed;
    private Vector3 velocity;

    private bool canMove = true;
    public void SetCanMove(bool newCanMove)
    {
        canMove = newCanMove;
    }

    private Vector2 inputVect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
            inputVect = InputHandler.Instance.Direction;
        else
            inputVect = Vector2.zero;
    }

    private void FixedUpdate()
    {
        //rb.velocity = InputHandler.Instance.Direction * moveSpeed;

        ApplyFrictionAndAcceleration();

        anim.SetFloat("HSpeed", heroRB.velocity.x + 0.2f*heroRB.velocity.y);
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

        Vector2 angleFacing = new Vector2(velocity.x, velocity.z);
        if (angleFacing.magnitude > 0.2f)
        {
            angleFacing.Normalize();

            float angle = Mathf.Atan2(angleFacing.y, angleFacing.x) - Mathf.Atan2(0, 1);
            angle = angle * 360 / (2 * Mathf.PI);
            interactTransform.localEulerAngles = new Vector3(0, 0, angle);
        }
    }
}
