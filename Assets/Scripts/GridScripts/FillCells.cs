
using TMPro;
using UnityEditor;
using UnityEngine;

public class FillCells 
{
    private PuzzleData gameData;
    private GameObject[,] gridCells;
    public FillCells(PuzzleData gameData, GameObject[,] gridCells)
    {
        this.gameData = gameData;
        this.gridCells = gridCells;
    }
    public void FillGrid()
    {
        PlaceWordsInGrid();
        FillGridWithRandomLetters();
    }
    public void ResetGrid()
    {
        ClearGrid();
        FillGrid();
    }
    private void PlaceWordsInGrid()
    {
        foreach (string word in gameData.words)
        {
            bool wordPlaced = false;

            // Randomly decide whether to place horizontally or vertically
            bool placeHorizontally = Random.Range(0,2) == 0;

            if (placeHorizontally)
            {
                wordPlaced = TryPlaceWordHorizontally(word);
                if (!wordPlaced)
                {
                    wordPlaced = TryPlaceWordVertically(word);
                }
            }
            else
            {
                wordPlaced = TryPlaceWordVertically(word);
                if (!wordPlaced)
                {
                    wordPlaced = TryPlaceWordHorizontally(word);
                }
            }

            if (!wordPlaced)
            {
                Debug.LogWarning($"Word '{word}' could not be placed in the grid.");
            }
        }
    }

    private bool TryPlaceWordHorizontally(string word)
    {
        for (int x = 0; x <= gameData.gridSizeX - word.Length; x++)
        {
            for (int y = 0; y < gameData.gridSizeY; y++)
            {
                if (CanPlaceWord(word, x, y, true))
                {
                    PlaceWord(word, x, y, true);
                    return true;
                }
            }
        }
        return false;
    }

    private bool TryPlaceWordVertically(string word)
    {
        for (int y = 0; y <= gameData.gridSizeY - word.Length; y++)
        {
            for (int x = 0; x < gameData.gridSizeX; x++)
            {
                if (CanPlaceWord(word, x, y, false))
                {
                    PlaceWord(word, x, y, false);
                    return true;
                }
            }
        }
        return false;
    }

    private bool CanPlaceWord(string word, int startX, int startY, bool horizontal)
    {
        if (horizontal)
        {
            if (startX + word.Length > gameData.gridSizeX)
            {
                return false;
            }

            for (int i = 0; i < word.Length; i++)
            {
                GameObject cell = gridCells[startX + i, startY];
                TextMeshProUGUI textMesh = cell.GetComponentInChildren<TextMeshProUGUI>();
                if (textMesh != null && !string.IsNullOrEmpty(textMesh.text) && textMesh.text != word[i].ToString())
                {
                    return false;
                }
            }
        }
        else // Vertical placement
        {
            if (startY + word.Length > gameData.gridSizeY)
            {
                return false;
            }

            for (int i = 0; i < word.Length; i++)
            {
                GameObject cell = gridCells[startX, startY + i];
                TextMeshProUGUI textMesh = cell.GetComponentInChildren<TextMeshProUGUI>();
                if (textMesh != null && !string.IsNullOrEmpty(textMesh.text) && textMesh.text != word[i].ToString())
                {
                    return false;
                }
            }
        }

        return true;
    }

    private void PlaceWord(string word, int startX, int startY, bool horizontal)
    {
        if (horizontal)
        {
            for (int i = 0; i < word.Length; i++)
            {
                GameObject cell = gridCells[startX + i, startY];
                TextMeshProUGUI textMesh = cell.GetComponentInChildren<TextMeshProUGUI>();
                if (textMesh != null)
                {
                    textMesh.text = word[i].ToString();
                }
            }
        }
        else // Vertical placement
        {
            for (int i = 0; i < word.Length; i++)
            {
                GameObject cell = gridCells[startX, startY + i];
                TextMeshProUGUI textMesh = cell.GetComponentInChildren<TextMeshProUGUI>();
                if (textMesh != null)
                {
                    textMesh.text = word[i].ToString();
                }
            }
        }
    }

    private void FillGridWithRandomLetters()
    {
        string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; // Letters to choose from
        for (int x = 0; x < gameData.gridSizeX; x++)
        {
            for (int y = 0; y < gameData.gridSizeY; y++)
            {
                GameObject cell = gridCells[x, y];
                TextMeshProUGUI textMesh = cell.GetComponentInChildren<TextMeshProUGUI>();
                if (textMesh != null && string.IsNullOrEmpty(textMesh.text))//if cell empty fill with random letter
                {
                    char randomLetter = letters[Random.Range(0,letters.Length)];
                    textMesh.text = randomLetter.ToString();
                }
            }
        }
    }
    private void ClearGrid()
    {
        for (int x = 0; x < gameData.gridSizeX; x++)
        {
            for (int y = 0; y < gameData.gridSizeY; y++)
            {
                GameObject cell = gridCells[x, y];
                TextMeshProUGUI textMesh = cell.GetComponentInChildren<TextMeshProUGUI>();
                if (textMesh != null)
                {
                    textMesh.text = ""; // Clear the text
                }
            }
        }
    }
   

}
