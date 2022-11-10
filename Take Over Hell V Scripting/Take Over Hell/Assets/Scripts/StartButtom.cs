using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtom : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject three, two, one;
    public void StartGameplay()
    {
        StartCoroutine(StartAnim());
    }

    IEnumerator StartAnim()
    {
        anim.SetTrigger("Change");

        yield return new WaitForSeconds(1);
        
        three.SetActive(true);
        
        yield return new WaitForSeconds(0.9f);

        three.SetActive(false);
        
        yield return new WaitForSeconds(0.1f);
        
        two.SetActive(true);
        
        yield return new WaitForSeconds(0.9f);

        two.SetActive(false);
        
        yield return new WaitForSeconds(0.1f);
        
        one.SetActive(true);
        
        yield return new WaitForSeconds(1f);
        
        SceneManager.LoadScene(1);
        
        yield return null;
    }
}
