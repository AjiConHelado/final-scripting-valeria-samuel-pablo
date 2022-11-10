using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    int level = 1;
    int experience = 0;
    [SerializeField] XPBar xpBar;
    private RandomSetUp rndSetUp;
    
    int TOP_LEVEL_UP
    {
        get
        {
            return level * 1000;
        }
    }

    private void Awake()
    {
        rndSetUp = FindObjectOfType<RandomSetUp>();
    }

    private void Start()
    {
        xpBar.UpdateExperienceSlider(experience, TOP_LEVEL_UP);
        xpBar.SetLevelText(level);
    }

    public void AddExperience(int amount)
    {
        experience += amount;
        CheckLevelUp();
        xpBar.UpdateExperienceSlider(experience, TOP_LEVEL_UP);
    }

    public void CheckLevelUp()
    {
        if (experience >= TOP_LEVEL_UP && FindObjectOfType<Character>().currentHp > 0)
        {
            experience -= TOP_LEVEL_UP;
            level += 1;
            xpBar.SetLevelText(level);
            StartCoroutine(Determine());
        }
    }

    void OpenStore()
    {
        Time.timeScale = 0;
        rndSetUp.OnOff(true);
    }

    IEnumerator Determine()
    {
        yield return new WaitForEndOfFrame();
        
        OpenStore();

        yield return null;
    }
}
