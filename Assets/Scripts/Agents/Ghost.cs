using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridUtilities;

public class Ghost : MonoBehaviour
{
    protected bool inHoldingPen;
    protected Path path;
    protected Square currentSquare;
    protected Square targetSquare;
    protected Square primeObjective;

    protected float speed = 2.5f;
    protected float step;

    public virtual void Initialize(Square _initialSquare)
    {
        currentSquare = _initialSquare;
        targetSquare = _initialSquare;
        CalculatePath(currentSquare, targetSquare);

        GameEventManager.StartListening("onGhostPenOpen", OpenPen);
    }

    void Update()
    {
        if (inHoldingPen) { return; }

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

    public virtual void CalculatePath(Square _start, Square _destination)
    {
        path = PathFinder.Instance.FindPathAStar(BoardManager.instance.grid ,_start, _destination);
    }

    private void UpdatePrimeObjective()
    {
        primeObjective = BoardManager.instance.GetPacmanSquare();
    }

    private void OpenPen(object _arg)
    {
        inHoldingPen = false;
    }

    void OnCollisionEnter(Collision _other)
    {
        if (_other.gameObject.tag == "Player")
        {
            GameEventManager.TriggerEvent("onGhostCatchingPlayer");
        }
    }

}
