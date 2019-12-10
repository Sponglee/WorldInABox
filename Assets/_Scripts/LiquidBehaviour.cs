using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Cube"))
        {
            gameObject.GetComponent<Renderer>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            gameObject.GetComponent<Renderer>().enabled = true;
        }
    }



}
