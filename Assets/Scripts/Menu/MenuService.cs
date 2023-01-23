using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuService : MonoBehaviour
{
    [SerializeField] private string sceneName,urlCoolCats;
    [SerializeField] private Button buttonStartGame,buttonScoreInfo;
    [SerializeField] private RectTransform targetButtonStart, targetButtonBack,targetButtonScore;
    [SerializeField] private UrlSelector urlSelector;
    [SerializeField] private AudioSource btnStartAudio;
    public Action OnSceneLoad;
    private void Awake()
    {
        buttonStartGame.onClick.AddListener(StartGame);
        
        urlSelector.OnActiveUrlSelector += RefreshStateButtonMenu;
    }

    private void Start()
    {
        buttonStartGame.transform.DOMove(targetButtonStart.transform.position, 1f);
        buttonScoreInfo.transform.DOMove(targetButtonScore.transform.position, 1f);
    }
    private void RefreshStateButtonMenu(bool disActive)
    {
        if (disActive) 
            buttonStartGame.interactable = false;
        else
            buttonStartGame.interactable = true;
    }
    private void StartGame()
    {
        btnStartAudio.Play();
        StartCoroutine(CorStartGame());
    }
    private IEnumerator CorStartGame()
    {
        yield return new WaitForSeconds(0.1f);
        urlSelector.OnActiveUrlSelector -= RefreshStateButtonMenu;
        OnSceneLoad?.Invoke();
        SceneManager.LoadSceneAsync(sceneName);
     
    }
}
