using UnityEngine;

[CreateAssetMenu(menuName = "Difficulty", fileName = "New Difficulty")]
public class DifficultyManager : ScriptableObject
{
    [SerializeField] private float MaxKills;
    [SerializeField] private float killFactor;
    [SerializeField] private float MaxTime;
    [SerializeField] private float timeFactor;
    [SerializeField] private float MaxDifficultyMultiplier;
    [SerializeField] private float MinTimeBetweenSpawns;
    [SerializeField] private float MaxTimeBetweenSpawns;
    [SerializeField] private FloatReference totalKills;
    [SerializeField] private FloatReference timePlayed;

    public float Difficulty 
    { 
        get 
        { 
            return (totalKills.Value * killFactor / MaxKills + timePlayed.Value * timeFactor / MaxTime) / 2; 
        }
    }

    public float EnemyMultiplier
    {
        get
        {
            return 1 + (MaxDifficultyMultiplier - 1) * Difficulty;
        }
    }

    public float SpawnRate
    {
        get
        {
            return MaxTimeBetweenSpawns - (MaxTimeBetweenSpawns - MinTimeBetweenSpawns) * Difficulty;
        }
    }
    private void OnEnable()
    {
        totalKills.Value = 0;
        timePlayed.Value = 0;
    }
}
