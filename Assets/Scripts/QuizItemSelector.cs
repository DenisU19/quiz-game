using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class QuizItemSelector : IInitializable, ILateDisposable
{
    private QuizConfigs _quizConfigs;
    private SignalBus _signalBus;
    private List<int> _usedQuizItemIndex;
    private int _lastItemIndex;

    [Inject]
    public void Construct(SignalBus signalBus, QuizConfigs quizConfigs)
    {
        _signalBus = signalBus;
        _quizConfigs = quizConfigs;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<SelectNewQuizItemSignal>(OnSelectNewItem);

        _usedQuizItemIndex = new List<int>();

        OnSelectNewItem();
    }

    public void OnSelectNewItem()
    {
        do
        {
            _lastItemIndex = Random.Range(0, _quizConfigs.QuizItems.Length);
        }
        while (_usedQuizItemIndex.Contains(_lastItemIndex));

        _usedQuizItemIndex.Add(_lastItemIndex);

        _signalBus.Fire(new QuizItemSelectedSignal(_quizConfigs.QuizItems[_lastItemIndex]));

        if (_usedQuizItemIndex.Count == _quizConfigs.QuizItems.Length)
        {
            _usedQuizItemIndex.Clear();
        }
    }

    public void LateDispose()
    {
        _signalBus.Unsubscribe<SelectNewQuizItemSignal>(OnSelectNewItem);
    }
}
