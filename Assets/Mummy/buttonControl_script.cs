using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class buttonControl_script : MonoBehaviour 
{

	Animator anim;
    public int rutina;
    public float cronometro;
    public Quaternion augulo;
    public float grado;
    public GameObject target;
    public GameObject rockfall;
    static public bool isRock = false;

    void Start()
	{
		anim = GetComponentInChildren<Animator>();
        target = GameObject.Find("Player");
    }

	public void Crippled()
	{
        anim.Play("crippledWalk");
    }

    public void Walk()
    {
        anim.Play("walk");
    }

    public void Idle()
	{
        anim.Play("idle");
    }

	public void Dance()
	{
        anim.Play("dance");
    }

    public void RemoteAttack()
    {
        rockfall = (GameObject)Resources.Load("Rockfall");
        GameObject myRockfall = Instantiate(rockfall, new Vector3(target.transform.position.x, target.transform.position.y - 1, target.transform.position.z), Quaternion.identity);
        myRockfall.transform.parent = transform;
        isRock = true;
    }

    public void Comportamiento_Enemigo()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > 5)
        {

            cronometro += 1 * Time.deltaTime;
            if (cronometro >= 2)
            {
                rutina = Random.Range(0, 5);
                cronometro = 0;
            }
            switch (rutina)
            {
                case 0:
                    Idle();
                    break;
                case 1:
                    grado = Random.Range(0, 360);
                    augulo = Quaternion.Euler(0, grado, 0);
                    rutina++;
                    break;
                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, augulo, 0.5f);
                    transform.Translate(Vector3.forward * Time.deltaTime);
                    Crippled();
                    break;
                case 3:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, augulo, 0.5f);
                    transform.Translate(Vector3.forward * Time.deltaTime);
                    Walk();
                    break;
                case 4:
                    Dance();
                    break;
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, target.transform.position) > 2.5)
            {
                var lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 3);
                anim.SetBool("isRun", true);
                //anim.Play("run");
                transform.Translate(Vector3.forward * 2 * Time.deltaTime);
                isRock = false;
                //anim.SetBool("attack", false);
            }
            else if (Vector3.Distance(transform.position, target.transform.position) > 2)
            {
                anim.Play("magic");
                RemoteAttack();
            }
            else
            {
                /*
                anim.SetBool("crippled", false);
                anim.SetBool("isRun", false);
                anim.Play("attack");
                */
                anim.SetBool("attack", true);
                isRock = false;
            }

        }
    }

    void Update()
    {
        Comportamiento_Enemigo();
    }
}
