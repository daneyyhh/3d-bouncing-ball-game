using UnityEngine;
using System.Collections;

/// <summary>
/// Advanced animation controller for smooth, professional animations
/// Handles pickup rotations, bounce effects, UI transitions, and more
/// </summary>
public class AnimationController : MonoBehaviour
{
    [Header("Rotation Animation")]
    public bool rotateObject = true;
    public Vector3 rotationSpeed = new Vector3(0, 100, 0);
    public bool smoothRotation = true;
    
    [Header("Bounce Animation")]
    public bool bounceEffect = false;
    public float bounceHeight = 0.5f;
    public float bounceSpeed = 2f;
    private Vector3 startPosition;
    
    [Header("Scale Pulse Animation")]
    public bool pulseEffect = false;
    public float pulseScale = 1.2f;
    public float pulseSpeed = 1f;
    private Vector3 originalScale;
    
    [Header("Color Fade Animation")]
    public bool fadeColors = false;
    public Color colorA = Color.white;
    public Color colorB = Color.yellow;
    public float fadeSpeed = 1f;
    private Renderer objectRenderer;
    
    [Header("Smooth Movement")]
    public bool smoothFollow = false;
    public Transform targetToFollow;
    public float followSpeed = 5f;
    public Vector3 offset;
    
    void Start()
    {
        startPosition = transform.position;
        originalScale = transform.localScale;
        objectRenderer = GetComponent<Renderer>();
    }
    
    void Update()
    {
        if (rotateObject)
            RotateAnimation();
            
        if (bounceEffect)
            BounceAnimation();
            
        if (pulseEffect)
            PulseAnimation();
            
        if (fadeColors && objectRenderer != null)
            ColorFadeAnimation();
            
        if (smoothFollow && targetToFollow != null)
            SmoothFollowAnimation();
    }
    
    void RotateAnimation()
    {
        if (smoothRotation)
        {
            transform.Rotate(rotationSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            transform.Rotate(rotationSpeed * Time.deltaTime);
        }
    }
    
    void BounceAnimation()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
    
    void PulseAnimation()
    {
        float scale = 1f + (Mathf.Sin(Time.time * pulseSpeed) * (pulseScale - 1f));
        transform.localScale = originalScale * scale;
    }
    
    void ColorFadeAnimation()
    {
        float lerp = Mathf.PingPong(Time.time * fadeSpeed, 1f);
        objectRenderer.material.color = Color.Lerp(colorA, colorB, lerp);
    }
    
    void SmoothFollowAnimation()
    {
        Vector3 targetPosition = targetToFollow.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
    
    // Public methods for triggering animations
    public void PlayCollectAnimation()
    {
        StartCoroutine(CollectAnimationCoroutine());
    }
    
    IEnumerator CollectAnimationCoroutine()
    {
        float duration = 0.5f;
        float elapsed = 0f;
        Vector3 startScale = transform.localScale;
        Vector3 startPos = transform.position;
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / duration;
            
            // Scale up then down
            float scale = Mathf.Sin(progress * Mathf.PI) * 0.5f + 1f;
            transform.localScale = startScale * scale;
            
            // Move up
            transform.position = startPos + Vector3.up * (progress * 2f);
            
            // Fade out
            if (objectRenderer != null)
            {
                Color color = objectRenderer.material.color;
                color.a = 1f - progress;
                objectRenderer.material.color = color;
            }
            
            yield return null;
        }
        
        Destroy(gameObject);
    }
    
    public void PlayDamageShake()
    {
        StartCoroutine(DamageShakeCoroutine());
    }
    
    IEnumerator DamageShakeCoroutine()
    {
        Vector3 originalPosition = transform.position;
        float duration = 0.3f;
        float magnitude = 0.1f;
        
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            float z = Random.Range(-1f, 1f) * magnitude;
            
            transform.position = originalPosition + new Vector3(x, y, z);
            
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        transform.position = originalPosition;
    }
    
    public void PlayScalePopAnimation()
    {
        StartCoroutine(ScalePopCoroutine());
    }
    
    IEnumerator ScalePopCoroutine()
    {
        Vector3 originalScale = transform.localScale;
        float duration = 0.2f;
        
        // Scale up
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float scale = Mathf.Lerp(1f, 1.3f, elapsed / duration);
            transform.localScale = originalScale * scale;
            yield return null;
        }
        
        // Scale back down
        elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float scale = Mathf.Lerp(1.3f, 1f, elapsed / duration);
            transform.localScale = originalScale * scale;
            yield return null;
        }
        
        transform.localScale = originalScale;
    }
}

/// <summary>
/// UI Animation helper for smooth menu transitions
/// </summary>
public class UIAnimator : MonoBehaviour
{
    public void FadeIn(CanvasGroup canvasGroup, float duration = 0.3f)
    {
        StartCoroutine(FadeCanvasGroup(canvasGroup, 0f, 1f, duration));
    }
    
    public void FadeOut(CanvasGroup canvasGroup, float duration = 0.3f)
    {
        StartCoroutine(FadeCanvasGroup(canvasGroup, 1f, 0f, duration));
    }
    
    IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            cg.alpha = Mathf.Lerp(start, end, elapsed / duration);
            yield return null;
        }
        cg.alpha = end;
    }
    
    public void SlideIn(RectTransform rect, Vector2 targetPosition, float duration = 0.3f)
    {
        StartCoroutine(SlideToPosition(rect, rect.anchoredPosition, targetPosition, duration));
    }
    
    IEnumerator SlideToPosition(RectTransform rect, Vector2 start, Vector2 end, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            rect.anchoredPosition = Vector2.Lerp(start, end, elapsed / duration);
            yield return null;
        }
        rect.anchoredPosition = end;
    }
}
