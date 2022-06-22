using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameMode gameMode;

    [SerializeField] private GameObject waitingGameStartHUD;
    [SerializeField] private GameObject pausedGameHUD;
    [SerializeField] private GameObject playingGameHUD;
    
    [SerializeField] private TMP_Text travelledDistance;
    [SerializeField] private TMP_Text highestDistance;

    private bool isWaitingStart = false;

    private void Awake()
    {
        OnOpenGame();
    }

    private void Start()
    {
        highestDistance.text = $"Highest Distance: {gameMode.HighestDistance}m";
    }

    private void Update()
    {
        travelledDistance.text = $"{gameMode.TravelledDistance}m";
    }

    void OnOpenGame()
    {
        waitingGameStartHUD.SetActive(true);
        pausedGameHUD.SetActive(false);
        playingGameHUD.SetActive(false);
        isWaitingStart = true;
    }

    public void OnStartGame()
    {
        waitingGameStartHUD.SetActive(false);
        pausedGameHUD.SetActive(false);
        playingGameHUD.SetActive(true);
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
