using System.Collections;
using System.Collections.Generic;
using GridUtilities;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Vector3 movementVector;
    float axisXTranslation;
    float axisZTranslation;

    [SerializeField]
    float moveSpeed = 3f;

    private Square currentTile;
    private float angleBetween = 0.0f;
	
	void Update()
    {
        axisXTranslation = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        axisZTranslation = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        movementVector = new Vector3(axisXTranslation, 0, axisZTranslation);

        transform.Translate(movementVector, Space.World);
        TurnTowardsDirection(movementVector);
    }

    public void TurnTowardsDirection(Vector3 moveDirection)
    {
        //Refine the turning at some point so it turns more slowly in the beginning and faster once the movement is started
        moveDirection.Normalize();
        Vector3 currentDirection = transform.forward;
        Vector3 newDir = Vector3.RotateTowards(currentDirection, moveDirection, 0.03f, 0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

}
