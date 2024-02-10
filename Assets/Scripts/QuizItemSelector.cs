using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class QuizItemSelector : IInitializable, ILateDisposable
{
    private QuizConfigs _quizConfigs;
    private EventBus _eventBus;

    private List<int> _usedQuizItemIndex;
    private int _lastItemIndex;

    [Inject]
    public void Construct(QuizConfigs quizConfigs, EventBus eventBus)
    {
        _quizConfigs = quizConfigs;
        _eventBus = eventBus;
    }

    public void Initialize()
    {
        _eventBus.OnPlayerAnswerCorrect += SelectNewItem;
        _eventBus.OnPlayerAnswerIncorrect += SelectNewItem;

        _usedQuizItemIndex = new List<int>();

        SelectNewItem();
    }

    public void LateDispose()
    {
        _eventBus.OnPlayerAnswerCorrect -= SelectNewItem;
        _eventBus.OnPlayerAnswerIncorrect -= SelectNewItem;
    }

    public void SelectNewItem()
    {
        do
        {
            _lastItemIndex = Random.Range(0, _quizConfigs.QuizItems.Length);
        }
        while (_usedQuizItemIndex.Contains(_lastItemIndex));

        _usedQuizItemIndex.Add(_lastItemIndex);

        _eventBus.OnNewQuizItemSelected?.Invoke(_quizConfigs.QuizItems[_lastItemIndex]);

        if (_usedQuizItemIndex.Count == _quizConfigs.QuizItems.Length)
        {
            _usedQuizItemIndex.Clear();
        }
    }
}
