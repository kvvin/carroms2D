using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public void LoadHomeScene()
    {
        SceneManager.LoadScene("homeScene");
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("gameScene");
    }

    public void LoadGameOverScene()
    {
        SceneManager.LoadScene("gameover");
    }

    public void StartGame()
    {
        LoadGameScene();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}