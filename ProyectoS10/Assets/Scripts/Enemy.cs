using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int routine;
    public float cronom;
    public Quaternion angle;
    public float grade;

    public Animator anim;
    public GameObject target;

    public bool attacking;
    public float speed;

    public RangeEnemy range;

    public NavMeshAgent agent;
    public float distance_attack;
    public float radius_vision;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.Find("Personaje");
    }

    public void BehaviorEnemy()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > radius_vision)
        {
            agent.enabled = false;
            anim.SetBool("run", false);
            cronom += 1 * Time.deltaTime;
            if (cronom >= 4)
            {
                routine = Random.Range(0, 2);
                cronom = 0;
            }

            switch (routine)
            {
                case 0:
                    anim.SetBool("walk", false);
                    break;

                case 1:
                    grade = Random.Range(0, 360);
                    angle = Quaternion.Euler(0, grade, 0);
                    routine++;
                    break;

                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angle, 0.5f);
                    transform.Translate(Vector3.forward * speed * Time.deltaTime);
                    anim.SetBool("walk", true);
                    break;
            }
        }
        else {
            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            
            agent.enabled = true;
            agent.SetDestination(target.transform.position);

            if (Vector3.Distance(transform.position, target.transform.position) > distance_attack && !attacking)
            {
                anim.SetBool("walk", false);
                anim.SetBool("run", true);
            }
            else
            {
                if (!attacking)
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 1);
                    anim.SetBool("walk", false);
                    anim.SetBool("run", false);
                }
            }
        }

        if (attacking)
        {
            agent.enabled = false;
        }
    }

    public void Final_Ani()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > distance_attack + 0.2f)
        {
            anim.SetBool("attack", false);
        }
        attacking = false;
        range.GetComponent<CapsuleCollider>().enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        BehaviorEnemy();
    }
}
