namespace GoogleAnalyticsTracker
{
    public class CustomVariable
    {
        public string Name { get; private set; }
        public string Value { get; private set; }

        public CustomVariable(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
