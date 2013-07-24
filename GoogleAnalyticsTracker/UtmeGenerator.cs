using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoogleAnalyticsTracker
{
    class UtmeGenerator
    {
        private readonly Tracker _tracker;

        private enum ValueType
        {
            Event = 5,
            CustomVariableName = 8,
            CustomVariableValue = 9,
            CustomVariableScope = 11
        }

        public UtmeGenerator(Tracker tracker)
        {
            _tracker = tracker;
        }

        public string Generate()
        {
            return GenerateCustomVariables();
        }

        private string GenerateCustomVariables()
        {
            Func<CustomVariable, Func<CustomVariable, string>, string> getProperty =
                (cv, f) => cv == null ? null : f(cv);
            Func<Func<CustomVariable, string>, ValueType, string> getValues =
                (f, type) => SerializeValues(_tracker.CustomVariables.Select(f).ToArray(), type);

            var names = getValues(cv => getProperty(cv, cv1 => cv1.Name), ValueType.CustomVariableName);

            if (string.IsNullOrEmpty(names)) return string.Empty;

            var values = getValues(cv => getProperty(cv, cv1 => cv1.Value), ValueType.CustomVariableValue);
            var scopes = getValues(cv => getProperty(cv, cv1 => cv1.Scope.ToString()), ValueType.CustomVariableScope);

            return names + values + scopes;
        }

        private static string SerializeValues(IList<string> values, ValueType type)
        {
            var builder = new StringBuilder();

            var hasNonNullValues = false;

            for (var i = 0; i < values.Count; i++)
            {
                if (values[i] == null) continue;

                if (!hasNonNullValues)
                    hasNonNullValues = true;
                else
                    builder.Append("*");

                if (i > 0 && values[i - 1] == null)
                    builder.AppendFormat("{0}!", i + 1);

                builder.Append(values[i]);
            }

            return hasNonNullValues ? string.Format("{0}({1})", (int)type, builder) : string.Empty;
        }
    }
}
