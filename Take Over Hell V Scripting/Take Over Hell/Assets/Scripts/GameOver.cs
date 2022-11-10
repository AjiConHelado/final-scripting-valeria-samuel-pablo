using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChacaterGameOver : MonoBehaviour
{
    public GameObject gameOverPanel;

    public void CharacterGameOver()
    {
        Debug.Log("Game Over");
        gameOverPanel.SetActive(true);
    }
}
