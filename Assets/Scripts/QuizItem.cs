using UnityEngine;
using System;

[Serializable]
public class QuizItem 
{
    [SerializeField] private Sprite _questionImage;

    [SerializeField] private string _question;

    [SerializeField] private string _correctAnswer;

    [SerializeField] private string[] _allAnswers;

    public Sprite QuestionImage => _questionImage;
    public string Question => _question;
    public string CorrectAnswer => _correctAnswer;
    public  string[] AllAnswers => _allAnswers;
}
