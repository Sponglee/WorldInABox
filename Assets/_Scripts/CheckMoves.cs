using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CheckMoves
{

    
    //Check if target can move or not, in case of Player - check cube after target aswell
    public static bool CanMove(Transform moveTransform, Vector3 dir, bool IsPlayer = false)
    {
        

        Transform dirCheck = RayCheck(moveTransform.position, dir);
       
        //Cast a ray towards targetDir
        if (dirCheck != null)
        {

            //Check if it's a cube
            if (dirCheck.CompareTag("Cube"))
            {
                //Check if that cube is fixated
                if (dirCheck.GetComponent<CubeBehaviour>() != null && dirCheck.GetComponent<CubeBehaviour>().Fixated)
                {
                    return false;
                }
                //Check if nothing ontop
                else if (RayCheck(dirCheck.position,Vector3.up) != null)
                {
                    return false;
                }
                //Check if that cube can climb itself
                else if (IsPlayer && dirCheck.GetComponent<CubeBehaviour>() != null
                            && dirCheck.GetComponent<CubeBehaviour>().Movable
                                && CanClimb(dirCheck, dir))
                {
                    return true;
                }
                //If next block can move and player is checking
                else if (IsPlayer && CanMove(dirCheck, dir))
                {
                    return true;
                }
                else return false;
            }
            else if(dirCheck.CompareTag("Liquid"))
            {
                return true;
            }
            else if(dirCheck.CompareTag("Exit"))
            {
                return true;
            }
            //Check for boundary
            else
            {
                return false;
            }

        }
        else
            return true;
       
    }


    //Check if can climb on top
    public static bool CanClimb(Transform moveTransform, Vector3 dir)
    {
        Transform dirCheck = RayCheck(moveTransform.position, dir);
        //Debug.Log("ClimbCheck " + dirCheck.name);
        if (dirCheck != null)
        {

            if (dirCheck.CompareTag("Cube"))
            {
                Transform upCheck = RayCheck(dirCheck.position, Vector3.up);
                if (upCheck != null)
                {
                    return false;
                }
                else
                    return true;
            }
            else return false;
        }
        else
            return true;
    }

    //Check if can climb on top
    public static bool CanFall(Transform moveTransform)
    {
        //Shoot ray down
        Transform downCheck = RayCheck(moveTransform.position, Vector3.down);


        if (downCheck != null)
        {
            //Debug.Log(hitDown.transform.name);
            if (downCheck.CompareTag("Liquid"))
            {
                return true;
            }
            else
                return false;
        }
        return true;
    }

    //Raycast
    public static Transform RayCheck(Vector3 origin, Vector3 dir)
    {
        Ray ray = new Ray(origin, dir);
        RaycastHit hit;

        Debug.DrawLine(origin, origin + dir, Color.magenta, 10f);

        //Shoot ray up from that cube
        if (Physics.Raycast(ray, out hit, 1f))
        {
            //Debug.Log(hit.transform.name);
            return hit.transform;
        }

        return null;
    }



    //public static Transform CanPushUp(Transform moveTransform, Vector3 dir)
    //{
    //    Ray rayDown = new Ray(moveTransform.position, dir);
    //    RaycastHit hit;
    //    //Shoot ray up from that cube
    //    if (Physics.Raycast(rayDown, out hit, 1.2f))
    //    {
    //        //Debug.Log(hitDown.transform.name);
    //        if (hit.transform.CompareTag("Cube") && hit.transform.GetComponent<CubeBehaviour>().Movable)
    //        {
    //            Debug.Log("Can Push up? " + hit.transform.name);
    //            //Move 
    //            if(CanClimb(hit.transform, dir))
    //            {
                   
    //                return hit.transform; 
    //            }
    //        }
    //        else return null;
    //    }
    //    return null;
    //}

}
