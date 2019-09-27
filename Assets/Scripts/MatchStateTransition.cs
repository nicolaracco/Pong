public struct MatchStateTransition
{
    public int leftScore;
    public int rightScore;
    public MatchState oldState;
    public MatchState newState;
    public PlayerID? lastGoalPlayerID;

    public MatchStateTransition(
        MatchState oldState, MatchState newState, int leftScore, int rightScore, PlayerID? lastGoalPlayerID
    ) {
        this.oldState = oldState;
        this.newState = newState;
        this.leftScore = leftScore;
        this.rightScore = rightScore;
        this.lastGoalPlayerID = lastGoalPlayerID;
    }
}