using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogWindow : MonoBehaviour
{
    [SerializeField] private GameTimeLine timeLine;
    [SerializeField] private Transform window;
    [SerializeField] private Button yesBtn, noBtn;
    public string Link;
    public bool MakeGo;
    public string Link2;

    private void Awake()
    {
        timeLine.OnEmptyBalance += (() => window.gameObject.SetActive(true));
        yesBtn.onClick.AddListener(() => GoingByLink());
        noBtn.onClick.AddListener(() => CloseWindow());
    }
    private void GoingByLink()
    {
        Application.OpenURL(Link);
    }
    private void CloseWindow()
    {
        if (MakeGo)
            Application.OpenURL(Link2);

        window.gameObject.SetActive(false);
    }

}
