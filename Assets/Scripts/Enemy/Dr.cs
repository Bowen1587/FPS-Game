using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dr : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Animator animator;
    public Quaternion augulo;
    public float grado;

    public GameObject target;
    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.Find("Player");
    }

    public void Comportamiento_Enemigo()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > 5)
        {

            cronometro += 1 * Time.deltaTime;
            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }
            switch (rutina)
            {
                case 0:
                    //animator.SetBool("walk", false);
                    animator.Play("idle02");
                    break;
                case 1:
                    grado = Random.Range(0, 360);
                    augulo = Quaternion.Euler(0, grado, 0);
                    rutina++;
                    break;
                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, augulo, 0.5f);
                    transform.Translate(Vector3.forward * Time.deltaTime);
                    animator.Play("walk");
                    break;
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, target.transform.position) > 2)
            {
                var lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 3);
                //animator.SetBool("walk", false);

                animator.Play("run00");
                transform.Translate(Vector3.forward * 2 * Time.deltaTime);

                //animator.SetBool("attack", false);
            }
            else
            {
                //animator.SetBool("walk", false);
                //animator.SetBool("run", false);

                animator.Play("attack00");
            }

        }
    }

    void Update()
    {
        Comportamiento_Enemigo();
    }
}
