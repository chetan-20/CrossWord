
using UnityEngine;
/*
 * Enables Disables LevelGridObject
 * Allows other script to create new puzzle controller when a level is reset or restarted or chosen from menu
 * Stores all the PuzzleData 
 */
public class LevelService : MonoBehaviour
{
    [SerializeField] private GameObject gridGenObject;
    public PuzzleData[] puzzleData;
    public void EnableGrid()=>gridGenObject.SetActive(true);
    public void DisableGrid()=>gridGenObject.SetActive(false);
    public void CreatePuzzleController()
    {
        GameService.Instance.puzzleController = new PuzzleController();
    }
}
