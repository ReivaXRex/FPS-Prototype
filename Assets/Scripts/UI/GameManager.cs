using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas gameOverCanvas;

    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null) Debug.Log("Game Manager is NULL");

            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // Turn off the Game Over Screen.
        gameOverCanvas.enabled = false;
    }

    /// <summary>
    /// Pause the game and enable the Game Over Screen.
    /// </summary>
    public void HandleDeath()
    {
        // Turn on the Game Over Screen.
        gameOverCanvas.enabled = true;

        // Pause the Game.
        Time.timeScale = 0;
        // Unlock the Mouse.
        Cursor.lockState = CursorLockMode.None;
        // Make the cursor visible.
        Cursor.visible = true;
    }

    /// <summary>
    /// Restart level.
    /// </summary>
    public void Restart()
    {
        // Load the first level.
        SceneManager.LoadScene(0);

        // Resume time
        Time.timeScale = 1;
    }

    /// <summary>
    /// Quit the game, and exit the application.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}