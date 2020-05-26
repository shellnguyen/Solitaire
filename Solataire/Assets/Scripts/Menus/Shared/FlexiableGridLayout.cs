using UnityEngine;
using UnityEngine.UI;

public class FlexiableGridLayout : LayoutGroup
{
    public enum FitType
    {
        Uniform,
        Width,
        Height,
        FixedRows,
        FixedColumns
    }

    public FitType fitType;
    public int rows;
    public int columns;
    public Vector2 cellsSize;
    public Vector2 spacing;

    public bool fitX;
    public bool fitY;

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        if(fitType == FitType.Width || fitType == FitType.Height || fitType == FitType.Uniform)
        {
            fitX = true;
            fitY = true;
        }
        float gridSize = Mathf.Sqrt(transform.childCount);
        rows = Mathf.CeilToInt(gridSize);
        columns = Mathf.CeilToInt(gridSize);

        if(fitType == FitType.Width || fitType == FitType.FixedColumns)
        {
            rows = Mathf.CeilToInt(transform.childCount / (float)columns);
        }

        if(fitType == FitType.Height || fitType == FitType.FixedRows)
        {
            columns = Mathf.CeilToInt(transform.childCount / (float)rows);
        }

        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float cellWidth = (parentWidth / (float)columns) - ((spacing.x / (float)columns) * (columns - 1)) - (padding.left / (float)columns) - (padding.right / (float)columns);
        float cellHeight = (parentHeight / (float)rows) - ((spacing.y / (float)rows) * (rows - 1)) - (padding.top / (float)rows) - (padding.bottom / (float)rows);

        cellsSize.x = fitX ? cellWidth : cellsSize.x;
        cellsSize.y = fitY ? cellHeight : cellsSize.y;

        int rowCount = 0;
        int columnCount = 0;

        for(int i = 0; i < rectChildren.Count; ++i)
        {
            rowCount = i / columns;
            columnCount = i % columns;

            RectTransform item = rectChildren[i];

            float xPos = (cellsSize.x * columnCount) + (spacing.x * columnCount) + padding.left;
            float yPos = (cellsSize.y * rowCount) + (spacing.y * rowCount) + padding.top; 

            SetChildAlongAxis(item, 0, xPos, cellsSize.x);
            SetChildAlongAxis(item, 1, yPos, cellsSize.y);
        }
    }

    public override void CalculateLayoutInputVertical()
    {
        
    }

    public override void SetLayoutHorizontal()
    {
        
    }

    public override void SetLayoutVertical()
    {
        
    }
}
