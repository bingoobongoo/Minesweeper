using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minesweeper : MonoBehaviour
{
    [SerializeField]
    private Square squareReference;
    
    private Text gameText;
    private Square square;
    public int boardSize;
    public int minesNumber;
    public int flagsPlaced;
    private float boardSquareScale;
    public bool isGameLost;

    void Start()
    {
        GenerateGameBoard();
    }

    void Update()
    {
        
    }

    private void GenerateGameBoard()
    {
        SetBoardAttributes();
        InstantiateSquares();
        PlaceMines();
        CalculateMines();
    }

    private void InstantiateSquares()
    {
        int rowIndex;
        int columnIndex;
        string squareName;
        Vector3 squarePosition;

        Debug.Log("Rozpoczęto tworzenie obiektów");

        for (rowIndex = 0; rowIndex < boardSize; rowIndex++)
        {
            for (columnIndex = 0; columnIndex < boardSize; columnIndex++)
            {
                square = Instantiate(squareReference);
                squareName = CreateSquareName(rowIndex, columnIndex);
                squarePosition = CalculateSquarePosition(rowIndex, columnIndex);
                square.InitializeTransformAttributes(squarePosition, boardSquareScale, squareName);
                square.InitializeRowAndColumn(rowIndex, columnIndex);
                square.AssignInitialValues();
                
            }
        }

    }

    public void ResetGame()
    {
        int rowIndex;
        int columnIndex;

        for (rowIndex = 0; rowIndex < boardSize; rowIndex++)
        {
            for (columnIndex = 0; columnIndex < boardSize; columnIndex++)
            {
                string squareName = CreateSquareName(rowIndex, columnIndex);
                GameObject square = GameObject.Find(squareName);
                int defualtSpriteIndex = GetComponent<SpriteUpdater>().coveredSquareSprite;
                Sprite defualtSprite = GetComponent<SpriteUpdater>().squareSprites[defualtSpriteIndex];
                square.GetComponent<Square>().AssignInitialValues();
                square.GetComponent<Square>().SetSprite(defualtSprite);
            }
        }

        PlaceMines();
        CalculateMines();
        GetComponent<SpriteUpdater>().PrintSmileFace();
        flagsPlaced = 0;
        isGameLost = false;
    }

    private void PlaceMines()
    {
        System.Random random = new System.Random();
        GameObject randomSquare;
        int randomColumnIndex;
        int randomRowIndex;
        string bombSquareName;
        bool squareContainsMine;

        Debug.Log("Rozpoczęto rozmieszczanie min");

        for (int i=0; i<minesNumber; i++)
        {
            randomColumnIndex = random.Next(boardSize);
            randomRowIndex = random.Next(boardSize);
            bombSquareName = CreateSquareName(randomRowIndex, randomColumnIndex);

            randomSquare = GameObject.Find(bombSquareName);
            squareContainsMine = randomSquare.GetComponent<Square>().containsMine;


            if (squareContainsMine == false)
            {
                randomSquare.GetComponent<Square>().containsMine = true;
            }

            else
            {
                i--;
            }
        }
    }

    private void CalculateMines()
    {
        for (int rowIndex = 0; rowIndex < boardSize; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < boardSize; columnIndex++)
            {
                GameObject square;
                string squareName = CreateSquareName(rowIndex, columnIndex);
                square = GameObject.Find(squareName);

                GetComponent<LogicHandler>().CalculateAdjacentMinesNumber(square);
            }
        }
    }

    public void LostGame()
    {
        isGameLost = true;
        GetComponent<SpriteUpdater>().PrintSadFace();
        GetComponent<LogicHandler>().UncoverAllSquares();
    }

    public void WinGame()
    {
        GetComponent<LogicHandler>().UncoverAllSquares();
        Debug.Log("Game WON!");
    }

    private Vector3 CalculateSquarePosition(int rowIndex, int columnIndex)
    {
        Vector3 squarePosition;
        float startingPositon_x = -3.5f;
        float startingPositon_y = -3.5f;
        float positionShift = 5.13f * boardSquareScale; // 5.13 is exact distance between two adject squares, when scale = 1

        float calculatedPosition_x;
        float calculatedPosition_y;

        calculatedPosition_x = startingPositon_x + columnIndex * positionShift;
        calculatedPosition_y = startingPositon_y + rowIndex * positionShift;

        squarePosition = new Vector3(calculatedPosition_x, calculatedPosition_y, 0);

        return squarePosition;
    }

    public string CreateSquareName(int rowIndex, int columnIndex)
    {
        string s = "S " + columnIndex + " " + rowIndex;
        return s; 
    }

    private void SetBoardAttributes()
    {
        boardSize = 15;
        boardSquareScale = 0.1f;
        minesNumber = 35;
        flagsPlaced = 0;
        isGameLost = false;

        Debug.Log("Ustawiono atrybuty gry");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
