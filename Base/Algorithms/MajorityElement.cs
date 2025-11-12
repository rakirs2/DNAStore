using System.Runtime.InteropServices;

namespace Base.Algorithms;

public class MajorityElement<T> 
{
    /// <summary>
    /// Returns the majority element of an array if it exists.
    /// Otherwise returns null. It is upon the caller to post process the default value
    ///
    /// Kind of a failed experiment with value types.
    /// // TODO: as an exercise consider how to do this with reference types.
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public static T BoyerMoore(List<T> values)
    {
        if (values.Count == 1)
        {
            return values[0];
        }

        var counter = 0;
        var element = default(T);
        foreach (var value in values)
        {
            if (counter == 0)
            {
                element = value;
                counter = 1;
            }
            
            counter += (element.Equals(value)) ? 1 : -1;
        }

        // ok, what if we have size 1
        if (counter > 1)
        {
            return element;
        }

        return default(T);
    }
    
    /// <summary>
    /// Returns the majority element of an array if it exists.
    /// Otherwise returns null. It is upon the caller to post process the default value
    ///
    /// Kind of a failed experiment with value types.
    /// // TODO: as an exercise consider how to do this with reference types.
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public static T? SimpleDictionary(List<T> values)
    {
        var tracker = new Dictionary<T, int>();
        var currentMax = 0;
        var currentValue = default(T);
        
        foreach (var value in values)
        {
            tracker.TryAdd(value, 0);
            tracker[value]++;
            if (tracker[value] > currentMax)
            {
                currentMax = tracker[value];
                currentValue = value;
            }
        }

        return currentMax > values.Count / 2 ? currentValue : default;
    }
    
}