using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            FunctionHandler.Instance.LevelComplete();
            Destroy(other.gameObject);
        }
    }
}
