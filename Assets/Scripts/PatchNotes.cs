using UnityEngine;
using UnityEngine.SceneManagement;

public class PatchNotes : MonoBehaviour
{
    public void BackPressed()
    {
        SceneManager.LoadScene("Support");
    }
}