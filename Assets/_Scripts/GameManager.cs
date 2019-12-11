using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveInfo
{
    public Transform element;
    public Vector3 elemPos;

    
}



public class GameManager : Singleton<GameManager>
{
    //Stack of moves to revert
    public Stack<List<MoveInfo>> moves;
    //MoveInfos per tick
    public List<MoveInfo> tickMoves;

    //Tick specifications
    public float tick = 0.5f;
    public float coolDown = 1f;
    public bool SaveTickInProgress = false;
  

    private void Start()
    {
        moves = new Stack<List<MoveInfo>>();
        tickMoves = new List<MoveInfo>();
    }

    private void Update()
    {
        //If save was initiated - wait until tickTime to save
        if(SaveTickInProgress)
        {
            coolDown += Time.deltaTime;

            //If cooldown is reached - Save every move within one tick, clear data for next tick
            if (coolDown>tick)
            {
                //Add every moveInfo from last tick into temporary container
                List<MoveInfo> tmpList = new List<MoveInfo>();
                foreach (var item in tickMoves)
                {
                    tmpList.Add(item);
                }

                //Save tickMoveData and reset the tick
                moves.Push(tmpList);
                coolDown = 0;
                SaveTickInProgress = false;
                tickMoves.Clear();   
            }
        }
      
    }

    //Save cube moveInfo - transform reference and position
    public void SaveCubePosition(Transform cube, Vector3 position)
    {
            SaveTickInProgress = true;
         
            //Create a MoveInfo container to save move data per obj
            MoveInfo tmpInfo = new MoveInfo();
            tmpInfo.element = cube;
            tmpInfo.elemPos = position;
            tickMoves.Add(tmpInfo);
    }

    //Get back turns
    public void RevertMove()
    {
        //While there're moves
        if (moves.Count > 0)
        {
            //Prepare a container for reverted tickMoves
            List<MoveInfo> tmpInfo = new List<MoveInfo>();
            
            //Get last tick from stack
            tmpInfo = moves.Pop();

            //Set every position within one tick
            foreach (var tickElem in tmpInfo)
            {
                tickElem.element.position = tickElem.elemPos;
            }
        }
        else
        {
            Debug.Log("NONE");
        }
       
    }

  
}
