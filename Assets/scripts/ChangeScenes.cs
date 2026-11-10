using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScenes : MonoBehaviour
{
    public void GoToScene2()
    {
        SceneManager.LoadScene("Appartment 1");
    }
     public void GoToScene3()
    {
        SceneManager.LoadScene("Appartment 2");
    }
    public void GoToScene4()
    {
        SceneManager.LoadScene("Appartment 3");
    }
    public void GoToScene5()
    {
        SceneManager.LoadScene("Appartment 4");
    }
    public void GoToScene6()
    {
        SceneManager.LoadScene("Appartment 5");
    }
    public void GoToFight()
    {
        SceneManager.LoadScene("EnemySelectMenu");
    }
     public void GoToActualFight()
    {
        SceneManager.LoadScene("MainScene");
    }
}

