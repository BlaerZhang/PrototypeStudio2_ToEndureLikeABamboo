using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class ShinobiControl : MonoBehaviour
{
    public KeyCode key;

    public Sprite slideDownSprite;

    private Animator animator;

    private SpriteRenderer spriteRenderer;

    [HideInInspector] public Rigidbody2D rb2D;

    private AudioSource slideSource;

    private RectTransform nameTagRect;

    public bool isKeyPressed;

    public bool hit;

    public bool landed;

    public GameObject uiIcon;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        slideSource = GetComponent<AudioSource>();
        nameTagRect = GetComponentInChildren<RectTransform>();

        rb2D.bodyType = RigidbodyType2D.Kinematic;
        hit = false;
        landed = false;
    }
    
    void Update()
    {
        if (AnimationManager.isPlayingFT)
        {
            nameTagRect.gameObject.SetActive(false);
        }
        else
        {
            nameTagRect.gameObject.SetActive(true);
        }
        
        if (Input.GetKeyDown(key) && !isKeyPressed && GameManager.isInGame)
        {
            transform.position = new Vector3(0, 33, 0);
            transform.localScale = Vector3.one;
            nameTagRect.localScale = Vector3.one;
            nameTagRect.localPosition = new Vector3(1.5f, 3, 0);
            animator.enabled = false;
            spriteRenderer.sprite = slideDownSprite;
            rb2D.bodyType = RigidbodyType2D.Dynamic;
            rb2D.gravityScale = 8;
            slideSource.Play();
            isKeyPressed = true;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Finish"))
        {
            hit = true;
            rb2D.bodyType = RigidbodyType2D.Kinematic;
            rb2D.velocity = Vector2.zero;
        }

        if (col.CompareTag("Ground"))
        {
            landed = true;
            rb2D.bodyType = RigidbodyType2D.Kinematic;
            rb2D.velocity = Vector2.zero;

            for (int i = 0; i < 1;)
            {
                bool allDistant = false;
                
                Vector3 deadPos = new Vector3(Random.Range(-10f, 10f), -37, 0);
                List<GameObject> players = GameObject.FindGameObjectsWithTag("Player").ToList();
                players.Remove(gameObject);
                foreach (GameObject player in players)
                {
                    if ((player.transform.position - deadPos).magnitude > 3)
                    {
                        allDistant = true;
                    }
                    else
                    {
                        allDistant = false;
                        break;
                    }
                }

                if (allDistant)
                {
                    transform.position = deadPos;
                    i++;
                }
            }
        }
    }
}
