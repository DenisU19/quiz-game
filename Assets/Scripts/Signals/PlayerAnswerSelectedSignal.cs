public struct PlayerAnswerSelectedSignal 
{
    public string PlayerAnswer { get; private set; }

    public PlayerAnswerSelectedSignal(string playerAnswer)
    {
        PlayerAnswer = playerAnswer;
    }
}
