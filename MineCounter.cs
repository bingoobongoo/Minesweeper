using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MineCounter : MonoBehaviour
{
    public Minesweeper minesweeper;
    private Text minesLeftText;

    // Start is called before the first frame update
    void Start()
    {
        minesLeftText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateLeftMines();
    }

    private void CalculateLeftMines()
    {
        int flagsPlaced = minesweeper.flagsPlaced;
        int minesNumber = minesweeper.minesNumber;

        int minesLeft = minesNumber - flagsPlaced;
        minesLeftText.text = minesLeft.ToString();
    }
}
