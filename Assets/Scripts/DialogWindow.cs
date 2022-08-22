using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogWindow : MonoBehaviour
{
    [SerializeField] private GameTimeLine timeLine;
    [SerializeField] private Transform window;
    [SerializeField] private Text textDialog;
    [SerializeField] private Button yesBtn, noBtn;
    [SerializeField] private SettingDialog settingDialog;
    public string Link { get; private set; }
    public bool MakeGo { get; private set; }
    public string Link2 { get; private set; }

    private void Awake()
    {
        textDialog.text = settingDialog.textDialog;
        Link = settingDialog.Link;
        MakeGo = settingDialog.MakeGo;
        Link2 = settingDialog.Link2;

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
