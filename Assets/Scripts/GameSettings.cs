public class GameSettings
{
    public static GameSettings Current { get; private set; }

    public static GameSettings Set(int pointsToWin, PlayerType LeftPlayerType, PlayerType RightPlayerType, bool classicMode, bool audioEnabled)
    {
        GameSettings.Current = new GameSettings(pointsToWin, LeftPlayerType, RightPlayerType, classicMode, audioEnabled);
        return GameSettings.Current;
    }

    public readonly int pointsToWin;
    public readonly PlayerType LeftPlayerType;
    public readonly PlayerType RightPlayerType;
    public readonly bool classicMode;
    public readonly bool audioEnabled;

    GameSettings(int pointsToWin, PlayerType LeftPlayerType, PlayerType RightPlayerType, bool classicMode, bool audioEnabled)
    {
        this.pointsToWin = pointsToWin;
        this.LeftPlayerType = LeftPlayerType;
        this.RightPlayerType = RightPlayerType;
        this.classicMode = classicMode;
        this.audioEnabled = audioEnabled;
    }

    public PlayerType GetPlayerTypeForPlayerID(PlayerID playerId)
    {
        return playerId == PlayerID.Left ? LeftPlayerType : RightPlayerType;
    }
}
