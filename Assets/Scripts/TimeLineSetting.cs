using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SettingGame")]
public class TimeLineSetting : ScriptableObject
{
    [Header("������ ������")]
    public int playerBalance;
    [Header("������ ������")]
    public int dealerBalane;
    [HideInInspector]
    public  int totalBet;
    [Header("������ ������")]
    public int playerBet;
    [Header("����������� ��������� ������")]
    public int maxBet;
    [Header("����� ����� ������ ����� �������� �� ��������� ����� �������")]
    public float timeActive_mainText;

    [Header("��������� ���� � ���������"), TextArea(5, 5)]
    public string title;
    [Header("����� � ���������"), TextArea(40, 50)]
    public string textRules;

    [Header("������� ������"),Header("����� � ������")]
    public AudioClip bgAudio;
    [Header("���� ������ DEAL")]
    public AudioClip deal;
    [Header("���� ������ HIT")]
    public AudioClip hitAudio;
    [Header("���� ������ STAND")]
    public AudioClip standAudio;
    [Header("���� ��� ��������")]
    public AudioClip winAudio;
    [Header("���� ��� ���������")]
    public AudioClip loseAudio;
    [Header("���� ����� ���������� ������")] 
    public AudioClip betAudio;
    [Header("���� ����� ��������� �����")]
    public AudioClip drawAudio;
    [Header("���� ��� ������ �������")] 
    public AudioClip emptyBalanceAudio;

   
}
