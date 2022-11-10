using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingDagger : MonoBehaviour
{
    public int damage = 3, speed = 10, swordCount = 1;
    [HideInInspector] public bool usable;
    
    [SerializeField] private int initialSize, incrementAmount;
    [SerializeField] private float timeToAttack;
    [SerializeField] private GameObject sword;

    private Queue<Projectile> pool = new Queue<Projectile>();
    private PlayerController cntrl;
    private float timer, spread = 0.5f;
    private byte zero = 0;

    private MusicPlayer player;
    
    private void Awake()
    {
        cntrl = GetComponentInParent<PlayerController>();
        AddInstances(initialSize);
        player = Camera.main.GetComponent<MusicPlayer>();
    }

    void Update()
    {
        if (usable)
        {
            timer -= Time.deltaTime;
            if (timer < zero)
            {
                SpawnSword();
            }
        }
    }

    private void SpawnSword()
    {
        timer = timeToAttack;
        for (int i = 0; i < swordCount; i++)
        {
            Allocate().SetDirection(cntrl.lastHorizontalVector, cntrl.lastVerticalVector, swordCount, spread, i);
        }
        
        player.PlaySound(player.Sword);
    }
    
    private void AddInstances(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Projectile obj = Instantiate(sword).GetComponent<Projectile>();
            obj.thrw = this;
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    private Projectile Allocate()
    { 
        Projectile instance;
            
        if (pool.Count == 0)
        {
            AddInstances(incrementAmount);
            return Allocate();
        }
            
        instance = pool.Dequeue();
        instance.gameObject.SetActive(true);

        instance.transform.position = transform.position;
        
        return instance;
    }

    public void Return(Projectile item)
    {
        pool.Enqueue(item);
        item.gameObject.SetActive(false);
        item.transform.position = transform.position;
    }
}
