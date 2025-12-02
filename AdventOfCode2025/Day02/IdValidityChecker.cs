namespace AdventOfCode2025.Day02;

public static class IdValidityChecker
{
    public static bool SimpleIsValid(long id)
    {
        var idStr = id.ToString();
        if (idStr.Length % 2 != 0)
        {
            return true;
        }

        var firstHalf = idStr[..(idStr.Length / 2)];
        var secondHalf = idStr[(idStr.Length / 2)..];

        return firstHalf != secondHalf;
    }

    public static bool ComplexIsValid(long id)
    {
        var idStr = id.ToString();
        for (var splitPos = 1; splitPos <= idStr.Length / 2; splitPos++)
        {
            if (idStr.Length % splitPos != 0)
            {
                continue;
            }

            var segment = idStr[..splitPos];
            var repeatCount = idStr.Length / splitPos;

            if (string.Concat(Enumerable.Repeat(segment, repeatCount)) == idStr)
            {
                return false;
            }
        }

        return true;
    }
}