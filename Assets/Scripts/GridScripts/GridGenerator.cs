
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GridGenerator : MonoBehaviour
{
    public PuzzleData gameData ;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;
    public GameObject[,] gridCells;
    private FillCells fillCells;
  
    public void GenerateLevel()
    {       
        GenerateGrid();
        SetGridLayout();
        fillCells = new FillCells(gameData, gridCells);
        fillCells.FillGrid();
    }
    private void GenerateGrid()
    {
        gridCells = new GameObject[gameData.gridSizeX, gameData.gridSizeY];

        for (int x = 0; x < gameData.gridSizeX; x++)
        {
            for (int y = 0; y < gameData.gridSizeY; y++)
            {
                GameObject cell = Instantiate(cellPrefab, new Vector2(x, y), Quaternion.identity);
                cell.transform.SetParent(transform, false); // Set the grid as parent
                gridCells[x, y] = cell;
               TextMeshProUGUI t = cell.GetComponentInChildren<TextMeshProUGUI>();
                //t.text = ""+x+""+y;
            }
        }
    }
    private void SetGridLayout()
    {
        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayoutGroup.constraintCount = gameData.gridSizeY;
        
    }
    public void ResetGrid()
    {
        fillCells.ResetGrid();
    }
    public void RemoveCells()
    {
        if (gridCells != null)
        {            
            for (int x = 0; x < gameData.gridSizeX; x++)
            {
                for (int y = 0; y < gameData.gridSizeY; y++)
                {
                    GameObject cell = gridCells[x, y];                    
                    if (cell != null)
                    {                      
                        Destroy(cell);
                    }
                }
            }           
            gridCells = null;
        }
    }
    public int BiggestWordLength()
    {
        int maxLength = 0;

        foreach (string word in gameData.words)
        {
            int wordLength = word.Length;
            if (wordLength > maxLength)
            {
                maxLength = wordLength;
            }
        }
        return maxLength;
    }
 }
