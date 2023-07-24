using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombi : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Animator animator;
    public Quaternion augulo; 
    public float grado;

    public GameObject target;
    private GameObject wall;
    //public bool atacando;
    //private GameObject ZombiAttackArea = default;
    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.Find("Player");
        //wall = GameObject.FindWithTag("wall");
        //ZombiAttackArea = transform.GetChild(2).gameObject;
    }

    /*void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "wall") ;
        rutina = 1;
    }
    */
    public void Comportamiento_Enemigo()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > 5)
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
                    animator.SetBool("walk", false);
                    animator.SetBool("run", false);
                    break;
                case 1:
                    grado = Random.Range(0, 360);
                    augulo = Quaternion.Euler(0, grado, 0);
                    rutina++;
                    break;
                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, augulo, 0.5f);
                    transform.Translate(Vector3.forward * Time.deltaTime);
                    animator.SetBool("walk", true);
                    break;
            }
        }
        else
        {


            if(Vector3.Distance(transform.position, target.transform.position) > 1.5)
            {
                var lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                animator.SetBool("walk", false);
                animator.SetBool("run", true);
                transform.Translate(Vector3.forward * 2 * Time.deltaTime);

                animator.SetBool("attack", false);
            }
            else
            {
                animator.SetBool("walk", false);
                animator.SetBool("run", false);

                animator.SetBool("attack", true);
                
                //atacando = true;
                //ZombiAttackArea.SetActive(atacando);
            }
            
        }
    }
    /*
    public void Final_Ani()
    {
        animator.SetBool("attack", false);
        atacando = false;

    }
    */

    void Update()
    {
        Comportamiento_Enemigo();
    }
}
