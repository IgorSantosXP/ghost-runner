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
    public int TravelledDistance { get; private set; }
    public int HighestDistance {get; private set; }

    private void Awake()
    {
        HighestDistance = PlayerPrefs.GetInt("highestDistance");
        player.enabled = false;
        isWaitingStart = true;
        isPaused = false;
    }

    void Update()
    {
        TravelledDistance = (int) player.transform.position.z;
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
        CheckHighestDistance();
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

    public void OnQuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    private void CheckHighestDistance()
    {
        if(TravelledDistance > HighestDistance)
        {
            PlayerPrefs.SetInt("highestDistance", TravelledDistance);
        }
    }

    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        isWaitingStart = true;
    }
}
