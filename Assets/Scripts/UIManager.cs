using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _scoretext;
    [SerializeField] private Text _laserText;
    [SerializeField] private Text _laserCooldown;
    [SerializeField] private Sprite[] _livesSprites;
    [SerializeField] private Image _livesImage;
    [SerializeField] private Text _gameOver;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private GameObject _pauseMenu;
    public bool GameIsPaused = false;
    private bool _gameOverBool = false;

    void Start()
    {
        _scoretext.text = "Score: " + 0;
        _gameOver.gameObject.SetActive(false);
        _restartButton.gameObject.SetActive(false);
        _mainMenuButton.gameObject.SetActive(false);
        _pauseMenu.SetActive(false);
        Time.timeScale = 1;
        StartCoroutine(ShowLaserInfo());
    }

    void Update()
    {
        if (_gameOverBool == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameIsPaused == true)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }

        if (GameIsPaused == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Resume();
            }
        }
    }

    public void UpdateScore(int playerScore)
    {
        _scoretext.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        _livesImage.sprite = _livesSprites[currentLives];
    }

    public void GameOver()
    {
        _gameOverBool = true;
        _gameOver.gameObject.SetActive(true);
        _restartButton.gameObject.SetActive(true);
        _mainMenuButton.gameObject.SetActive(true);
        StartCoroutine(GameFlickerRoutine());
    }

    IEnumerator GameFlickerRoutine()
    {
        while (true)
        {
            _gameOver.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOver.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void ButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenuClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Resume()
    {
        Time.timeScale = 1;
        _pauseMenu.SetActive(false);
        GameIsPaused = false;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        _pauseMenu.SetActive(true);
        GameIsPaused = true;
    }

    public void ResumePressed()
    {
        Resume();
    }

    public void MainPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator ShowLaserInfo()
    {
        _laserText.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        _laserText.gameObject.SetActive(false);
    }

    public void Cooldown(string cooldown)
    {
        _laserCooldown.text = "LASER  COOLDOWN: " + cooldown;
    }
}