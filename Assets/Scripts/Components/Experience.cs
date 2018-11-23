using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Experience {

    [SerializeField] private int level;
    [SerializeField] private long totalExperience;

    public const float constA = 6.9f;
    public const float constB = -3.4f;

    public long ExperienceToNextLevel
    {
        get
        {
            return NextLevelExperience - ExperienceOfCurrentLevel; 
        }
    }

    public long NextLevelExperience
    {
        get
        {
            return (long)(((Mathf.Pow(Level + 1, constA) * Mathf.Pow(Level + 1, constB)) + 100) * 10);
        }
    }

    public long ProgressToNextLevel
    {
        get
        {
            return TotalExperience - ExperienceOfCurrentLevel;
        }
    }

    public long ExperienceOfCurrentLevel
    {
        get
        {
            return (long)(((Mathf.Pow(Level, constA) * Mathf.Pow(Level, constB)) + 100) * 10);
        }
    }

    public long TotalExperience
    {
        get
        {
            return totalExperience;
        }

        set
        {
            totalExperience = value;
        }
    }

    public int Level
    {
        get
        {
            level = (int)Mathf.Max(Mathf.Pow((TotalExperience / 10) - 100, 1 / (constA + constB)), 1);
            return level;
        }
        set
        {
            level = value;
        }
    }
    public void WriteExpTableToFile(int maxLevel = 150)
    {
        string path = "Assets/Resources/Text/ExperienceTable.txt";
        
        float constXP = -0.01f;
        int combatSec = 3;
        int nonCombatSec = 1;
        
        float combatMin = combatSec / 60f;
        float secPerMonster = combatSec + nonCombatSec;

        float totalMin = 0;
        StreamWriter writer = new StreamWriter(path, false);
        writer.WriteLine(string.Format("|{0}|{1}|{2}", "Level", "Total XP","XP to next lvl"));
        writer.WriteLine("Const1: " + constA.ToString());
        writer.WriteLine("Const2: " + constB.ToString());
        writer.WriteLine("___________________________________________________________________________________");
        for (int i = 1; i <= maxLevel; i++)
        {
            long exp = (long)(((Mathf.Pow(i, constA) * Mathf.Pow(i, constB)) + 100) * 10);
            float level = Mathf.Max(Mathf.Round(Mathf.Pow((exp / 10) - 100, 1 / (constA + constB))),1);
            long expNext = (long)(((Mathf.Pow(i+1, constA) * Mathf.Pow(i+1, constB)) + 100) * 10);
            
            float monsterXp = level * (10 * ((Mathf.Pow(level, constXP))) / (level/100+1));
            float levelUpInMinutes = ((expNext - exp) / monsterXp * secPerMonster) / 60f;

            totalMin += levelUpInMinutes;

            string line = "";
            if(i < maxLevel)
                line = string.Format("{0} \t \t {1:#,#} \t \t {2:#,#} \t \t {3} \t \t {4}", level.ToString(), exp, (expNext - exp), monsterXp.ToString("F2"), levelUpInMinutes.ToString("F2") + "Min");
            else
                line = string.Format("{0} \t \t {1:#,#}", i.ToString(), exp);
            writer.WriteLine(line);
        }
        writer.WriteLine("Totalhours: " + (totalMin/60).ToString("F2"));
        writer.Close();
    }
}
