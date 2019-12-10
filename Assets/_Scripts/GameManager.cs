using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public float goal = 500;
    [SerializeField]
    private float score = 0;
    public float Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            progression.value = score/goal;

            if (score/goal >=1)
            {
                FunctionHandler.Instance.LevelComplete();
            }
        }
    }

    public Slider progression;

    public Transform pressRef;
    public Transform targetRef;

    public Transform pressLower;
    public Transform pressUpper;

    public float speed = 1f;
    public float upperSpeed = 0.2f;
    public float pressUpperLimit;

    public bool upperBool = false;



    public Transform brigde;
    public float bridgeSpeed = 1f;

    private void Start()
    {
        pressUpperLimit = pressUpper.position.y;    
    }

    // Update is called once per frame
    void Update()
    {
        //Scroll the bridge
        brigde.Translate(Vector3.back * bridgeSpeed * Time.deltaTime);

        //Move down if pressed
        if (Input.GetMouseButtonDown(0))
        {
            targetRef = pressLower;
            pressUpper.position = new Vector3(pressUpper.position.x, pressUpperLimit, pressUpper.position.z);
        }
        //Move up if released
        else if(Input.GetMouseButtonUp(0))
        {
            targetRef = pressUpper;
        }
        else if (Input.GetMouseButton(0))
        {
            if (targetRef == pressUpper && pressRef.position.y < targetRef.position.y)
                pressRef.Translate(Vector3.up * speed / 2 * Time.deltaTime);
            else if (targetRef == pressLower && pressRef.position.y > targetRef.position.y)
                pressRef.Translate(Vector3.down * speed * Time.deltaTime);
            else
            {
                Debug.Log("REE");
                targetRef = (targetRef == pressLower) ? pressUpper : pressLower;
            }

        }
        //else if (upperBool)
        //{
        //    pressUpper.Translate(Vector3.down * upperSpeed * Time.deltaTime);

        //    pressUpper.position = new Vector3(pressUpper.position.x, Mathf.Clamp(pressUpper.transform.position.y, pressUpperLimit, 10f), pressUpper.position.z);
        //    if (pressUpper.transform.position.y == -1.5f)
        //    {
        //        upperBool = false;
        //    }
        //}
        else
        {
            upperBool = true;
           
            if (targetRef == pressUpper && pressRef.position.y < targetRef.position.y)
            {
                pressUpper.Translate(Vector3.up * upperSpeed * Time.deltaTime);
                pressUpper.position = new Vector3(pressUpper.position.x, Mathf.Clamp(pressUpper.transform.position.y, pressUpperLimit, 1f), pressUpper.position.z);
                pressRef.Translate(Vector3.up * speed / 2 * Time.deltaTime);
            }
        }

        Debug.Log(Mathf.Abs(pressRef.position.y - targetRef.position.y));

       
        

        
    }
}
