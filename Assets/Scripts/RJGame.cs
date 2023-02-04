// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

static class RJGame
{
    public static int growthPoints = 0;

    //enverda esto lo mismo no hace falta
    // static int evolutionLevel = 0;

    static int planetLevel = 0;

    static int level0BreakPoint = 0;
    static int level1BreakPoint = 20;
    static int level2BreakPoint = 40;
    public static int level3BreakPoint = 60;

    //enverda esto lo mismo no hace falta
    //public static int playerResources = 0;

    public static int resourceType = 0;

    public static int resource1Points = 1;

    public static int resource1Size = 8;

    public static int currentGatherPoints = 0;

    public static void AddGrowthPoints(int points)
    {
        growthPoints += points;
        if (growthPoints < 0)
            growthPoints = 0;
    }

    static int CheckLevel(int points)
    {
        /*
        switch (points)
        {
            // esto puede petar porque es un apaño de estacoverflou
            // cambiar a ifelse y a chorrarla
            case int value when value == level0BreakPoint:
                planetLevel = 0;
                // evolutionLevel = 0;
                break;

            case int value when value <= level1BreakPoint:
                planetLevel = 1;
                // evolutionLevel = 1;
                break;

            case int value when value <= level2BreakPoint:
                planetLevel = 2;
                // evolutionLevel = 2;
                break;

            case int value when value <= level3BreakPoint:
                planetLevel = 3;
                // evolutionLevel = 3;
                break;
        }
        */

        if (points < level1BreakPoint)
            planetLevel = 0;
        else if(points < level2BreakPoint)
            planetLevel = 1;
        else if(points < level3BreakPoint)
            planetLevel = 2;
        else if(points >= level3BreakPoint)
            planetLevel = 3;

        return planetLevel;
    }

    public static int CheckCurrentLevel()
    {
        return CheckLevel(growthPoints);

    }

    //por si hiciera falta identificar diferentes tipos de recursos
    /* 
    public static void CheckResource()
    {
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
    */


}
