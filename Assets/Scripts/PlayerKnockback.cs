using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerKnockback : MonoBehaviour
{
    public Rigidbody2D body;
    [Header("Knockback")]
    public float strength = 6f;
    public float delay = 0.15f;
    public UnityEvent OnBegin;
    public UnityEvent OnDone;
    public bool isKnockedBack = false;
    public void PlayerFeedback(GameObject sender)
    {
        if (body == null || sender == null) return;

        StopAllCoroutines();
        Vector2 direction = ((Vector2)transform.position - (Vector2)sender.transform.position).normalized;
        isKnockedBack = true;

        OnBegin?.Invoke();
        body.linearVelocity = Vector2.zero;
        body.AddForce(direction * strength, ForceMode2D.Impulse);

        StartCoroutine(Reset());
    }
    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        isKnockedBack = false;
        OnDone?.Invoke();
    }
}