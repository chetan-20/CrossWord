
using UnityEngine;
/*
 * Scriptable Object that store the puzze data ie their words and hints and the grid size we want
 * for new level just make new SO of this and add a button from the Editor in level selector
 * Then add the new SO to level service
 */
[System.Serializable]
[CreateAssetMenu]
public class PuzzleData : ScriptableObject
{
    public int gridSizeX;
    public int gridSizeY;
    public string[] words;
    public string[] hints;
}
