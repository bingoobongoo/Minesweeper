using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    private Minesweeper minesweeper;
    private LogicHandler logicHandler;
    
    void Start()
    {
        minesweeper = FindObjectOfType<Minesweeper>();
        logicHandler = FindObjectOfType<LogicHandler>();
    }

    void Update()
    {
        GetMouseButton();
    }

     private void GetMouseButton()
    {
        GetLeftClick();
        GetRightClick();
    }

    private void GetLeftClick()
    {
        bool isLeftButtonClicked = Input.GetMouseButtonDown(0);

        if (isLeftButtonClicked)
        {
            GameObject square;
            square = GetObjectFromRaycastHit();
            if (square != null)
            {
                logicHandler.AutoUncoverConnectedSquares(square);
            }
        }

    }

    private void GetRightClick()
    {
        bool isRightButtonClicked = Input.GetMouseButtonDown(1);

        if (isRightButtonClicked)
        {
            GameObject square;
            square = GetObjectFromRaycastHit();
            if (square != null)
            {
                logicHandler.ChangeFlag(square);
            }
        }

    }

    private GameObject GetObjectFromRaycastHit()
    {
        RaycastHit2D rayHit;

        rayHit = SendRaycast();

        if (rayHit.collider != null && rayHit == true)
        {
            GameObject selectedSquare = rayHit.collider.gameObject;

            return selectedSquare;
        }

        else
        {
            return null;
        }
    }

    private RaycastHit2D SendRaycast()
    {
        Vector3 offsetMousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10);
        Vector3 cameraToPointVector = Camera.main.ScreenToWorldPoint(offsetMousePosition);
        Vector3 mousePosition = cameraToPointVector;

        RaycastHit2D rayHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        return rayHit;
    }
   
}
