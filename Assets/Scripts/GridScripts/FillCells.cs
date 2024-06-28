
using System.Collections.Generic;
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
    // Fills the grid with shuffled words and random letters.      
    public void FillGrid()
    {        
        List<string> wordList = new List<string>(gameData.words);       
        List<string> shuffledWords = ShuffleWords(wordList);
        // Place words in the grid
        foreach (string word in shuffledWords)
        {
            bool wordPlaced = TryPlaceWord(word);

            if (!wordPlaced)
            {
                Debug.LogWarning($"Word '{word}' could not be placed in the grid.");
            }
        }       
        FillGridWithRandomLetters();
    }  
    // Tries to place a word in the grid, attempting both horizontal and vertical placements.      
    private bool TryPlaceWord(string word)
    {
        // Randomly decide whether to place horizontally or vertically
        bool placeHorizontally = UnityEngine.Random.Range(0, 2) == 0;

        if (placeHorizontally)
        {
            return TryPlaceWordHorizontally(word) || TryPlaceWordVertically(word);
        }
        else
        {
            return TryPlaceWordVertically(word) || TryPlaceWordHorizontally(word);
        }
    }  
    // Tries to place a word horizontally in the grid.   
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
    // Tries to place a word vertically in the grid.  
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
    // Checks if a word can be placed starting from given coordinates and direction.   
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
    // Places a word in the grid starting from given coordinates and direction.   
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
                if (textMesh != null && string.IsNullOrEmpty(textMesh.text))
                {
                    char randomLetter = letters[UnityEngine.Random.Range(0, letters.Length)];
                    textMesh.text = randomLetter.ToString();
                }
            }
        }
    }  
    private List<string> ShuffleWords(List<string> words)
    {
        for (int i = 0; i < words.Count; i++)
        {
            string temp = words[i];
            int randomIndex = UnityEngine.Random.Range(i, words.Count);
            words[i] = words[randomIndex];
            words[randomIndex] = temp;
        }
        return words;
    }
}
