using UnityEngine;

[CreateAssetMenu(menuName = "GameConfigs/QuizConfigs", fileName = "QuizConfigs")]
public class QuizConfigs : ScriptableObject
{
    [SerializeField] QuizItem[] _quizItems;

    public QuizItem[] QuizItems => _quizItems;
}
