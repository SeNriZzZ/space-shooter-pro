using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("Difficulty");
    }

    public void SettingsButton()
    {
        SceneManager.LoadScene("Support");
    }

    public void QuitButton()
    {
        Application.Quit();
        Debug.Log("QUIT");
    }

    public void PatchNotes()
    {
        SceneManager.LoadScene("Patch");
    }

    public void HowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
    }
}