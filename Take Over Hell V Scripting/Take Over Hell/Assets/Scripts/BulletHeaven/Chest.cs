using System;
using Unity.Mathematics;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject pickUp;
    [SerializeField] private int life = 10;

    public void TakeDamage(int damage)
    {
        life -= damage;

        if (life < 0)
        {
            Instantiate(pickUp,transform.position, Quaternion.identity);
            Destroy(gameObject, 0.1f);
        }
    }
}
