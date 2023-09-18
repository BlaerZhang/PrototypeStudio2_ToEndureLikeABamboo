using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class TargetControl : MonoBehaviour
{
    public AudioSource singingSource;
    public AudioSource voiceSource;
    private Animator animator;
    private bool isStartMoving;

    void Start()
    {
        animator = GetComponent<Animator>();
        // audioSource = GetComponentInChildren<AudioSource>();
        // voiceSource = GetComponent<AudioSource>();
    }
    
    
    void Update()
    {
        if (!isStartMoving && !AnimationManager.isPlayingFT)
        {
            transform.DOMoveX(20, Random.Range(30, 40)).SetEase(Ease.Linear);
            isStartMoving = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            voiceSource.Play();
            
            singingSource.Stop();

            transform.DOKill();

            animator.enabled = false;

        }
    }
}
