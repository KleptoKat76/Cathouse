using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    private static bool tutorialPlayed;
    public float sceneChangeDelay;
    public void LoadScene(string levelName)
    {
        SceneManager.LoadScene(levelName, LoadSceneMode.Single);
    }
    public void DelayedLoadScene(string name)
    {
        StartCoroutine(DelayedLoadScene(sceneChangeDelay, name));
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void ActivateScreenTransition(Animator animator)
    {
        animator.gameObject.SetActive(true);
        animator.SetBool("Activated", true);
    }
    private IEnumerator DelayedLoadScene(float delay, string name)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(name);
    }
}
