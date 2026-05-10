using UnityEngine;

[CreateAssetMenu(fileName = "NewLevel", menuName = "Tower Defense/Level Data")]
public class LevelData : ScriptableObject
{
    public int levelNumber;
    public Wave[] waves;
}