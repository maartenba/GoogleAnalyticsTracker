namespace GoogleAnalyticsTracker.Core
{
    public class CustomVariable
    {
        public string Name { get; private set; }
        public string Value { get; private set; }
        public int Scope { get; private set; }

        public CustomVariable(string name, string value, int scope)
        {
            Name = name;
            Value = value;
            Scope = scope;
        }
    }
}
