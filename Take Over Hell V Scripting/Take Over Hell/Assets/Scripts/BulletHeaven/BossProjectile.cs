using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    [SerializeField] private float timeDuration;
    
    private GameObject targetGameobject;
    private Transform boss;
    private float step;
    private Vector3 position;

    private void Awake()
    {
        Destroy(gameObject, timeDuration);
    }

    private void Start()
    {
        position = targetGameobject.transform.position;
        boss = FindObjectOfType<EnemySpawner>().thisIsBoss;
    }

    private void Update()
    {
        step = speed * Time.deltaTime;
        
        if (targetGameobject != null)
        {
            transform.position = Vector3.MoveTowards(boss.position, position, step);
        }
    }

    public void SetTarget(GameObject target)
    {
        targetGameobject = target;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject ==  targetGameobject)
        {
            targetGameobject.GetComponent<Character>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
