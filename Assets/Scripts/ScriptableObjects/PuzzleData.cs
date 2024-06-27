
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class PuzzleData : ScriptableObject
{
    public float timeInSeconds;
    public int gridSizeX;
    public int gridSizeY;
    public string[] words;
   
}
