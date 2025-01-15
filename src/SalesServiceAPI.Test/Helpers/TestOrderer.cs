using Xunit.Abstractions;
using Xunit.Sdk;

namespace SalesServiceAPI.Tests.Helpers;

public class TestOrderer : ITestCaseOrderer
{
    public const string TypeName = "SalesServiceAPI.Tests.Helpers.TestOrderer";
    public const string AssemblyName = "SalesServiceAPI.Tests";

    public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases)
        where TTestCase : ITestCase
    {
        var sortedMethods = new SortedDictionary<int, List<TTestCase>>();

        foreach (var testCase in testCases)
        {
            var order = int.MaxValue;
            var attr = testCase.TestMethod.Method
                .GetCustomAttributes((typeof(TestOrderAttribute).AssemblyQualifiedName))
                .FirstOrDefault();

            if (attr != null)
            {
                order = attr.GetNamedArgument<int>("Order");
            }

            GetOrCreate(sortedMethods, order).Add(testCase);
        }

        foreach (var list in sortedMethods.Values)
        {
            foreach (var testCase in list)
            {
                yield return testCase;
            }
        }
    }

    private static TValue GetOrCreate<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key)
        where TKey : struct
        where TValue : new()
    {
        if (!dictionary.TryGetValue(key, out var result))
        {
            result = new TValue();
            dictionary[key] = result;
        }

        return result;
    }
}

