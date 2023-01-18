using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioService : MonoBehaviour
{
    [SerializeField] private TimeLineSetting setting;
    [SerializeField] private GameTimeLine gameTimeLine;
    [HideInInspector]
    public AudioClip bgAudio, dealAudio,hitAudio,standAudio,
         winAudio, loseAudio, betAudio, drawAudio, emptyBalanceAudio;

     [SerializeField] AudioSource button, button2, button3, gameEventsTimeLine, bg;
    [SerializeField] private AudioListener audioListener;
    [SerializeField] private Sprite iconAudioOnEnabled, iconAudioOffEnabled;
    [SerializeField] private Button buttonAudioActive;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Initialized();
       

        buttonAudioActive.onClick.AddListener(AudioHandlerEnabled);
    }
    public void AudioHandlerEnabled()
    {
        if (bg.enabled)
        {
            button.enabled = false;
                button2.enabled = false;
            button3.enabled = false;
            gameEventsTimeLine.enabled = false;
            bg.enabled = false;

            buttonAudioActive.GetComponent<Image>().sprite = iconAudioOffEnabled;
        }
        else
        {
            button.enabled = true;
            button2.enabled = true;
            button3.enabled = true;
            gameEventsTimeLine.enabled = true;
            bg.enabled = true;
            buttonAudioActive.GetComponent<Image>().sprite = iconAudioOnEnabled;
        }
    }
    private void Start()
    {
        if (bgAudio)
        {
            bg.clip = bgAudio;
            bg.Play();
        }
    }

    public void Initialized()
    {
        bgAudio = setting.bgAudio;
        dealAudio = setting.deal;
        hitAudio = setting.hitAudio;
        standAudio = setting.standAudio;
        winAudio = setting.winAudio;
        loseAudio = setting.loseAudio;
        betAudio = setting.betAudio;
        drawAudio = setting.drawAudio; 
        emptyBalanceAudio = setting.emptyBalanceAudio;
        gameTimeLine = FindObjectOfType<GameTimeLine>();
        if (gameTimeLine)
        {
            gameTimeLine.OnBet += OnBet;
            gameTimeLine.OnDeal += OnDeal;
            gameTimeLine.OnHit += OnHit;
            gameTimeLine.OnStend += OnStend;
            gameTimeLine.OnDraw += OnDraw;
            gameTimeLine.OnEmptyBalance += OnEmptyBalance;
            gameTimeLine.OnLose += OnLose;
            gameTimeLine.OnWin += OnWin;
        }
    }

    private void OnWin()
    {
        PlaySound(gameEventsTimeLine, winAudio);
    }

    private void OnLose()
    {
        PlaySound(gameEventsTimeLine, loseAudio);
    }

    private void OnEmptyBalance()
    {
        PlaySound(gameEventsTimeLine, emptyBalanceAudio);
    }

    private void OnDraw()
    {
        PlaySound(gameEventsTimeLine, drawAudio);
    }

    private void OnStend()
    {
        PlaySound(button, standAudio);
    }

    private void OnHit()
    {
        PlaySound(button2, hitAudio);
    }

    private void OnDeal()
    {
        PlaySound(button3, dealAudio);
    }

    private void OnBet()
    {
        PlaySound(button, betAudio);
    }

    public void PlaySound(AudioSource audio, AudioClip audioClip)
    {
        if (audioClip)
        {
            audio.clip = audioClip;
            audio.Play();
        }
    }
    public void StopSound(AudioSource audio, AudioClip audioClip)
    {
        if (audioClip)
        {
            audio.clip = audioClip;
            audio.Stop();
        }
    }
    private void OnDestroy()
    {
        if (gameTimeLine)
        {
            gameTimeLine.OnBet -= OnBet;
            gameTimeLine.OnDeal -= OnDeal;
            gameTimeLine.OnHit -= OnHit;
            gameTimeLine.OnStend -= OnStend;
            gameTimeLine.OnDraw -= OnDraw;
            gameTimeLine.OnEmptyBalance -= OnEmptyBalance;
            gameTimeLine.OnLose -= OnLose;
            gameTimeLine.OnWin -= OnWin;
        }
    }
}
