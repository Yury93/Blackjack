using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SettingGame")]
public class TimeLineSetting : ScriptableObject
{
    [Header("Баланс игрока")]
    public int playerBalance;
    [Header("Баланс дилера")]
    public int dealerBalane;
    [HideInInspector]
    public  int totalBet;
    [Header("Ставка игрока")]
    public int playerBet;
    [Header("Максимально возможная ставка")]
    public int maxBet;
    [Header("Текст после раунда будет исчезать по истечении этого времени")]
    public float timeActive_mainText;

    [Header("Заголовок окна с правилами"), TextArea(5, 5)]
    public string title;
    [Header("Текст с правилами"), TextArea(40, 50)]
    public string textRules;

    [Header("Фоновая музыка"),Header("ЗВУКИ И МУЗЫКА")]
    public AudioClip bgAudio;
    [Header("Звук кнопки DEAL")]
    public AudioClip deal;
    [Header("Звук кнопки HIT")]
    public AudioClip hitAudio;
    [Header("Звук кнопки STAND")]
    public AudioClip standAudio;
    [Header("Звук при выигрыше")]
    public AudioClip winAudio;
    [Header("Звук при проигрыше")]
    public AudioClip loseAudio;
    [Header("Звук когда происходит ставка")] 
    public AudioClip betAudio;
    [Header("Звук когда произошла ничья")]
    public AudioClip drawAudio;
    [Header("Звук при пустом балансе")] 
    public AudioClip emptyBalanceAudio;

   
}
