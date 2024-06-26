
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class PuzzleData : ScriptableObject
{
    public float timeInSeconds;
    public int rows;
    public int columns;
    [System.Serializable]
    public class SearchingWord
    {
        public string word;
    }
    [System.Serializable]
    public class BoardRow
    {
        public int size;
        public string[] Row;
        public BoardRow() { }
        public BoardRow(int size) 
        { 
            this.size = size;
            Row = new string[size];
            ClearRow();
        }
        public void ClearRow()
        {
            for(int i = 0; i < size; i++)
            {
                Row[i] = " ";
            }
        }
        
    }
    public BoardRow[] Board;
    public void CreateWithEmptyString()
    {
        for(int i =0; i < columns; i++)
        {
            Board[i].ClearRow();
        }
    }
    public void CreateNewBoard()
    {
        Board = new BoardRow[columns];
        for(int i = 0;i < columns; i++)
        {
            Board[i] = new BoardRow(rows);
        }
    }
}
