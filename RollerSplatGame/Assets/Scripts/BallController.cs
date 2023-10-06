using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    public Rigidbody rb;
    public float speed = 15.0f;

    private bool isTraveling;
    private Vector3 travelDirection;
    private Vector3 nextCollisionPosition;

    public int minSwipeRecognition = 500;
    private Vector2 swipePosLastFrame;
    private Vector2 swipePosCurrentFrame;
    private Vector2 currentSwipe;
    // Start is called before the first frame update
   

    private void FixedUpdate()
    {
        if (isTraveling)
        {
            rb.velocity = speed * travelDirection;
        }

        if(nextCollisionPosition != Vector3.zero)
        {
            if(Vector3.Distance(transform.position, nextCollisionPosition) < 1)
            {
                isTraveling = false;
                travelDirection = Vector3.zero;
                nextCollisionPosition = Vector3.zero;
            }
        }

        if (isTraveling)
        {
            return; // exits the FixedUpdate method and doesn't execute any of the lines of code below this point
        }

        if (Input.GetMouseButton(0))
        {
            swipePosCurrentFrame = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            if(swipePosLastFrame != Vector2.zero)
            {
                currentSwipe = swipePosCurrentFrame - swipePosLastFrame;

                if(currentSwipe.sqrMagnitude < minSwipeRecognition)
                {
                    return;
                }
                currentSwipe.Normalize();

                //UP/DOWN
                if(currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    //GO UP/DOWN
                    SetDestination(currentSwipe.y > 0 ? Vector3.forward : Vector3.back);
                }
                if (currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    //GO LEFT/RIGHT
                    SetDestination(currentSwipe.x > 0 ? Vector3.right : Vector3.left);
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            swipePosLastFrame = Vector2.zero;
            currentSwipe = Vector2.zero;
        }
    }

    private void SetDestination(Vector3 direction)
    {
        travelDirection = direction;
        RaycastHit hit;
        if(Physics.Raycast(transform.position, direction, out hit, 100f))
        {
            nextCollisionPosition = hit.point;
        }
    }
}
