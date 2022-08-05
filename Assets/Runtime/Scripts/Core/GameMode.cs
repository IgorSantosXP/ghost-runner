using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private Animator animator;
    [SerializeField] private UIController uiController;
    [SerializeField] private MusicPlayer musicPlayer;

    private bool isWaitingStart = false;
    private bool isPaused = false;
    public int TravelledDistance { get; private set; }
    public int HighestDistance {get; private set; }
    public float ForwardSpeed { get; private set; } = 10f;
    private bool isPlaying = false;
    private bool isDead = false;
    private float maxForwardSpeed = 50f;
    private float timeToMaxForwardSpeed = 300f;
    private float startGameTime;
    private float startGameSpeed;

    private void Awake()
    {
        musicPlayer.PlayMusic();
        startGameSpeed = ForwardSpeed;
        HighestDistance = PlayerPrefs.GetInt("highestDistance");
        player.enabled = false;
        isWaitingStart = true;
        isPaused = false;
        isDead = false;
    }

    void Update()
    {
        if (isPlaying && !isDead)
        {
            float timePercent = (Time.time - startGameTime) / timeToMaxForwardSpeed;
            ForwardSpeed = Mathf.Lerp(startGameSpeed, maxForwardSpeed, timePercent);

            TravelledDistance = (int) player.transform.position.z;
        }
        

        
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
        isPlaying = true;
        isDead = false;
        startGameTime = Time.time;
        uiController.OnStartGame();
    }

    public void OnGameOver()
    {
        animator.SetTrigger(PlayerAnimationConstants.DieTrigger);
        ForwardSpeed = 0;
        isDead = true;
        isPlaying = false;
        player.Die();
        CheckHighestDistance();
        StartCoroutine(RestartGame());
    }

    public void OnPauseGame()
    {
        isPaused = true;
        isPlaying = false;
        Time.timeScale = 0;
        uiController.OnPauseGame();
    }

    public void OnResumeGame()
    {
        isPaused = false;
        isPlaying = true;
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
