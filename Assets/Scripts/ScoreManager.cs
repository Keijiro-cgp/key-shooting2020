using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int got_golds_count = 0;
    public static int use_muteki_count = 0;

    private void Start()
    {
        got_golds_count = 0;
        use_muteki_count = 0;
    }

    public static void AddCountGold()
    {
        got_golds_count++;
    }

    public static void AddCountMuteki()
    {
        use_muteki_count++;
    }
}
