namespace task01;

public static class StringExtensions
{
    public static bool IsPalindrome(this string input)
    {
        if (input.Length > 0)
        {
            string new_input = input.ToLower();
            string input1 = "";
            string input2 = "";
            foreach (var ch in new_input)
            {
                if (!char.IsPunctuation(ch) && !char.IsWhiteSpace(ch))
                {
                    input1 += ch;
                }
            }
            for (int i = input1.Length - 1; i >= 0; i--)
            {
                input2 += input1[i];
            }
            if (input1 == input2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
