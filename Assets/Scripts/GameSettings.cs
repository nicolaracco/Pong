public static class GameSettings
{
    public static int? PointsToWin = 11;
    public static PlayerType LeftPlayerType = PlayerType.AI;
    public static PlayerType RightPlayerType = PlayerType.AI;
    public static float discMovementSpeed = 15f;
    public static bool increaseSpeedOnBounce = true;

    public static PlayerType GetPlayerTypeForPlayerID(PlayerID playerId)
    {
        return playerId == PlayerID.Left ? LeftPlayerType : RightPlayerType;
    }
}
