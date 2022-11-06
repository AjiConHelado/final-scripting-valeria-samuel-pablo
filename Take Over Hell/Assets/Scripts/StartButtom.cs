using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtom : MonoBehaviour
{
    public void StartGameplay()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
