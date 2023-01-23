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
    [SerializeField] private string goTo;
    [SerializeField] private Button buttonYes,buttonNo, buttonBack;
    [SerializeField] private Transform startPos1,startPos2,startPos3,target1, target2, target3, targetButtonBack;
    [SerializeField] private List<Button> otherButtons;
    [SerializeField] private Button betButton;
    [SerializeField] private AudioSource btnYesAudio, btnNoAudio, btnBackAudio;
    public bool menu,activeSelf;
    private void Start()
    {
        //DontDestroyOnLoad(this.gameObject);
        buttonBack.onClick.AddListener(GoToUrlSelector);
        buttonYes.onClick.AddListener(GoToLink);
        buttonBack.transform.DOMove(targetButtonBack.transform.position, 1f);
  
    }
    public Action<bool> OnActiveUrlSelector;
    public void SetActiveDialogWindow(bool active)
    {
        activeSelf = active;
        var btnYes =  buttonYes.GetComponent<RectTransform>();
        var btnNo = buttonNo.GetComponent<RectTransform>();
        if (active)
        {
            dialogWindow.transform.DOMoveY(target1.transform.position.y, 1);
            buttonYes.transform.DOMoveY(target2.transform.position.y, 1.5f);
            buttonNo.transform.DOMoveY(target3.transform.position.y, 1.5f);
            buttonBack.interactable = false;
            otherButtons.ForEach(b => b.interactable = false);
            if (betButton != null)
            {
                if (GameTimeLine.instance.totalBet == 0 && GameTimeLine.instance.bet == false
                    && GameTimeLine.instance.betBtnAnimator.enabledButton == true)
                {
                    GameTimeLine.instance.betBtnAnimator.ButtonDisabled();
                }
              
                betButton.GetComponent<Image>().raycastTarget = false;
            }
        }
        else
        {
            dialogWindow.transform.DOMoveY(startPos1.transform.position.y, 1);
            buttonYes.transform.DOMoveY(startPos2.transform.position.y, 1.5f);
            buttonNo.transform.DOMoveY(startPos3.transform.position.y, 1.5f);
            btnNoAudio.Play();
            buttonBack.interactable = true;
            otherButtons.ForEach(b => b.interactable = true);
            if (betButton != null)
            {
                if (GameTimeLine.instance.totalBet == 0 && GameTimeLine.instance.bet == false
                    && GameTimeLine.instance.betBtnAnimator.enabledButton == false)
                {
                    GameTimeLine.instance.betBtnAnimator.ButtonEnable(); 
                }
          
                betButton.GetComponent<Image>().raycastTarget = true;
            }
        }
        OnActiveUrlSelector?.Invoke(active);
    }
    private void GoToUrlSelector()
    {
        SetActiveDialogWindow(true);
        btnBackAudio.Play();
    }
    public void GoToLink()
    {
        if (menu)
            Application.OpenURL(goTo);
        else
            SceneManager.LoadScene(goTo);

        btnYesAudio.Play();
    }
   
}
