using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverPanel;

    public void CharacterGameOver()
    {
        Debug.Log("Game Over");
        GetComponent<PlayerController>().enabled = false;
        gameOverPanel.SetActive(true);
    }
}
