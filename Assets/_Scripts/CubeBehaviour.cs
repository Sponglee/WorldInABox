using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehaviour : MonoBehaviour
{
    
    [SerializeField] private bool fixated = false;
    public bool Fixated
    {
        get
        {
            return fixated;
        }

        set
        {
            fixated = value;
        }
    }


    [SerializeField] private bool movable = false;
    public bool Movable
    {
        get
        {
            return movable;
        }

        set
        {
            movable = value;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //Check if movable and can move
            if (CheckMoves.CanMove(transform, (transform.position - other.transform.position).normalized))
            {
                GameManager.Instance.SaveCubePosition(transform, transform.position);

                Move(transform.position - other.transform.position);
            }
            else if (Movable && CheckMoves.CanClimb(transform, (transform.position - other.transform.position).normalized))
            {
                GameManager.Instance.SaveCubePosition(transform, transform.position);

                PushUp(transform.position - other.transform.position);
            }
        }
    }

    public void PushUp(Vector3 dir)
    {
        StartCoroutine(StopPushUp(dir.normalized));
    }

    
    private void Move(Vector3 dir, bool CheckBelow = true)
    {
        StartCoroutine(StopMove(dir.normalized, CheckBelow));
    }


    private IEnumerator StopMove(Vector3 dir, bool CheckBelow = true)
    {
        //Debug.Log("RE");
        float elapsed = 0;
        float moveDuration = 0.2f;
        Vector3 startPos = transform.position;

        while (elapsed <= moveDuration)
        {
            transform.position = Vector3.Lerp(startPos, startPos + dir, elapsed / moveDuration);
            elapsed += Time.deltaTime;
            
            yield return null;
        }
        transform.position = startPos + dir;

        if (CheckBelow)
        {
            if (CheckMoves.CanFall(transform))
            {
                yield return StopMove(Vector3.down);
            }
        }
    }

    private IEnumerator StopPushUp(Vector3 dir)
    {
        yield return StopMove(Vector3.up, false);
        yield return StopMove(dir);
    }

}
