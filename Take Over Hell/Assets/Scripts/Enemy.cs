using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    One, two, three
}

public class Enemy : MonoBehaviour
{
    [SerializeField] private int hp = 999; 
    
    [SerializeField] public EnemyType type;
    
    private Transform targetDestination;
    private GameObject targetGameobject;
    private Character targetCharacter;
    private Rigidbody2D rgbd2d;
    [HideInInspector] public EnemySpawner enemySpawner;
    
    public EnemyData stats;
    private void Awake()
    {
        rgbd2d = GetComponent<Rigidbody2D>();
      
    }
 
    public void SetTarget(GameObject target)
    {
        targetGameobject = target;
        targetDestination = target.transform;
    }

    private void FixedUpdate()
    {
        Vector3 direction = (targetDestination.position - transform.position).normalized;
        rgbd2d.velocity = direction * stats.speed;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject ==  targetGameobject)
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (targetCharacter==null)
        {
            targetCharacter = targetGameobject.GetComponent<Character>();
        }
        targetCharacter.TakeDamage(stats.damage);
    }

    public void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.tag == "MainCamera")
        {

            enemySpawner.Return(this, type);
            return;
        }
    }

       


    public void TakeDamage(int damage)
    {
        hp -= damage;

        if (hp < 1)
        {
            targetGameobject.GetComponent<Level>().AddExperience(stats.ExpReward);
            
            if (enemySpawner != null)
            { 
                enemySpawner.Return(this, type);
                return;
            }
            
            Destroy(gameObject);
        }
    }
}

