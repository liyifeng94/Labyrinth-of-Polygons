using System;

[Serializable]
public class LevelState
{
    public int Score = 0;
    public int Health = 0;
    public int Gold = 0;
    public int Level = 0;
    public uint FinalScore = 0;
    public uint NormalKilled = 0;
    public uint AttackKilled = 0;
    public uint FlyingKilled = 0;
    public uint FastKilled = 0;
    public uint BossKilled = 0;
    public GameOptions LastGameOptions;

    //TODO: Gems
    //public Gems

    //TODO: save grid state

    public uint CalculateFinalScore(GameOptions gameOptions)
    {
        LastGameOptions = gameOptions;
        FinalScore = (uint) Score;
        FinalScore *= (uint)Level;
        // FinalScore *= gameOptions.Width + gameOptions.Height + gameOptions.Entrances;
        FinalScore += gameOptions.Obstacles; // (uint) Score * ((100 - gameOptions.Obstacles) / 2);

        FinalScore += NormalKilled * 100;
        FinalScore += AttackKilled * 200;
        FinalScore += FastKilled * 300;
        FinalScore += FlyingKilled * 300;
        FinalScore += BossKilled * 1000;
        return FinalScore;
    }
}
