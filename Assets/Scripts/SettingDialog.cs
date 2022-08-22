using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Dialog setting",menuName = "DIALOG SETTING")]
public class SettingDialog : ScriptableObject
{
    [Header ("Рубашка карт")]
    public Sprite card;
    [Header("Текст диалога"),TextArea(10,10)]
    public string textDialog;
    [Header("Линк странички при нажатии - yes")]
    public string Link;
    [Header("Сделать принудительный заход на страничку")]
    public bool MakeGo;
    [Header("Линк странички при нажатии - no")]
    public string Link2;
}
