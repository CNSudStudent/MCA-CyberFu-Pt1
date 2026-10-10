using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControls : MonoBehaviour
{
    public float speed = 2f;
    public float attackingDistance = 0.6f;
    
    private Animator animatorEnemy;
    private Rigidbody rigidBodyEnemy;
    private Transform target;

    [SerializeField]
    private bool isFollowingTarget;

    private float currentAttackingTime = 0f;
    private float maxAttackingTime = 2f;
   
    void Start()
    {
        animatorEnemy = GetComponent<Animator>();
        rigidBodyEnemy = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

   
    void Update()
    {
        transform.LookAt(target.position);

        isFollowingTarget = Vector3.Distance(transform.position, target.position) >= attackingDistance;

        if (isFollowingTarget)
        {
            rigidBodyEnemy.velocity = transform.forward * speed;
        } else
        {
            Attack();
        }

        animatorEnemy.SetBool("Walk", isFollowingTarget);
    }

    void Attack()
    {
        rigidBodyEnemy.velocity = Vector3.zero;

        currentAttackingTime += Time.deltaTime;

        if (currentAttackingTime > maxAttackingTime)
        {
            currentAttackingTime = 0f;
            int rand = Random.Range(1, 4);
            animatorEnemy.SetTrigger("Attack" + rand);
        }
    }
}
