using UnityEngine;

public class CharacterGameOver : MonoBehaviour
{
    public CanvasGroup gameOverPanel;

    public void GameOver()
    {
        Debug.Log("Game Over");
        GetComponent<PlayerController>().enabled = false;
        gameOverPanel.interactable = true;
        gameOverPanel.alpha = 1;
        gameOverPanel.blocksRaycasts = true;
    }
}