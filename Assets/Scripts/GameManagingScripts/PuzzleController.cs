using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzzleController
{
    private int maxWordLength;
    private string[] hints;
    private bool wordSelected = false;
    private List<MouseInputHandler> selectedCells = new List<MouseInputHandler>();
    private List<string> selectedLetters = new List<string>();
    private List<string> correctWords = new List<string>();         
    private Dictionary<string, string> wordHintDictionary = new Dictionary<string, string>();
    
    public PuzzleController()
    {
        correctWords.AddRange(GameService.Instance.GridGenerator.gameData.words);       
        maxWordLength = BiggestWordLength();
        hints = GameService.Instance.GridGenerator.gameData.hints;
        for (int i = 0; i < correctWords.Count; i++)
        {
            if (i < hints.Length)
            {
                wordHintDictionary[correctWords[i]] = hints[i];
            }
        }        
    }  
    public void HandleCellClick(MouseInputHandler cell)
    {
        if (selectedCells.Count > 0 && wordSelected)
        {
            ResetSelection(); 
        }
        if (selectedCells.Contains(cell))
        {
            DeselectCell(cell);
        }
        else
        {
            if (CanSelectCell(cell))
            {
                SelectCell(cell);
            }
            else
            {
                EndSelection();
                SelectCell(cell);
            }
        }
    }
    private void SelectCell(MouseInputHandler cell)
    {
        if (selectedLetters.Count >= maxWordLength)
        {
            EndSelection();
            return;
        }
        selectedCells.Add(cell);
        selectedLetters.Add(cell.GetText());
        cell.SetSelected(true);
        // Check if a word is formed after selecting this cell
        string currentWord = string.Join("", selectedLetters);
        if (currentWord.Length <= maxWordLength)
        {
            if (IsCorrectWord(currentWord))
            {
                MarkCorrectWord(currentWord);
                wordSelected = true;
                CheckEmptyCorrectWords();//Level Over Condition
            }
        }
    }
    private void ResetSelection()
    {
        wordSelected = false;
        selectedLetters.Clear();
        foreach (var cell in selectedCells)
        {
            cell.SetSelected(false);
        }
        selectedCells.Clear();
    }
    private void DeselectCell(MouseInputHandler cell)
    {
        selectedCells.Remove(cell);
        selectedLetters.Remove(cell.GetText());
        cell.SetSelected(false);
    }
    private bool CanSelectCell(MouseInputHandler cell)
    {
        // Only allow selection if this cell is adjacent to the last selected cell
        if (selectedCells.Count > 0)
        {
            MouseInputHandler lastCell = selectedCells[selectedCells.Count - 1];
            Vector2Int currentPos = GetCellPosition(cell.gameObject);
            Vector2Int lastPos = GetCellPosition(lastCell.gameObject);
            return IsAdjacent(currentPos, lastPos);
        }
        return true; // Always allow first selection
    }
    private bool IsAdjacent(Vector2Int pos1, Vector2Int pos2)
    {
        return Mathf.Abs(pos1.x - pos2.x) <= 1 && Mathf.Abs(pos1.y - pos2.y) <= 1;
    }
    private void EndSelection()
    {       
        if (wordSelected)
        {
            return;
        }
        selectedLetters.Clear();
        foreach (var cell in selectedCells)
        {
            cell.SetSelected(false);
        }
        selectedCells.Clear();
    }
    private bool IsCorrectWord(string word)
    {
        return correctWords.Contains(word);
    }
    private void MarkCorrectWord(string word)
    {
        foreach (MouseInputHandler cell in selectedCells)
        {
            cell.SetCorrect();
        }
        correctWords.Remove(word);
        wordHintDictionary.Remove(word);
    }
    private Vector2Int GetCellPosition(GameObject cellGO)
    {
        GameObject[,] gridCells = GameService.Instance.GridGenerator.gridCells;
        for (int x = 0; x < gridCells.GetLength(0); x++)
        {
            for (int y = 0; y < gridCells.GetLength(1); y++)
            {
                if (gridCells[x, y] == cellGO)
                {
                    return new Vector2Int(x, y);
                }
            }
        }
        return Vector2Int.zero;
    }
    private int BiggestWordLength()
    {
        int maxLength = 0;
        foreach (string word in GameService.Instance.GridGenerator.gameData.words)
        {
            int wordLength = word.Length;
            if (wordLength > maxLength)
            {
                maxLength = wordLength;
            }
        }
        return maxLength;
    }
    public void CheckEmptyCorrectWords()
    {
        if (correctWords.Count == 0)
        {
            GameService.Instance.UIService.OnLevelOver();
        }
    }  
    public string GetRandomHint()
    {
        if (wordHintDictionary.Count == 0)
        {
            return string.Empty;
        }      
        int randomIndex = UnityEngine.Random.Range(0, correctWords.Count);
        string randomWord = correctWords[randomIndex];       
        return wordHintDictionary[randomWord];
    }     
}
