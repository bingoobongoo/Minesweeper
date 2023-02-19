using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    SpriteUpdater spriteUpdater;

    private float scale;
    private Vector3 position;
    private string squareName;
    private SpriteRenderer spriteRenderer;
    public int column;
    public int row;
    public int adjacentMinesCounter = 0;
    public bool containsMine;
    public bool isCovered;
    public bool isFlagged;

    void Start()
    {
        AssignSpriteRendererComponent(); 
        if (FindObjectOfType<SpriteUpdater>() != null)
            spriteUpdater  = FindObjectOfType<SpriteUpdater>();
        else
            Debug.Log("huja znaleziono");
    }

    void Update()
    {
        
    }

    public void InitializeTransformAttributes(Vector3 position, float scale, string name)
    {
        this.position = position;
        this.scale = scale;
        this.squareName = name;

        AssignScale();
        AssignPosition();
        AssignName();
    }

    public void InitializeRowAndColumn(int row, int column)
    {
        this.row = row;
        this.column = column;
    }

    public void AssignInitialValues()
    {
        isCovered = true;
        containsMine = false;
        isFlagged = false;
        adjacentMinesCounter = 0;
    }

    private void AssignScale()
    {
        float defaultScale = 1;

        float scale_x = scale;
        float scale_y = scale;
        float scale_z = defaultScale;

        transform.localScale = new Vector3(scale_x, scale_y, scale_z);
    }

    private void AssignPosition()
    {
        transform.position = position; 
    }

    public void AssignName()
    {
        transform.name = squareName;
    }

    private void AssignSpriteRendererComponent()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
}


