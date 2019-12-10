using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(CheckMoves.CanMove(transform,(transform.position - other.transform.position).normalized))
            {
                transform.position += (transform.position - other.transform.position).normalized;
                Debug.Log((transform.position - other.transform.position).normalized);
            }
           
        }


    }


    



}
