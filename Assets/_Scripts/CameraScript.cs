using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public SpriteRenderer referenceSprite;
    public CinemachineVirtualCamera camRef;
    // Use this for initialization
    void Start()
    {
        //camRef = GameManager.Instance.levelCam;


        ////For orthographic cam
        //float screenRatio = (float)Screen.width / (float)Screen.height;
        //float targetRatio = referenceSprite.bounds.size.x / referenceSprite.bounds.size.y;

        //if (screenRatio >= targetRatio)
        //{
        //    camRef.m_Lens.OrthographicSize = referenceSprite.bounds.size.y / 2;
        //}
        //else
        //{
        //    float differenceInSize = targetRatio / screenRatio;
        //    camRef.m_Lens.OrthographicSize = referenceSprite.bounds.size.y / 2 * differenceInSize;
        //}



        ////For perspective cam variable distance
        //float targetWidth = referenceSprite.bounds.size.x;
        //float dist = targetWidth / Screen.width * Screen.height;
        //dist = dist / (2.0f * Mathf.Tan(0.5f * Camera.main.fieldOfView * Mathf.Deg2Rad));
        //Vector3 v3T = camRef.transform.position;

        //v3T.z = -dist;
        //camRef.transform.position = v3T;

        //For perspective cam variable FOV
        //float targetWidth = referenceSprite.bounds.size.x;
        //float dist = targetWidth / Screen.width * Screen.height;
        //dist = dist / (2.0f * Mathf.Tan(0.5f * Camera.main.fieldOfView * Mathf.Deg2Rad));

        //float FOV = Mathf.Atan(2f * dist) / 0.5f / Mathf.Deg2Rad;

        //camRef.m_Lens.FieldOfView = FOV;
    }
}