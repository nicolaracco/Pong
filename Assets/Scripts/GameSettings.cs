public static class GameSettings
{
    public static int? PointsToWin = 11;
    public static PlayerType LeftPlayerType = PlayerType.AI;
    public static PlayerType RightPlayerType = PlayerType.Human;

    public static PlayerType GetPlayerTypeForPlayerID(PlayerID playerId)
    {
        return playerId == PlayerID.Left ? LeftPlayerType : RightPlayerType;
    }
}
