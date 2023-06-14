using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBallController : MonoBehaviour
{
    private float collisionDisableTime;
    private UnityEngine.Collider2D shooterCollider;
    private UnityEngine.Collider2D ballCollider;
    private bool disableCollision = false;
    private SpriteRenderer spriteRenderer;
    private Color initialColor;
    private float waitDuration;
    private float fadeDuration;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (disableCollision)
        {
            // spike ball has been flagged to disable collisions

            // decrement collision disable time
            collisionDisableTime -= Time.deltaTime;

            // once time has reached 0, reenable collisions
            if (collisionDisableTime < 0)
            {
                Physics2D.IgnoreCollision(ballCollider, shooterCollider, false);
                disableCollision = false;
            }
        }
    }

    public void DisableCollision(UnityEngine.Collider2D collision, float duration)
    {
        // set collision disable time and shooter collision and ball collision
        collisionDisableTime = duration;
        shooterCollider = collision;
        ballCollider = GetComponent<Collider2D>();

        // flag to disable collisions
        disableCollision = true;

        // disable collisions between shooter and spikeball
        Physics2D.IgnoreCollision(ballCollider, shooterCollider, true);
    }

    public void FadeOut(float wait, float fade)
    {
        // get sprite renderer
        spriteRenderer = GetComponent<SpriteRenderer>();
        // get initial color
        initialColor = spriteRenderer.color;
        // set wait and fade durations
        waitDuration = wait;
        fadeDuration = fade;
        // start fade out routine
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        // Wait for the specified duration before the object disappears
        yield return new WaitForSeconds(waitDuration);

        // Fade out the object by reducing its alpha value gradually over the fadeDuration
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            // reduce alpha with Lerp
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            // set new alpha
            spriteRenderer.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);

            // increment elapsed time
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Destroy the object
        Destroy(gameObject);
    }
}
