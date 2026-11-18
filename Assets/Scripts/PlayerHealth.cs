using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color startColor;
    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth;
    public Image healthBar;
    private void OnEnable()
    {
        GameController.OnReset += ResetHealth;
    }

    private void OnDisable()
    {
        GameController.OnReset -= ResetHealth;
    }

    void Start()
    {
        currentHealth = maxHealth;

        spriteRenderer = GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;

        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0) return;

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        UpdateHealthBar();
        StartCoroutine(FlashRed());
        if (currentHealth <= 0)
        {
            FindAnyObjectByType<GameController>().CheckWinner();
        }
    }
    private void UpdateHealthBar()
    {
        healthBar.fillAmount = (float)currentHealth / maxHealth;
    }

    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);

        spriteRenderer.color = startColor;
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        StopAllCoroutines();
        spriteRenderer.color = startColor;

        UpdateHealthBar();
    }
}