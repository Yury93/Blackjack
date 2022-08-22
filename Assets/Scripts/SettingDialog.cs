using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Dialog setting",menuName = "DIALOG SETTING")]
public class SettingDialog : ScriptableObject
{
    [Header ("������� ����")]
    public Sprite card;
    [Header("����� �������"),TextArea(10,10)]
    public string textDialog;
    [Header("���� ��������� ��� ������� - yes")]
    public string Link;
    [Header("������� �������������� ����� �� ���������")]
    public bool MakeGo;
    [Header("���� ��������� ��� ������� - no")]
    public string Link2;
}
