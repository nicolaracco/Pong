namespace Pong.InputControllers
{
    public struct InputValue
    {
        public readonly InputValueType type;
        public readonly float value;

        public InputValue(float value, InputValueType type)
        {
            this.value = value;
            this.type = type;
        }
    }
}