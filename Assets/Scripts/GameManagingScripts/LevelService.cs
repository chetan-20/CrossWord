using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelService : MonoBehaviour
{
    [SerializeField] private GameObject gridGenObject;
    public PuzzleData[] puzzleData;
    public void EnableGrid()=>gridGenObject.SetActive(true);
    public void DisableGrid()=>gridGenObject.SetActive(false);        
}
