using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Animator animator;
    public Quaternion augulo;
    public GameObject fire;
    public float grado;
    int count;
    private bool isGround = false;

    public GameObject target;
    public GameObject mouth;
    private GameObject dragon;
    private string currentState;

    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.Find("Player");
        dragon = GameObject.Find("Boss2");
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

    void Run()
    {
        var lookPos = target.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
        ChangeState("Fly Forward");
        transform.Translate(Vector3.forward * 3 * Time.deltaTime);
    }

    void Fire()
    {
        fire = (GameObject)Resources.Load("FlameThrower");
        GameObject myFire = Instantiate(fire, new Vector3(mouth.transform.position.x, mouth.transform.position.y, mouth.transform.position.z), transform.rotation);
        myFire.transform.parent = mouth.transform;
    }

    public void Fly_Action()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > 20)
        {
            
            cronometro += 1 * Time.deltaTime;
            if(cronometro >= 2)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }
            switch (rutina)
            {
                case 0:
                    ChangeState("Fly Float");
                    break;
                case 1:
                    grado = Random.Range(0, 360);
                    augulo = Quaternion.Euler(0, grado, 0);
                    rutina++;
                    break;
                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, augulo, 0.5f);
                    transform.Translate(Vector3.forward * 2 * Time.deltaTime);
                    ChangeState("Fly Glide");
                    break;
            }
        }

        else
        {

            if (Vector3.Distance(transform.position, target.transform.position) > 14)
            {
                Run();
            }

            else
            {
                var lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                ChangeState("Fly Flame Attack");
                Invoke("Fire", 0.5f);
            }
        }
    }

    public void Action()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > 11)
        {
            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
            ChangeState("Run");
            transform.Translate(Vector3.forward * 3 * Time.deltaTime);
        }

        else if (Vector3.Distance(transform.position, target.transform.position) > 6)
        {
            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
            ChangeState("Flame Attack");
            Invoke("Fire", 0.5f);
        }
    }

    void Update()
    {
        if (!BossHP.willDie)
        {
            //transform.position = new Vector3(transform.position.x, 3, transform.position.z);
            Fly_Action();
        }

        if (BossHP.willDie)
        {
            if(transform.position.y > 0)
            {
                ChangeState("Land");
                transform.Translate(Vector3.down * Time.deltaTime);
            }

            if(transform.position.y == 0)
            {
                isGround = true;
            }
            
        }

        if (isGround)
        {
            Action();
        }
    }
}
