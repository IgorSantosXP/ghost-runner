using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject waitingGameStartHUD;
    [SerializeField] private GameObject pausedGameHUD;

    private bool isWaitingStart = false;

    private void Awake()
    {
        OnOpenGame();
    }

    void OnOpenGame()
    {
        waitingGameStartHUD.SetActive(true);
        pausedGameHUD.SetActive(false);
        isWaitingStart = true;
    }

    public void OnStartGame()
    {
        waitingGameStartHUD.SetActive(false);
        pausedGameHUD.SetActive(false);
        isWaitingStart = false;
    }

    public void OnPauseGame()
    {
        waitingGameStartHUD.SetActive(false);
        pausedGameHUD.SetActive(true);
    }

    public void OnResumeGame()
    {
        if (isWaitingStart)
        {
            waitingGameStartHUD.SetActive(true);
        }
        pausedGameHUD.SetActive(false);
    }
}
