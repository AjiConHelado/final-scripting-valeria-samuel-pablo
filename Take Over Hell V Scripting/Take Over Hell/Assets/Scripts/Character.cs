using System;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [HideInInspector] public Level level;
    [SerializeField] private float healTimer;
    [SerializeField] private int maxHp = 1000;

    public int healAmount = 0;
    public int currentHp = 1000;
    private float timer;
    private byte zero = 0;

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
        timer -= Time.deltaTime;
        if (timer < zero)
        {
            Heal(healAmount);
            timer = healTimer;
        }
        
        slider.value = currentHp;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Taking Dmg");
        
        currentHp -= damage;

        if (currentHp <= 0)
        {
            FindObjectOfType<CharacterGameOver>().GameOver();
            Debug.Log("Character is dead");
        }
    }

    public void Heal(int amount)
    {
        if(currentHp <= 0) return;

        currentHp += amount;
        
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
    }
}
