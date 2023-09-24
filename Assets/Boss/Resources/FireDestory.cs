using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDestory : MonoBehaviour
{
    static public bool Isfire = false;
    void Start()
    {
        
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            Isfire = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, 1.5f);
        Isfire = false;
    }
}
