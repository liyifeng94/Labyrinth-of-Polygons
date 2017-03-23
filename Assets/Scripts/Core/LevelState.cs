using System;

[Serializable]
public class LevelState
{
    public int Score = 0;
    public int Health = 0;
    public int Gold = 0;
    public int Level = 0;
    public uint FinalScore = 0;
    public uint BossKilled = 0;
    public GameOptions LastGameOptions;

    //TODO: Gems
    //public Gems

    //TODO: save grid state

    public uint CalculateFinalScore(GameOptions gameOptions)
    {
        LastGameOptions = gameOptions;
        FinalScore = (uint) Score;
        FinalScore *= gameOptions.Width + gameOptions.Height + gameOptions.Entrances;
        FinalScore *= (uint)Level;
        FinalScore += (uint) Score * ((100 - gameOptions.Obstacles) / 2);
        FinalScore += (uint)Gold; // delete startup money
        return FinalScore;
    }
}
