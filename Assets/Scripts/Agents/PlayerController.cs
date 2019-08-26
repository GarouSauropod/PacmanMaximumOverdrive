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

    public void Initialize(Square _initialSquare)
    {
        currentTile = _initialSquare;
    }
	
	void Update()
    {
        axisXTranslation = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        axisZTranslation = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        movementVector = new Vector3(axisXTranslation, 0, axisZTranslation);

        transform.Translate(movementVector, Space.World);
        TurnTowardsDirection(movementVector);
        UpdateCurrentSquare();
    }

    private void TurnTowardsDirection(Vector3 moveDirection)
    {
        moveDirection.Normalize();
        Vector3 currentDirection = transform.forward;
        Vector3 newDir = Vector3.RotateTowards(currentDirection, moveDirection, 0.03f, 0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    private void UpdateCurrentSquare()
    {
        if (transform.position.x > (currentTile.position.x + 0.5f))
        {
            currentTile = currentTile.rightNeighbor;
        }
        else if (transform.position.x < (currentTile.position.x - 0.5f))
        {
            currentTile = currentTile.leftNeighbor;
        }
        else if (transform.position.z > (currentTile.position.z + 0.5f))
        {
            currentTile = currentTile.topNeighbor;
        }
        else if (transform.position.z < (currentTile.position.z - 0.5f))
        {
            currentTile = currentTile.bottomNeighbor;
        }
    }

    public Square GetCurrentSquare()
    {
        return currentTile;
    }

    //Add collision detection with obstacles so pacman doesn't try to interpenetrate barriers

}
