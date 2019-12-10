using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CheckMoves
{
    //Check if target can move or not, in case of Player - check cube after target aswell
    public static bool CanMove(Transform moveTransform, Vector3 dir, bool IsPlayer = false)
    {
        Ray ray = new Ray(moveTransform.position, dir);
        RaycastHit hit;

        //Cast a ray towards targetDir
        if (Physics.Raycast(ray, out hit, 1f))
        {
            Debug.DrawLine(hit.point, hit.point+ dir, Color.black, 10f);
            //Check if it's a cube
            if(hit.transform.CompareTag("Cube"))
            {
                //Check if that cube can move
                if (IsPlayer && CanMove(hit.transform, dir))
                {
                    return true;
                }
                else
                    return false;
            }
            else if(hit.transform.CompareTag("Wall"))
            {
                return false;
            }
            
        }
        return true;
    }


    //Check if can climb on top
    public static bool CanClimb(Transform moveTransform, Vector3 dir)
    {
        Ray ray = new Ray(moveTransform.position, dir);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1f))
        {
            Debug.DrawLine(hit.transform.position, hit.transform.position + Vector3.up, Color.white, 10f);
            //Check if it's a cube
            if (hit.transform.CompareTag("Cube"))
            {
                Debug.DrawLine(hit.transform.position, hit.transform.position + Vector3.up, Color.red, 10f);
                Ray rayUp = new Ray(hit.transform.position, Vector3.up);
                RaycastHit hitUp;
                //Shoot ray up from that cube
                if (Physics.Raycast(ray, out hitUp, 1f))
                {
                    
                    Debug.Log(hitUp.transform.name);
                    return false;
                }
                else
                    return true;
            }
        }
        return false;
    }

    //Check if can climb on top
    public static bool CanFall(Transform moveTransform)
    {
        Ray rayDown = new Ray(moveTransform.position, Vector3.down);
        RaycastHit hitDown;
        //Shoot ray up from that cube
        if (Physics.Raycast(rayDown, out hitDown, 1f))
        {
            Debug.Log(hitDown.transform.name);
            if(hitDown.transform.CompareTag("Liquid"))
            {
                return true;
            }
            else
                return false;
        }
        else
        {

            Debug.Log("DOWN");
            return true;
        }
    }
}
