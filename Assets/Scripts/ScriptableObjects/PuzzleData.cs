
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class PuzzleData : ScriptableObject
{
    public int gridSizeX;
    public int gridSizeY;
    public string[] words;   
}
