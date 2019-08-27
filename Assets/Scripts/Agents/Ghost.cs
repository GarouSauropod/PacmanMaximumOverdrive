using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridUtilities;

public class Ghost : MonoBehaviour
{
    bool inHoldingPen;
    Path path;
    Square currentSquare;
    Square targetSquare;
    Square primeObjective;

    float speed = 2.5f;
    float step;

    public void Initialize(Square _initialSquare)
    {
        currentSquare = _initialSquare;
        targetSquare = _initialSquare;
        CalculatePath(currentSquare, targetSquare);
    }

    void Update()
    {
        if (path.squareList.Count > 0)
        {
            targetSquare = path.squareList[0];
        }

        step = speed * Time.deltaTime * Gameclock.instance.GetTimeFactor();
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetSquare.position.x, transform.position.y, targetSquare.position.z), step);

        if (HasReachedCenterSquare(targetSquare))
        {
            currentSquare = targetSquare;
            UpdatePrimeObjective();
            CalculatePath(currentSquare, primeObjective);
        }
    }

    public bool HasReachedCenterSquare(Square _targetSquare)
    {
        if (Mathf.Abs(transform.position.x - _targetSquare.position.x) <= 0.05f && Mathf.Abs(transform.position.z - _targetSquare.position.z) <= 0.05f)
        {
            return true;
        }
        return false;
    }

    public void CalculatePath(Square _start, Square _destination)
    {
        //path = PathFinder.Instance.FindPath(_start, _destination);
        path = PathFinder.Instance.FindPathAStar(BoardManager.instance.grid ,_start, _destination);
    }

    private void UpdatePrimeObjective()
    {
        primeObjective = BoardManager.instance.GetPacmanSquare();
    }

    void OnCollisionEnter(Collision _other)
    {
        if (_other.gameObject.tag == "Player")
        {
            GameEventManager.TriggerEvent("onGhostCatchingPlayer");
        }
    }

}
