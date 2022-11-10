using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipWeapon : MonoBehaviour
{
    public float timeToAttack=4f;
    float timer;

    [SerializeField] private GameObject leftWhipObject, rightWhipObject, topWhipObject, botWhipObject;

    [SerializeField] private Vector2 whipAttackSize = new Vector2(4f, 2f);
    public int whipDamage = 1;
    
    private PlayerController playerController;
    private MusicPlayer player;
    
    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        player = Camera.main.GetComponent<MusicPlayer>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            Attack();
        }
    }

    private void Attack()
    {
        //Debug.Log("Attack");
        timer = timeToAttack;

        if (playerController.lastHorizontalVector > 0)
        {
            rightWhipObject.SetActive(true);
            Collider2D[] colliders = Physics2D.OverlapBoxAll(rightWhipObject.transform.position, whipAttackSize, 0f);
            ApplyDamage(colliders);
        }
        else if (playerController.lastHorizontalVector < 0)
        {
            leftWhipObject.SetActive(true);
            Collider2D[] colliders = Physics2D.OverlapBoxAll(leftWhipObject.transform.position, whipAttackSize, 0f);
            ApplyDamage(colliders);
        }
        
        if (playerController.lastVerticalVector < 0)
        {
            botWhipObject.SetActive(true);
            Collider2D[] colliders = Physics2D.OverlapBoxAll(botWhipObject.transform.position, whipAttackSize, 0f);
            ApplyDamage(colliders);
        }
        else if (playerController.lastVerticalVector > 0)
        {
            topWhipObject.SetActive(true);
            Collider2D[] colliders = Physics2D.OverlapBoxAll(topWhipObject.transform.position, whipAttackSize, 0f);
            ApplyDamage(colliders);
        }
        
        player.PlaySound(player.Whipe);
    }

    private void ApplyDamage(Collider2D[] colliders)
    {
        for (int i=0;i<colliders.Length;i++)
        {
            Enemy e = colliders[i].GetComponent<Enemy>();
            
            if (e != null)
            {
                colliders[i].GetComponent<Enemy>().TakeDamage(whipDamage);
            }
            else if (colliders[i].gameObject.TryGetComponent(typeof(Chest), out Component chest))
            {
                Chest a = (Chest)chest;
                
                a.TakeDamage(whipDamage);
            }
        }
    }
}
