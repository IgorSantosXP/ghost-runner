using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private Animator animator;

    private bool isWaitingStart = false;

    private void Awake()
    {
        player.enabled = false;
        isWaitingStart = true;
    }

    void Update()
    {
        if (isWaitingStart)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger(PlayerAnimationConstants.StartGameTrigger);
                player.enabled = true;
                isWaitingStart = false;
            }
        }
    }

    public void OnGameOver()
    {
        animator.SetTrigger(PlayerAnimationConstants.DieTrigger);
        player.Die();
        StartCoroutine(RestartGame());
    }

    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        isWaitingStart = true;
    }
}
