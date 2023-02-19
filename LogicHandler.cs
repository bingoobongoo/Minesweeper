using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicHandler : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public bool IsSquareWithMine(GameObject selectedSquare)
    {
        if (selectedSquare.GetComponent<Square>().containsMine == true)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    public bool IsSquareFlagged(GameObject selectedSquare)
    {
        if (selectedSquare.GetComponent<Square>().isFlagged == true)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    public void ChangeFlag(GameObject square)
    {
        if (IsSquareFlagged(square) == false && IsSquareCovered(square) == true)
        {
            GetComponent<SpriteUpdater>().CheckAndApplyFlagSprite(square);
            GetComponent<Minesweeper>().flagsPlaced++;
            square.GetComponent<Square>().isFlagged = true;
        }

        else if (IsSquareFlagged(square) == true && IsSquareCovered(square) == true)
        {
            GetComponent<SpriteUpdater>().CheckAndApplyFlagSprite(square);
            GetComponent<Minesweeper>().flagsPlaced--;
            square.GetComponent<Square>().isFlagged = false;
        }
    }

    public bool IsSquareCovered(GameObject selectedSquare)
    {
        if (selectedSquare.GetComponent<Square>().isCovered == true)
        {
            return true;
        }

        else 
        {
            return false;
        }
    }

    public void UncoverAllSquares()
    {
        int boardSize = GetComponent<Minesweeper>().boardSize;

        for (int rowIndex=0; rowIndex<boardSize; rowIndex++)
        {
            for (int columnIndex=0; columnIndex<boardSize; columnIndex++)
            {
                string squareName = GetComponent<Minesweeper>().CreateSquareName(rowIndex, columnIndex);
                GameObject square = GameObject.Find(squareName);

                UncoverSquare(square);
            }
        }
    }

    public void UncoverSquare(GameObject square)
    {
        if (IsSquareFlagged(square) == false && IsSquareCovered(square) == true)
        {
            GetComponent<SpriteUpdater>().CheckForSpriteChange(square);
            square.GetComponent<Square>().isCovered = false;
        }

        else if (IsSquareFlagged(square) == true && IsSquareCovered(square) == true && GetComponent<Minesweeper>().isGameLost)
        {
            GetComponent<SpriteUpdater>().CheckForSpriteChange(square);
            square.GetComponent<Square>().isCovered = false;
        }
    }   

    public void AutoUncoverConnectedSquares(GameObject square)
    {
        int minesNumber = square.GetComponent<Square>().adjacentMinesCounter;
        bool isCovered = GetComponent<LogicHandler>().IsSquareCovered(square);

        if (minesNumber == 0 && isCovered && IsSquareWithMine(square) == false)
        {
            int thisSquareRow = square.GetComponent<Square>().row;
            int thisSquareColumn = square.GetComponent<Square>().column;

            GetComponent<LogicHandler>().UncoverSquare(square);

            AutoUncoverSquare(thisSquareRow + 1, thisSquareColumn); // top
            AutoUncoverSquare(thisSquareRow + 1, thisSquareColumn + 1); // top-right
            AutoUncoverSquare(thisSquareRow, thisSquareColumn + 1); // right
            AutoUncoverSquare(thisSquareRow - 1, thisSquareColumn + 1); // bottom-right
            AutoUncoverSquare(thisSquareRow - 1, thisSquareColumn); // bottom
            AutoUncoverSquare(thisSquareRow - 1, thisSquareColumn - 1); // bottom-left
            AutoUncoverSquare(thisSquareRow, thisSquareColumn - 1); // left
            AutoUncoverSquare(thisSquareRow + 1, thisSquareColumn - 1); // topleft            

        }

        else if (minesNumber != 0 && isCovered && IsSquareWithMine(square) == false)
        {
            GetComponent<LogicHandler>().UncoverSquare(square);
            
            if (IsGameWon())
                GetComponent<Minesweeper>().WinGame();
        }

        else if (IsSquareWithMine(square) == true && IsSquareFlagged(square) == false)
        {
            GetComponent<LogicHandler>().UncoverSquare(square);
            GetComponent<Minesweeper>().LostGame();
        }
    }

    public bool IsGameWon()
    {
        int boardSize = GetComponent<Minesweeper>().boardSize;

        for (int rowIndex = 0; rowIndex<boardSize; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex<boardSize; columnIndex++)
            {
                string squareName = GetComponent<Minesweeper>().CreateSquareName(rowIndex, columnIndex);
                GameObject square = GameObject.Find(squareName);

                if (GetComponent<LogicHandler>().IsSquareCovered(square) && GetComponent<LogicHandler>().IsSquareWithMine(square) == false)
                    return false;
            }
        }

            return true;
    }

    public void CalculateAdjacentMinesNumber(GameObject square)
    {
        CheckAdjacentSquares(square);
    }

    private void CheckAdjacentSquares(GameObject square)
    {
        int thisSquareRow = square.GetComponent<Square>().row;
        int thisSquareColumn = square.GetComponent<Square>().column;

        CheckSquareMine(square, thisSquareRow + 1, thisSquareColumn); // top
        CheckSquareMine(square, thisSquareRow + 1, thisSquareColumn + 1); // top-right
        CheckSquareMine(square, thisSquareRow, thisSquareColumn + 1); // right
        CheckSquareMine(square, thisSquareRow - 1, thisSquareColumn + 1); // bottom-right
        CheckSquareMine(square, thisSquareRow - 1, thisSquareColumn); // bottom
        CheckSquareMine(square, thisSquareRow - 1, thisSquareColumn - 1); // bottom-left
        CheckSquareMine(square, thisSquareRow, thisSquareColumn - 1); // left
        CheckSquareMine(square, thisSquareRow + 1, thisSquareColumn - 1); // topleft
    }

    private void CheckSquareMine(GameObject square, int adjacentSquareRow, int adjacentSquareColumn)
    {
        string adjacentSquareName = GetComponent<Minesweeper>().CreateSquareName(adjacentSquareRow, adjacentSquareColumn);

        if (squareExists(adjacentSquareName))
        {
            GameObject adjacentSquare = GameObject.Find(adjacentSquareName);
            
            if (IsSquareWithMine(adjacentSquare) == true)
            {
                square.GetComponent<Square>().adjacentMinesCounter++;
            }     
        }      
    }
    
    private void AutoUncoverSquare(int adjacentSquareRow, int adjacentSquareColumn)
    {
        string adjacentSquareName = GetComponent<Minesweeper>().CreateSquareName(adjacentSquareRow, adjacentSquareColumn);

        if (squareExists(adjacentSquareName))
        {
            GameObject adjacentSquare = GameObject.Find(adjacentSquareName);

            GetComponent<LogicHandler>().AutoUncoverConnectedSquares(adjacentSquare);    
        }              
    }

    private bool squareExists(string name)
    {
        if (GameObject.Find(name) != null)
        {
            return true;
        }

        else
        {
            return false;
        }

    }


}
