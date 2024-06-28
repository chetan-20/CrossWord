using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController
{
    private List<MouseInputHandler> selectedCells = new List<MouseInputHandler>();
    private List<string> selectedLetters = new List<string>();
    private List<string> correctWords = new List<string>();
    private int maxWordLength;
    private bool wordSelected = false;
    public PuzzleController()
    {
        correctWords.AddRange(GameService.Instance.GridGenerator.gameData.words);
        maxWordLength = BiggestWordLength();
    }
    public void HandleCellClick(MouseInputHandler cell)
    {
        if (selectedCells.Count > 0 && wordSelected)
        {
            ResetSelection(); // Reset selection if a word is correctly selected and new selection is made
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
            return; // Prevent further selection
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
                CheckEmptyCorrectWords();// Set flag to true when a correct word is selected
            }
        }
    }
    private void ResetSelection()
    {
        wordSelected = false; // Reset wordSelected flag
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
        // Do nothing if a correct word is currently selected
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
            cell.SetCorrect(); // Mark cell as correct
        }
        correctWords.Remove(word); // Remove word from list of correct words
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
    public void CheckEmptyCorrectWords()
    {
        if (correctWords.Count == 0)
        {
            Debug.Log("Level Completed");
        }
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
}
