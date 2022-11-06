using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    int level = 1;
    int experience = 0;
    [SerializeField] XPBar xpBar;
    
    int TOP_LEVEL_UP
    {
        get
        {
            return level * 1000;
        }
    }

    public void AddExperience(int amount)
    {
        experience += amount;
        CheckLevelUp();
        xpBar.UpdateExperienceSlider(experience, TOP_LEVEL_UP);
    }

    private void Start()
    {
        xpBar.UpdateExperienceSlider(experience, TOP_LEVEL_UP);
        xpBar.SetLevelText(level);
    }

    public void CheckLevelUp()
    {
        if (experience >= TOP_LEVEL_UP)
        {
            experience -= TOP_LEVEL_UP;
            level += 1;
            xpBar.SetLevelText(level);
        }
    }
}
