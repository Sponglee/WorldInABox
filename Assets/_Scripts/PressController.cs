using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressController : MonoBehaviour
{
    public Material pressedMat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            collision.transform.localScale = new Vector3(collision.transform.localScale.x, collision.transform.localScale.y / 2, collision.transform.localScale.z);
            collision.transform.GetComponent<Renderer>().material = pressedMat;
            GameManager.Instance.Score++;
        }
        else if (collision.gameObject.CompareTag("Danger"))
        {
            FunctionHandler.Instance.GameOver();
        }
    }
}
