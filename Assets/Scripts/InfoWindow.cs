using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoWindow : MonoBehaviour
{
    [SerializeField] private TimeLineSetting timeLineSetting;
    [SerializeField] private Button butReadInfo;
    [SerializeField] private Transform infoRulesGame;
    [SerializeField] private Text title;
    [SerializeField] private Text rules;
    [SerializeField] private AudioSource btnScoreAudio;
    private bool infoRead;

    private void Awake()
    {
        title.text = timeLineSetting.title;
        rules.text = timeLineSetting.textRules;
        butReadInfo.onClick.AddListener(() => WindowInfoClick());
    }
    public void WindowInfoClick()
    {
        if (infoRead)
        {
            Close();
        }
        else
        {
            infoRulesGame.gameObject.SetActive(true);
   
            infoRead = true;
        }
        btnScoreAudio.Play();
    }
    public void Close()
    {
        infoRulesGame.gameObject.SetActive(false);
        infoRead = false;
    }

}
