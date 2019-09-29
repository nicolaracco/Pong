public struct MatchStateTransition
{
    public int leftScore;
    public int rightScore;
    public bool isMatchPoint;
    public MatchState oldState;
    public MatchState newState;
    public PlayerID? lastGoalPlayerID;

    public MatchStateTransition(
        MatchState oldState, MatchState newState, int leftScore, int rightScore, bool isMatchPoint, PlayerID? lastGoalPlayerID
    ) {
        this.oldState = oldState;
        this.newState = newState;
        this.leftScore = leftScore;
        this.rightScore = rightScore;
        this.isMatchPoint = isMatchPoint;
        this.lastGoalPlayerID = lastGoalPlayerID;
    }
}