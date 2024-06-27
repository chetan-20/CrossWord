using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController
{
    private List<MouseInputHandler> selectedCells = new List<MouseInputHandler>();
    private List<string> selectedLetters = new List<string>();
    private List<string> correctWords = new List<string>();
    private int maxWordLength;

    public PuzzleController()
    {
        correctWords.AddRange(GameService.Instance.GridGenerator.gameData.words);
        maxWordLength = GameService.Instance.GridGenerator.BiggestWordLength();
    }
    public void HandleCellClick(MouseInputHandler cell)
    {
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
            }
        }
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
            Vector2Int currentPos = GetCellPosition(cell);
            Vector2Int lastPos = GetCellPosition(lastCell);
            return Mathf.Abs(currentPos.x - lastPos.x) + Mathf.Abs(currentPos.y - lastPos.y) == 1;
        }
        return true; // Always allow first selection
    }

    private Vector2Int GetCellPosition(MouseInputHandler cell)
    {
        GameObject[,] gridCells = GameService.Instance.GridGenerator.gridCells;
        for (int x = 0; x < gridCells.GetLength(0); x++)
        {
            for (int y = 0; y < gridCells.GetLength(1); y++)
            {
                if (gridCells[x, y] == cell.gameObject)
                {
                    return new Vector2Int(x, y);
                }
            }
        }
        return Vector2Int.zero;
    }

    private void EndSelection()
    {
        string currentWord = string.Join("", selectedLetters);

        if (currentWord.Length > maxWordLength)
        {
            ResetIncorrectCells();
        }
        else if (IsCorrectWord(currentWord))
        {
            MarkCorrectWord(currentWord);
        }
        else
        {
            ResetIncorrectCells();
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
        correctWords.Remove(word); // Remove word from list of correct words
    }

    private void ResetIncorrectCells()
    {
        foreach (MouseInputHandler cell in selectedCells)
        {
            cell.ResetCell();
        }
    }

    public void CheckEmptyCorrectWords()
    {
        if (correctWords.Count == 0)
        {
            // Call some method or perform action when all correct words are found
        }
    }
}
