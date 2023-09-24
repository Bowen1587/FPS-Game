using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHP : MonoBehaviour
{
    [SerializeField] private float HP;
    [SerializeField] private float max_health;
    static public bool willDie = false;
    //[SerializeField] private float current_HP;
    private int count = 0;
    Animator animator;
    private string currentState;

    void Start()
    {
        animator = GetComponent<Animator>();
        HP = max_health;
        //current_HP = HP;
    }

    void ChangeState(string newState)
    {
        if (currentState == newState)
        {
            animator.Play(currentState);
        }

        animator.Play(newState);
        currentState = newState;
    }

    private IEnumerator Delay(float duration)
    {
        yield return new WaitForSeconds(duration);
    }

    public void Damage(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot Have Negative Damage");
        }
        Debug.Log("HP" + HP);
        if (count == 0)
        {
            HP -= amount;
            count++;
            Hurt();
        }
        Debug.Log("HP" + HP);
    }

    public void Heal(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot Have Negative Healing");
        }

        HP += amount;

        if (HP + amount > max_health)
        {
            HP = max_health;
        }

    }

    private void Die()
    {
        Debug.Log("I Am Dead!!");
        ChangeState("Die");
        transform.Translate(Vector3.forward * 0 * Time.deltaTime);
        Destroy(this.gameObject, 2);
    }

    private void Hurt()
    {
        ChangeState("Get Hit");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet"))
        {
            Damage(10);
            Debug.Log("玩家扣10滴血");
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("bullet"))
        {
            Damage(10);
            Debug.Log("玩家扣10滴血");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("j"))
        {
            Damage(10);
            Debug.Log("玩家自動扣10滴血");
        }

        count = 0;

        if (Input.GetKeyDown("h"))
        {
            Heal(10);
            Debug.Log("玩家自動加10滴血");
        }

        if(HP <= (max_health / 3))
        {
            willDie = true;
        }

        if (HP <= 0)
        {
            Die();
        }
    }
}
