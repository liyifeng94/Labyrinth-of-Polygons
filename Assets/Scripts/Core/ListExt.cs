using System.Collections.Generic;

public static class ListExt
{
    // Using Fisher–Yates shuffle
    public static void Shuffle<T>(List<T> list)
    {
        int last = list.Count - 1;
        for (int i = 0; i < last; ++i)
        {
            int j = UnityEngine.Random.Range(i, list.Count);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}
