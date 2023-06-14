using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Credit for theme music: https://pixabay.com/music/video-games-music-for-arcade-style-game-146875/
// Credit for all sound effects: https://mixkit.co/free-sound-effects
// Credit for code tutorial: https://www.youtube.com/watch?v=JnbDxG04i7c

public class SoundEffects : MonoBehaviour
{
    public AudioSource ButtonClick;
    public AudioSource ButtonHover;
    public AudioSource Dash;
    public AudioSource Falling;
    public AudioSource Hit;
    public AudioSource Jump;
    public AudioSource LevelComplete;
    public AudioSource Run;

    public void PlayButtonClick()
    {
        ButtonClick.Play();
    }

    public void PlayButtonHover()
    {
        ButtonHover.Play();
    }

    public void PlayDash()
    {
        Dash.Play();
    }
    
    public void PlayFalling()
    {
        Falling.Play();
    }

    public void PlayHit()
    {
        Hit.Play();
    }

    public void PlayJump()
    {
        Jump.Play();
    }

    public void PlayLevelComplete()
    {
        LevelComplete.Play();
    }

    public void PlayRun()
    {
        Run.Play();
    }
}