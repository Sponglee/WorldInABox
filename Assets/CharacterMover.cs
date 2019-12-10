using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    public Animator anim;
    public float moveDuration;

    public float moveCoolDown = 5f;


    private Vector3 targetDir;
    private Vector3 targetRot;


    //Move a character on cooldown
    public void MoveChar(SwipeDirection dir)
    {
        if (moveCoolDown >= moveDuration)
        {
            //Pick direction instruction
            switch (dir)
            {
                case SwipeDirection.Up:
                    {
                        targetDir = Vector3.left;
                        targetRot = new Vector3(0, -90f, 0);
                        break;
                    }
                case SwipeDirection.Down:
                    {
                        targetDir = Vector3.right;
                        targetRot = new Vector3(0, 90f, 0);
                        break;
                    }
                case SwipeDirection.Left:
                    {
                        targetDir = Vector3.back;
                        targetRot = new Vector3(0, -180f, 0);
                        break;
                    }
                case SwipeDirection.Right:
                    {
                        targetDir = Vector3.forward;
                        targetRot = new Vector3(0, 0, 0);
                        break;
                    }
                default:
                    break;
            }



            //Move depending on switch instructions
            transform.eulerAngles = targetRot;
            if (CheckMoves.CanMove(transform, targetDir, true))
            {
                moveCoolDown = 0f;
                StartCoroutine(StopMove(targetDir));
                anim.SetTrigger("Move");
            }
            else
            {
                if (CheckMoves.CanClimb(transform, targetDir))
                {
                    StartCoroutine(StopClimb(targetDir));
                }
            }

        }
        
    }


    private IEnumerator StopMove(Vector3 dir,bool CheckBelow=true)
    {
        float elapsed = 0;
        
        Vector3 startPos = transform.position;

        while (elapsed<=moveDuration)
        {
            transform.position = Vector3.Lerp(startPos, startPos + dir, elapsed / moveDuration);
            elapsed += Time.deltaTime;
            moveCoolDown += Time.deltaTime;
            yield return null;
        }
        transform.position = startPos + dir;

        if(CheckBelow)
        {
            if(CheckMoves.CanFall(transform))
            {
               yield return StopMove(Vector3.down, true);
            }
        }
    }

    private IEnumerator StopClimb(Vector3 dir)
    {
        //Move up, dont check what's below
        yield return StopMove(Vector3.up,false);
        //Move to dir
        yield return StopMove(dir);
    }


    
}
