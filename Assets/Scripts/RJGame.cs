// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

static class RJGame
{
    static int growthPoints = 0;

    static int evolutionLevel = 0;

    static int planetLevel = 0;

    static int level0BreakPoint = 0;
    static int level1BreakPoint = 10;
    static int level2BreakPoint = 20;
    static int level3BreakPoint = 30;

    public static void AddGrowthPoints(int points)
    {
        growthPoints += points;
    }

    static int CheckLevel(int points)
    {
        switch (points)
        {
            // esto puede petar porque es un apaño de estacoverflou
            // cambiar a ifelse y a chorrarla
            case int value when value == level0BreakPoint:
                planetLevel = 0;
                break;

            case int value when value <= level1BreakPoint:
                planetLevel = 1;
                break;

            case int value when value <= level2BreakPoint:
                planetLevel = 2;
                break;

            case int value when value <= level3BreakPoint:
                planetLevel = 3;
                break;
        }
        return planetLevel;
    }

    public static int CheckCurrentLevel()
    {
        return CheckLevel(growthPoints);

    }




}
