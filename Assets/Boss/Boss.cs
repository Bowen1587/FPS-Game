using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Animator animator;
    public Quaternion augulo;
    public GameObject fire;
    public float grado;
    int count;

    public GameObject target;
    public GameObject mouth;
    private GameObject dragon;
    private string currentState;

    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.Find("Player");
        dragon = GameObject.Find("Boss");
        mouth = dragon.transform.Find("Root").gameObject;
        mouth = mouth.transform.Find("Spine01").gameObject;
        mouth = mouth.transform.Find("Spine02").gameObject;
        mouth = mouth.transform.Find("Chest").gameObject;
        mouth = mouth.transform.Find("Neck01").gameObject;
        mouth = mouth.transform.Find("Neck02").gameObject;
        mouth = mouth.transform.Find("Neck03").gameObject;
        mouth = mouth.transform.Find("Head").gameObject;
        mouth = mouth.transform.Find("Jaw01").gameObject;
    }


    void ChangeState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }

    void Fire()
    {
        fire = (GameObject)Resources.Load("FlameThrower");
        GameObject myFire = Instantiate(fire, new Vector3(mouth.transform.position.x, mouth.transform.position.y, mouth.transform.position.z), transform.rotation);
        myFire.transform.parent = mouth.transform;
    }

    public void Action()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > 20)
        {
            ChangeState("Sleep");
        }
        
        else if (Vector3.Distance(transform.position, target.transform.position) > 15)
        {
            ChangeState("Scream");
        }
        
        else
        {
            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);

            if (Vector3.Distance(transform.position, target.transform.position) > 11)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                ChangeState("Run");
                transform.Translate(Vector3.forward * 3 * Time.deltaTime);
                Rango.attack = false;
            }
            
            else if (Vector3.Distance(transform.position, target.transform.position) > 6)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                ChangeState("Flame Attack");
                Invoke("Fire", 0.5f);
            }

            else
            {
                ChangeState("Basic Attack");
            }
        }
    }

    void Update()
    {
        Action();
    }
}
