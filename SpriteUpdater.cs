using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteUpdater : MonoBehaviour
{
    LogicHandler logicHandler;
    Sprite sprite;
    public Sprite[] squareSprites;
    public Sprite[] faceSprites;
    public int coveredSquareSprite = 9;
    int mineIndex = 10;
    int redMineSpriteIndex = 11;
    int crossedMineIndex = 12;
    int flaggedSquareIndex = 13;

    void Start()
    {
        logicHandler = FindObjectOfType<LogicHandler>();
    }

    void Update()
    {
        
    }

    public void CheckForSpriteChange(GameObject selectedSquare)
    {
        if (selectedSquare != null)
        {
            ChooseAndApplySprite(selectedSquare);
        }
    }

    private void ChooseAndApplySprite(GameObject selectedSquare)
    {
        int spriteIndex;

        if (logicHandler.IsSquareCovered(selectedSquare) == true && logicHandler.IsSquareFlagged(selectedSquare) == false)
        {
            if (logicHandler.IsSquareWithMine(selectedSquare) == false)
            {
                spriteIndex = selectedSquare.GetComponent<Square>().adjacentMinesCounter;
                sprite = squareSprites[spriteIndex];
                selectedSquare.GetComponent<Square>().SetSprite(sprite);
            }

            else if (logicHandler.IsSquareCovered(selectedSquare) == true && logicHandler.IsGameWon() == true)
            {
                spriteIndex = mineIndex;
                sprite = squareSprites[spriteIndex];
                selectedSquare.GetComponent<Square>().SetSprite(sprite);
            }

            else if (logicHandler.IsSquareWithMine(selectedSquare) == true && GetComponent<Minesweeper>().isGameLost == false)
            {
                spriteIndex = redMineSpriteIndex;
                sprite = squareSprites[spriteIndex];
                selectedSquare.GetComponent<Square>().SetSprite(sprite);
            }

            else if (logicHandler.IsSquareWithMine(selectedSquare) == true && GetComponent<Minesweeper>().isGameLost == true)
            {
                spriteIndex = mineIndex;
                sprite = squareSprites[spriteIndex];
                selectedSquare.GetComponent<Square>().SetSprite(sprite);
            }

        }

        else if (logicHandler.IsSquareCovered(selectedSquare) == true && logicHandler.IsSquareFlagged(selectedSquare) == true && logicHandler.IsSquareWithMine(selectedSquare) == false && logicHandler.IsGameWon() == false)
        {
            spriteIndex = crossedMineIndex;
            sprite = squareSprites[spriteIndex];
            selectedSquare.GetComponent<Square>().SetSprite(sprite);
        }

    }

    public void CheckAndApplyFlagSprite(GameObject selectedSquare)
    {
        if (selectedSquare != null)
        {
            if (logicHandler.IsSquareCovered(selectedSquare) == true && logicHandler.IsSquareFlagged(selectedSquare) == false)
            {
                sprite = squareSprites[flaggedSquareIndex];
                selectedSquare.GetComponent<Square>().SetSprite(sprite);
            }

            else if (logicHandler.IsSquareCovered(selectedSquare) == true && logicHandler.IsSquareFlagged(selectedSquare) == true)
            {
                sprite = squareSprites[coveredSquareSprite];
                selectedSquare.GetComponent<Square>().SetSprite(sprite);
            }
        }
    }

    public void PrintSmileFace()
    {
        string faceObject = "Face";
        GameObject face = GameObject.Find(faceObject);
        Sprite faceSprite = face.GetComponent<SpriteRenderer>().sprite;

        face.GetComponent<SpriteRenderer>().sprite = faceSprites[0];
    }

    public void PrintSadFace()
    {
        string faceObject = "Face";
        GameObject face = GameObject.Find(faceObject);
        Sprite faceSprite = face.GetComponent<SpriteRenderer>().sprite;

        face.GetComponent<SpriteRenderer>().sprite = faceSprites[1];
    }
}
