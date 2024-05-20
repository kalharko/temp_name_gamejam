using System;
using UnityEngine;

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

    private void Awake()
    {
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
            animator.SetTrigger(HasFailed);
            angryAudioSource.time = 0;
            angryAudioSource.PlayOneShot(angryAudioClip);
            
        } 
        else if (state == GameState.HasSucceeded)
        {
            animator.SetTrigger(HasSucceeded);
            happyAudioSource.time = 0;
            happyAudioSource.PlayOneShot(happyAudioClip);
            
        }
    }
}
