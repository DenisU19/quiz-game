public struct QuizItemSelectedSignal
{
    public QuizItem NewQuizItem { get; private set; }

    public QuizItemSelectedSignal(QuizItem newQuizItem)
    {
        NewQuizItem = newQuizItem;
    }
}
