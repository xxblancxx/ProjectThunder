using UnityEngine;
using System.Collections;

public class TextWrappingHandler : MonoBehaviour
{
    // Takes a string and splits to next line on "NEWLINE"
    // Because string dialog input in inspector can't take \n
    public static string SplitByNewline(string textString)
    {
        textString = textString.Replace("NEWLINE", "\n");
        return textString;
    }
}
