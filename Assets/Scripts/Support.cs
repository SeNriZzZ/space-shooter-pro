using UnityEngine;
using UnityEngine.SceneManagement;

public class Support : MonoBehaviour
{
    public void BackPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Beta1Pressed()
    {
        SceneManager.LoadScene("Beta1");
    }

    public void Beta1_2Pressed()
    {
        SceneManager.LoadScene("Beta1.2");
    }

    public void Beta1_3Pressed()
    {
        SceneManager.LoadScene("Beta1.3");
    }
}