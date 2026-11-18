using UnityEngine;
using TMPro;
using System;

public class GameController : MonoBehaviour
{
    [Header("Players")]
    public PlayerHealth player1;
    public PlayerHealth player2;
    [Header("Spawn Positions")]
    public Transform player1Spawn;
    public Transform player2Spawn;
    [Header("UI")]
    public GameObject gameOverPanel;
    public TMP_Text winnerText;
    public static event Action OnReset;
    private bool gameEnded = false;
    private void Start()
    {
        gameOverPanel.SetActive(false);
    }
    public void CheckWinner()
    {
        if (gameEnded) return;

        if (player1.currentHealth <= 0)
        {
            EndGame(player2.name);
        }
        else if (player2.currentHealth <= 0)
        {
            EndGame(player1.name);
        }
    }
    private void EndGame(string winnerName)
    {
        gameEnded = true;

        winnerText.text = winnerName + " Wins!";
        gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }
    public void ResetGame()
    {
        Time.timeScale = 1f;

        gameEnded = false;
        gameOverPanel.SetActive(false);

        player1.transform.position = player1Spawn.position;
        player2.transform.position = player2Spawn.position;

        Rigidbody2D body1 = player1.GetComponent<Rigidbody2D>();
        Rigidbody2D body2 = player2.GetComponent<Rigidbody2D>();

        body1.linearVelocity = Vector2.zero;
        body2.linearVelocity = Vector2.zero;

        OnReset?.Invoke();
    }
}