
public static class GameplayController
{
    public static int hightScorePlayer1;
    public static int hightScorePlayer2;
    public static int nbPlayers = 2;

    public static void SaveScore(int score, int player)
    {
        if (player == 0)
        {
            if (score > hightScorePlayer1)
            {
                hightScorePlayer1 = score;
            }
        }
        else
        {
            if (score > hightScorePlayer2)
            {
                hightScorePlayer2 = score;
            }
        }
    }

}
