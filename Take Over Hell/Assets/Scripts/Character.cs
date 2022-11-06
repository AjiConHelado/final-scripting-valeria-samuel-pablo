using System;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public int maxHp =1000;
    public int currentHp = 1000;
    [SerializeField] private Slider slider;
    [HideInInspector] public Level level;

    private void Awake()
    {
        level = GetComponent<Level>();
    }

    private void Start()
    {
        slider.maxValue = maxHp;
    }

    private void Update()
    {
        slider.value = currentHp;
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;

        if (currentHp <= 0)
        {
            Debug.Log("Character is dead");
        }

    }

    public void Heal(int amount)
    {
        if(currentHp<=0){return;}

        currentHp += amount;
        
        if (currentHp>maxHp);
        {
            currentHp = maxHp;
        }

    }
}
