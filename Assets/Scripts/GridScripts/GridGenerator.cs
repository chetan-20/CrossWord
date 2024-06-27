
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GridGenerator : MonoBehaviour
{
    [SerializeField] private PuzzleData gameData;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;
    private GameObject[,] gridCells;
    private FillCells fillCells;

    private void Start()
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
    
}
