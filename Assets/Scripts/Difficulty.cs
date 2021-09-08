using UnityEngine;
using UnityEngine.SceneManagement;

public class Difficulty : MonoBehaviour
{
    public void EasyPressed()
    {
        SceneManager.LoadScene("Game");
    }

    public void MediumPressed()
    {
        SceneManager.LoadScene("GameMedium");
    }

    public void HardPressed()
    {
        SceneManager.LoadScene("GameHard");
    }

    public void MainMenuPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }
}