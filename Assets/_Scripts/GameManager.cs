using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveInfo
{
    public Transform element;
    public Vector3 elemPos;

    
}

public class TickInfo
{
    public List<MoveInfo> tickElements = new List<MoveInfo>();
}


public class GameManager : Singleton<GameManager>
{
    public Stack<TickInfo> moves;

    //Debug
    public TickInfo move = new TickInfo();

    public TickInfo tickMoves;

    public float tick = 0.5f;
    public float coolDown = 1f;
    public bool SaveTickInProgress = false;
    public bool RevertInProgress = false;

    private void Start()
    {
        moves = new Stack<TickInfo>();
        tickMoves = new TickInfo();
    }

    private void Update()
    {
        if(SaveTickInProgress)
        {
            coolDown += Time.deltaTime;
            if(coolDown>tick)
            {
                Debug.Log("TS>" + tickMoves.tickElements.Count);
                //moves.Push(tickMoves);
                move = tickMoves;
                
                //Debug.Log("T>" + moves.Peek().tickElements.Count);
                coolDown = 0;
                SaveTickInProgress = false;
            }
        }
      
    }

    public void SaveCubePosition(Transform cube, Vector3 position)
    {
        if(!RevertInProgress)
        {
            SaveTickInProgress = true;

            if (coolDown > tick)
                tickMoves.tickElements.Clear();


            MoveInfo tmpInfo = new MoveInfo();
            tmpInfo.element = cube;
            tmpInfo.elemPos = position;
            tickMoves.tickElements.Add(tmpInfo);
            Debug.Log("S>" + tickMoves.tickElements.Count);
        }
       
    }


    public void RevertMove()
    {
        if(move != null /*moves.Count > 0*/)
        {
            RevertInProgress = true;
            StartCoroutine(StopRevertInProgress());
            TickInfo tmpInfo = new TickInfo();
            //tmpInfo = moves.Pop();
            tmpInfo = move;

            //Debug.Log("R>" + moves.Count + " : " + tmpInfo.tickElements.Count);

            foreach (var tickElem in tmpInfo.tickElements)
            {
                tickElem.element.position  = tickElem.elemPos;
            }

            move.tickElements.Clear();
        }
       
    }

    private IEnumerator StopRevertInProgress()
    {
        yield return new WaitForSeconds(0.5f);
        RevertInProgress = false;
    }
}
