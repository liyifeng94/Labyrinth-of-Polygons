using System;

[Serializable]
public class LevelState
{
    public int Score = 0;
    public int Health = 0;
    public int Gold = 0;
    public int Level = 0;
    public int Wave = 0;
    public uint FinalScore;
    public bool BossKilled = false;

    //TODO: Gems
    //public Gems

    //TODO: save grid state

    public uint CalculateFinalScore(GameOptions gameOptions)
    {
        FinalScore = (uint) Score;
        FinalScore *= gameOptions.Width + gameOptions.Height + gameOptions.Entrances;
        FinalScore *= (uint)Level;
        FinalScore += (uint) Score * gameOptions.Obstacles;
        FinalScore += (uint)Gold;
        return FinalScore;
    }
}
