using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Sequence = DG.Tweening.Sequence;

public class AnimationManager : MonoBehaviour
{
    public TextMeshProUGUI display;
    public TextMeshProUGUI skip;
    public Image topMask;
    public Image bottomMask;
    
    private Sequence firstTutorialAnimation;
    // private Sequence tutorialAnimation;
    private Sequence skipAnimation;

    public static bool isPlayingFT;
    // private bool isPlayingT;
    
    void Start()
    {
        firstTutorialAnimation = DOTween.Sequence();
        skipAnimation = DOTween.Sequence();
        if (GameManager.isFirstTimeInGame)
        {
            firstTutorialAnimation
                .Insert(0,topMask.rectTransform.DOAnchorPosY(0,0))
                .Insert(0,bottomMask.rectTransform.DOAnchorPosY(0,0))
                .AppendInterval(1)
                .Append(display.DOText("", 0))
                .Append(display.DOText("To endure like a bamboo...", 3))
                .AppendInterval(1)
                .Append(display.DOText("", 0))
                .AppendInterval(1)
                .Append(display.DOText("What you are going to see might be a bit absurd, but...", 3))
                .Join(GetComponent<GameManager>().topCam.transform.DOMoveY(33, 5))
                .AppendInterval(1)
                .Append(display.DOText("", 0))
                .Append(display.DOText("Yes..."+MenuManager.numberOfPlayer+" Shinobi on a single bamboo", 2))
                .AppendInterval(1)
                .Append(display.DOText("", 0))
                .Append(display.DOText("It's a long story, but what you need to know is...", 2))
                .AppendInterval(2)
                .Append(display.DOText("", 0))
                .Append(display.DOText("We are trying to assassinate the same target", 2))
                .AppendInterval(2)
                .Append(display.DOText("", 0))
                .Append(display.DOText("So we agreed to have a match", 2))
                .AppendInterval(1)
                .Append(display.DOText("", 0))
                .Append(display.DOText("The target likes practise singing here", 2))
                .AppendInterval(1)
                .Append(display.DOText("", 0))
                .Append(display.DOText("The bamboo we are hiding is too long\nWe can't see from here", 4))
                .AppendInterval(2)
                .Append(display.DOText("", 0))
                .Append(display.DOText("Yet we can hear his voice", 1))
                .AppendInterval(2)
                .Append(display.DOText("", 0))
                .Append(display.DOText("When the voice is about to move underneath...", 3))
                .AppendInterval(1)
                .Append(display.DOText("", 0))
                .Append(display.DOText("Press the key to slide down the bamboo", 2))
                .AppendInterval(2)
                .Append(display.DOText("", 0))
                .Append(display.DOText("and execute", 1))
                .AppendInterval(2)
                .Append(display.DOText("", 0))
                .Append(display.DOText("The executioner get 3 pt", 2))
                .AppendInterval(1)
                .Append(display.DOText("", 0))
                .Append(display.DOText("Others get fewer pt according to the timing", 2))
                .AppendInterval(1)
                .Append(display.DOText("", 0))
                .Append(display.DOText("If no one hits the target\nThen no one gets point", 3))
                .AppendInterval(2)
                .Append(display.DOText("", 0))
                .Append(display.DOText("After 3 rounds, \nWhoever with the highest score wins", 3))
                .AppendInterval(1)
                .Append(display.DOText("", 0))
                .Append(display.DOText("Listen...The Target is coming closer...", 2))
                .AppendInterval(1)
                .Append(display.DOText("", 0))
                .Join(topMask.rectTransform.DOAnchorPosY(300,1))
                .Join(bottomMask.rectTransform.DOAnchorPosY(-300,1));
            
            firstTutorialAnimation.Play();
            
            skipAnimation
                .Append(skip.DOText("Press S to Skip Intro", 0))
                .AppendInterval(1)
                .Append(skip.DOText("", 0))
                .AppendInterval(1)
                .SetLoops(-1,LoopType.Restart);
            
            skipAnimation.Play();
        }

        // tutorialAnimation = DOTween.Sequence();
        // tutorialAnimation
        //     .Append(display.DOText("", 0))
        //     .Append(display.DOText("OK...a quick review", 1))
        //     .AppendInterval(1)
        //     .Append(display.DOText("", 0))
        //     .Append(display.DOText("I always have full control of my body\nby my own free will", 4))
        //     .AppendInterval(2)
        //     .Append(display.DOText("", 0))
        //     .Append(display.DOText("Press ←/→ t Turn Myself Clockwise/Counterclockwise", 3))
        //     .AppendInterval(2)
        //     .Append(display.DOText("", 0))
        //     .Append(display.DOText("Press ↓ to Stand At Ease", 2))
        //     .AppendInterval(1)
        //     .Append(display.DOText("", 0))
        //     .Append(display.DOText("Just Click on the Student that I think is me", 3))
        //     .AppendInterval(1)
        //     .Append(display.DOText("", 0))
        //     .Append(display.DOText("But I only have one chance", 1))
        //     .AppendInterval(2)
        //     .Append(display.DOText("", 0))
        //     .Pause();
        // tutorialAnimation.SetAutoKill(false);
    }
    
    void Update()
    {
        isPlayingFT = firstTutorialAnimation.IsPlaying();

        if (isPlayingFT == false) 
        {
            skipAnimation.Kill();
            skip.text = "";
        }

        if (isPlayingFT)
        {
            GameManager.isInGame = false;
        }

        if (isPlayingFT)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                firstTutorialAnimation.Kill();
                topMask.rectTransform.DOAnchorPosY(300,1);
                bottomMask.rectTransform.DOAnchorPosY(-300,1);
                GetComponent<GameManager>().topCam.transform.DOMoveY(33, 1);
                display.text = "";
                skip.text = "";
            }
        }
    }

    public void ShowWinner(string playerName)
    {
        display.DOText(playerName + " Wins", 2);
    }
}

