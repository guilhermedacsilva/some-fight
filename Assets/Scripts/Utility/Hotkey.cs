using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Hotkey
{
    public static string ConvertIndexToHotkey(int index)
    {
        switch (index)
        {
            case 1: return "Q";
            case 2: return "W";
            case 3: return "E";
            case 4: return "R";
        }
        return "";
    }

    public static int ConvertKeyToIndex(string key)
    {
        switch (key)
        {
            case "Q": return 1;
            case "W": return 2;
            case "E": return 3;
            case "R": return 4;
        }
        return -1;
    }
}