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
                case SwipeDirection.None:
                    return;
                default:
                    break;
            }



            //Move depending on switch instructions
            transform.eulerAngles = targetRot;


            //If player can Move
            if (CheckMoves.CanMove(transform, targetDir, true))
            {
                //Debug.Log("MOVE");

                anim.SetTrigger("Move");
                moveCoolDown = 0f;

                GameManager.Instance.SaveCubePosition(transform, transform.position);

                StartCoroutine(StopMove(targetDir));
            }
            //Or can climb
            else if(CheckMoves.CanClimb(transform, targetDir))
            {
                GameManager.Instance.SaveCubePosition(transform, transform.position);

                StartCoroutine(StopClimb(targetDir));

            }

        }
        
    }

    //Move sequence
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

    //Climb sequence
    private IEnumerator StopClimb(Vector3 dir)
    {
        //Move up, dont check what's below
        yield return StopMove(Vector3.up,false);
        //Move to dir
        yield return StopMove(dir);
    }


    
}
