using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Button Comtroller", menuName = "New Button Comtroller")]

public class ButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //SceneManager.UnloadScene();
    }
    public void LoadScene(string levelName)
    {
        SceneManager.LoadScene(levelName, LoadSceneMode.Single);
    }
    public void MainMenuPlayButton(int delay)
    {
        if ()
        {

        }
        Invoke("LoadScene", delay);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void ActivateScreenTransition(Animator animator)
    {
        animator.SetBool("Activated", true);
    }
}
