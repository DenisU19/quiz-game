using UnityEngine;
using System;


[CreateAssetMenu(menuName = "GameConfigs/EventBus", fileName = "EventBus")]
public class EventBus : ScriptableObject
{
    public Action<QuizItem> OnNewQuizItemSelected;

    public Action<string> OnPlayerSelectAnswer;

    public Action OnPlayerAnswerCorrect;

    public Action OnPlayerAnswerIncorrect;

    public Action<int> OnHealthCountReduced;

    public Action OnGameOver;

    public Action OnGameRestarted;
}
