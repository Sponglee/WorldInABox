using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehaviour : MonoBehaviour
{
    //Toggle if object can't be moved
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

    //Toggle if object can be pushed up
    [SerializeField] private bool pushable = false;
    public bool Pushable
    {
        get
        {
            return pushable;
        }

        set
        {
            pushable = value;
        }
    }

    [SerializeField] private bool isMoving = false;
    public bool IsMoving
    {
        get
        {
            return isMoving;
        }

        set
        {
            isMoving = value;
            if (value == false)
            {
                Vector3 tmp = transform.position;
                tmp.x = Mathf.Round(tmp.x);
                tmp.y = Mathf.Round(tmp.y);
                tmp.z = Mathf.Round(tmp.z);

                transform.position = tmp;
            }
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
            //Or can be pushed and push
            else if (Pushable && CheckMoves.CanClimb(transform, (transform.position - other.transform.position).normalized))
            {
                GameManager.Instance.SaveCubePosition(transform, transform.position);

                PushUp(transform.position - other.transform.position);
            }
        }
    }


    //Push this object up(climb)
    public void PushUp(Vector3 dir)
    {
        StartCoroutine(StopPushUp(dir.normalized));
    }

    //Move object to dir
    private void Move(Vector3 dir, bool CheckBelow = true)
    {
        StartCoroutine(StopMove(dir.normalized, CheckBelow));
    }


    //Move coroutine
    private IEnumerator StopMove(Vector3 dir, bool CheckBelow = true)
    {
        IsMoving = true;
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
                Debug.Log("CANFALL");
                yield return StopMove(Vector3.down);
            }
        }

        IsMoving = false;
    }

    //Push coroutine (Climb)
    private IEnumerator StopPushUp(Vector3 dir)
    {
        yield return StopMove(Vector3.up, false);
        yield return StopMove(dir);
    }

}
