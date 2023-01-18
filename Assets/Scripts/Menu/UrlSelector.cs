using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UrlSelector : MonoBehaviour
{
    [SerializeField] private RectTransform dialogWindow;
    [SerializeField] private string url;
    [SerializeField] private Button buttonYes,buttonNo, buttonBack;
    [SerializeField] private Transform startPos1,startPos2,startPos3,target1, target2, target3, targetButtonBack;
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        buttonBack.onClick.AddListener(GoToUrlSelector);
        buttonBack.transform.DOMove(targetButtonBack.transform.position, 2f);
  
    }
    public Action<bool> OnActiveUrlSelector;
    public void SetActiveDialogWindow(bool active)
    {
        var btnYes =  buttonYes.GetComponent<RectTransform>();
        var btnNo = buttonNo.GetComponent<RectTransform>();
        if (active)
        {
            dialogWindow.transform.DOMoveY(target1.transform.position.y, 1);
            buttonYes.transform.DOMoveY(target2.transform.position.y, 1.5f);
            buttonNo.transform.DOMoveY(target3.transform.position.y, 1.5f);
            buttonBack.interactable = false;
        }
        else
        {
            dialogWindow.transform.DOMoveY(startPos1.transform.position.y, 1);
            buttonYes.transform.DOMoveY(startPos2.transform.position.y, 1.5f);
            buttonNo.transform.DOMoveY(startPos3.transform.position.y, 1.5f);
            buttonBack.interactable = true;
        }
        OnActiveUrlSelector?.Invoke(active);
    }
    private void GoToUrlSelector()
    {
        SetActiveDialogWindow(true);

    }
}
