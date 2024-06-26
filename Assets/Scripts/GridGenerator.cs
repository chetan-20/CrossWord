using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GridGenerator : MonoBehaviour
{
    [SerializeField] private PuzzleData gameData;
    [SerializeField] private GameObject cellPrefab;
    private GameObject[,] gridCells;

    private void Start()
    {
        GenerateGrid();
        PlaceWordsInGrid();
    }
    private void GenerateGrid()
    {
        gridCells = new GameObject[gameData.gridSizeX, gameData.gridSizeY];

        for (int x = 0; x < gameData.gridSizeX; x++)
        {
            for (int y = 0; y < gameData.gridSizeY; y++)
            {
                GameObject cell = Instantiate(cellPrefab, new Vector2(x, y), Quaternion.identity);
                cell.transform.parent = transform; // Set the grid as parent
                gridCells[x, y] = cell;
            }
        }
    }
    private void PlaceWordsInGrid()
    {
        foreach (string word in gameData.words)
        {           
            int startX = Random.Range(0, gameData.gridSizeX);
            int startY = Random.Range(0, gameData.gridSizeY);
           
            for (int i = 0; i < word.Length; i++)
            {
                if (startX + i < gameData.gridSizeX)
                {
                    TextMeshPro textMesh = gridCells[startX + i, startY].GetComponentInChildren<TextMeshPro>();
                    if (textMesh != null)
                    {
                        textMesh.text = word[i].ToString();
                    }
                }
            }
        }
    }
}
