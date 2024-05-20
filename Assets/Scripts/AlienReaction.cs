using System;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class AngryAlien : MonoBehaviour
{
    private Animator animator;
    private static readonly int HasFailed = Animator.StringToHash("HasFailed");
    private static readonly int HasSucceeded = Animator.StringToHash("HasSucceeded");

    //audio sources
    public AudioSource angryAudioSource;
    public AudioClip angryAudioClip;
    public AudioSource happyAudioSource;
    public AudioClip happyAudioClip;

    //animate alien behavior
    public SpriteRenderer spriteRenderer;
    public Sprite spriteHappy;
    public Sprite spriteAngry;
    public Sprite spriteNormal;

    private void Awake()
    {
        spriteRenderer.sprite = spriteNormal;
        GameManager.OnGameStateChanged += OnGameStateChanged;
        animator = GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState state)
    {
        if (state == GameState.HasFailed)
        {
            spriteRenderer.sprite = spriteAngry;
            //StartCoroutine(AlienBehaviorSprite(spriteAngry, spriteNormal));
            animator.SetTrigger(HasFailed);
            angryAudioSource.time = 0;
            angryAudioSource.PlayOneShot(angryAudioClip);
            Invoke(nameof(ResetSprite), 1f);
            
        } 
        else if (state == GameState.HasSucceeded)
        {

            //StartCoroutine(AlienBehaviorSprite(spriteAngry, spriteNormal));
            spriteRenderer.sprite = spriteHappy;
            animator.SetTrigger(HasSucceeded);
            happyAudioSource.time = 0;
            happyAudioSource.PlayOneShot(happyAudioClip);
            Invoke(nameof(ResetSprite), 1f);
            
        }
        
    }

    private void ResetSprite()
    {
        spriteRenderer.sprite = spriteNormal;
    }

    /*IEnumerator AlienBehaviorSprite(Sprite sprite1, Sprite sprite2)
    {
        while (true)
        {
            spriteRenderer.sprite = sprite1;
            yield return new WaitForSeconds(1f);
            spriteRenderer.sprite = sprite2;
        }
    }*/

}
