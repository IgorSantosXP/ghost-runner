using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private Animator animator;
    [SerializeField] private UIController uiController;

    private bool isWaitingStart = false;
    private bool isPaused = false;

    private void Awake()
    {
        player.enabled = false;
        isWaitingStart = true;
        isPaused = false;
    }

    void Update()
    {
        if (isWaitingStart && !isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnStartGame();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                OnPauseGame();
            }
            else
            {
                OnResumeGame();
            }
        }
    }

    public void OnStartGame()
    {
        animator.SetTrigger(PlayerAnimationConstants.StartGameTrigger);
        player.enabled = true;
        isWaitingStart = false;
        uiController.OnStartGame();
    }

    public void OnGameOver()
    {
        animator.SetTrigger(PlayerAnimationConstants.DieTrigger);
        player.Die();
        StartCoroutine(RestartGame());
    }

    public void OnPauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        uiController.OnPauseGame();
    }

    public void OnResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        uiController.OnResumeGame();
    }

    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        isWaitingStart = true;
    }
}
