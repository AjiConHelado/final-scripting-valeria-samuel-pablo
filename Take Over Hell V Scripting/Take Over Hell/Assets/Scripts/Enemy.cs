using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public enum EnemyType
{
    One, two, three, four
}

public class Enemy : MonoBehaviour
{
    [HideInInspector] public EnemySpawner enemySpawner;

    [SerializeField] private int hp = 999;

    [SerializeField] public EnemyType type;
    [SerializeField] private GameObject bossProjectile;
    public EnemyData stats;
    private Transform targetDestination;
    private GameObject targetGameobject;
    private Character targetCharacter;
    private Rigidbody2D rgbd2d;
    private Animator anim;
    private int currentHp;
    private float distance, timer;
    private bool canAttack;
    private void Awake()
    {
        rgbd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHp = hp;
    }

    public void SetTarget(GameObject target)
    {
        targetGameobject = target;
        targetDestination = target.transform;
    }

    private void FixedUpdate()
    {
        Vector3 direction = (targetDestination.position - transform.position).normalized;

        if (type != EnemyType.four)
        {
            rgbd2d.velocity = direction * stats.speed;
        }
        else
        {
            distance = Vector3.Distance(transform.position, targetDestination.position);

            if (distance > 6)
            {
                rgbd2d.velocity = direction * stats.speed;
                canAttack = false;
            }
            else
            {
                rgbd2d.velocity = Vector2.zero;
                transform.position = transform.position;
                canAttack = true;
            }
        }

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            AttackBoss();
            timer = 1.5f;
        }

        anim.SetFloat("MovimientoX", direction.x);
    }

    private void AttackBoss()
    {
        if (canAttack)
        {
            BossProjectile obj = Instantiate(bossProjectile).GetComponent<BossProjectile>();

            obj.SetTarget(targetGameobject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject == targetGameobject)
        {
            Attack();
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.tag == "MainCamera")
        {

            enemySpawner.Return(this, type);
            return;
        }
    }


    private void Attack()
    {
        if (targetCharacter == null)
        {
            targetCharacter = targetGameobject.GetComponent<Character>();
        }
        targetCharacter.TakeDamage(stats.damage);
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;

        if (currentHp < 1)
        {
            targetGameobject.GetComponent<Level>().AddExperience(stats.ExpReward);

            if (enemySpawner != null)
            {
                enemySpawner.Return(this, type);
                GetComponent<DropOnDestroy>().OneDestroy();
                currentHp = hp;
                return;
            }

            Destroy(gameObject);
        }
    }
}
