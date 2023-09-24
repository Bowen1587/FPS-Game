using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rango : MonoBehaviour
{
    static public bool attack = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            attack = true;
            Debug.Log("ppp");
        }
    }
}
