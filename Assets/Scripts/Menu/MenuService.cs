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
    [SerializeField] private Button buttonStartGame;
    [SerializeField] private RectTransform targetButtonStart, targetButtonBack;
    [SerializeField] private UrlSelector urlSelector;
   
    private void Awake()
    {
        buttonStartGame.onClick.AddListener(StartGame);
        
        urlSelector.OnActiveUrlSelector += RefreshStateButtonMenu;
    }

    private void Start()
    {
        buttonStartGame.transform.DOMove(targetButtonStart.transform.position, 1f);
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
        StartCoroutine(CorStartGame());
    }
    private IEnumerator CorStartGame()
    {
        yield return new WaitForSeconds(0.01f);
        urlSelector.OnActiveUrlSelector -= RefreshStateButtonMenu;
        SceneManager.LoadSceneAsync(sceneName);
    }
   
}
