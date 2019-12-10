﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/** Swipe direction */
public enum SwipeDirection
{
    None = 0,
    Left = 1,
    Right = 2,
    Up = 4,
    Down = 8,
    UpRight = 16,
    DownRight = 32,
    DownLeft = 64,
    UpLeft = 128,

}



public class SwipeManager : Singleton<SwipeManager>
{


    public SwipeDirection Direction { set; get; }


    private Vector3 startTouch;
    private Vector3 endTouch;
    private Vector3 screenTouch;


    [SerializeField]
    private float swipeResistance = 5f;


    public bool SwipeC = true;
    public bool swipeValue = false;

    //gameManager reference
    [SerializeField]
    private CharacterMover charMover;


    //JumpBool charge system
    public float ChargeTimer = 0;
    public float ChargeLimit = 1f;
    public float ChargeResistance = 0.4f;
    public Image ChargeImg;

    

    // Update is called once per frame
    void Update()
    {
        Direction = SwipeDirection.None;



        if (true /*Input.touchCount > 0 && !gameManager.activeCube.CubeOpened*/)
        {
            Vector2 deltaSwipe = Vector2.zero;

            Touch[] touches = Input.touches;

            ////If there's 2 fingers
            //if(touches.Length == 2)
            //{
            //    //Activate jumpbool
            //    gameManager.character.JumpBool = true;

            //    //If second finger swipes
            //    if (touches[1].phase == TouchPhase.Began)
            //    {
            //        //Debug.Log("BEGAN");
            //        startTouch = touches[1].position;
            //        screenTouch = gameManager.physicalCam.ScreenToViewportPoint(startTouch);
            //    }
            //    else if (touches[1].phase == TouchPhase.Ended)
            //    {
            //        endTouch = gameManager.physicalCam.ScreenToViewportPoint(touches[1].position);
            //        deltaSwipe = screenTouch - endTouch;
            //        //Debug.Log("ENDED " + deltaSwipe.x + ":" + deltaSwipe.y
            //        CheckSwipe(deltaSwipe);

            //    }
            //}
            //Else if only 1 finger

            if (true)
            {
                //Touch touch = Input.GetTouch(0);

                //Proceed with 1 finger
                if (Input.GetMouseButtonDown(0))
                {
                    //ChargeTimer = 0;

                    if (!IsPointerOverUIObject("UI"))
                    {
                        //Debug.Log("BEGAN");
                        startTouch = Input.mousePosition;

                        screenTouch = Camera.main.ScreenToViewportPoint(startTouch);
                    }

                   
                }
                //else if (Input.GetMouseButton(0))
                //{
                //    ////If not touching ui - charge the jump


                //    //ChargeTimer += Time.deltaTime;
                //    //if (ChargeTimer >= ChargeResistance)
                //    //{
                //    //    ChargeImg.gameObject.SetActive(true);
                //    //    ChargeImg.transform.position = Input.mousePosition;

                //    //    ChargeImg.fillAmount = ChargeTimer / ChargeLimit;
                //    //    if (ChargeTimer >= ChargeLimit)
                //    //    {
                //    //        //gameManager.character.JumpBool = true;
                //    //        ChargeImg.color = Color.black;
                //    //    }
                //    //}




                //}
                else if (Input.GetMouseButtonUp(0))
                {

                    //ChargeImg.gameObject.SetActive(false);
                    //ChargeImg.color = Color.white;
                    endTouch = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                
                    deltaSwipe = screenTouch - endTouch;
                    //Debug.Log("ENDED " + deltaSwipe.x + ":" + deltaSwipe.y);
                    CheckSwipe(deltaSwipe);

                }

                ////If jump finger was pressed - release and return
                //if (gameManager.character.JumpBool)
                //{
                //    if (Input.GetMouseButtonUp(0))
                //    {
                //        //Release jumpbool
                //        gameManager.character.JumpBool = false;

                //        //Debug.Log("SECOND END");
                //        return;
                //    }
                //}
            }
            ////reset charge sprite
            //else
            //{
            //    ChargeImg.gameObject.SetActive(false);
            //    ChargeImg.color = Color.white;
            //}
        }
    }


    //Execute the swipe
    public void CheckSwipe(Vector2 deltaSwipe)
    {
        if (Mathf.Abs(deltaSwipe.x) > Mathf.Abs(deltaSwipe.y) && Mathf.Abs(deltaSwipe.x) > swipeResistance)
        {
            if (Mathf.Abs(deltaSwipe.y) > swipeResistance)
            {
                if (deltaSwipe.x < 0)
                    Direction |= (deltaSwipe.y < 0) ? SwipeDirection.Right : SwipeDirection.Down;
                else if (deltaSwipe.x > 0)
                    Direction |= (deltaSwipe.y < 0) ? SwipeDirection.Up : SwipeDirection.Left;
            }
            else
                Direction |= (deltaSwipe.x < 0) ? SwipeDirection.Right : SwipeDirection.Left;
        }
        else if (Mathf.Abs(deltaSwipe.y) > Mathf.Abs(deltaSwipe.x) && Mathf.Abs(deltaSwipe.y) > swipeResistance)
        {
            if (Mathf.Abs(deltaSwipe.x) > swipeResistance)
            {
                if (deltaSwipe.x < 0)
                    Direction |= (deltaSwipe.y < 0) ? SwipeDirection.Right : SwipeDirection.Down;
                else if (deltaSwipe.x > 0)
                    Direction |= (deltaSwipe.y < 0) ? SwipeDirection.Up : SwipeDirection.Left;
            }
            else
                Direction |= (deltaSwipe.y < 0) ? SwipeDirection.Up : SwipeDirection.Down;
        }
        else
        {

            Direction |= SwipeDirection.None;
        }

        //Debug.Log(Direction);
        charMover.MoveChar(Direction);
    }

    //Remember swipe state
    public bool IsSwiping(SwipeDirection dir)
    {
        
        return (Direction & dir) == dir;
    }


    //Change swipe value
    public void SwipeChange()
    {
        swipeValue = !swipeValue;

    }


    // Is touching ui
    public bool IsPointerOverUIObject(string obj)
    {
        //Debug.Log("RE");
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        if (results.Count > 0)
            return results[0].gameObject.CompareTag(obj);
        else
            return false;
    }

}