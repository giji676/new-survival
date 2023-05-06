using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlexLayout : LayoutGroup {
    public enum Direction {
        Row,
        Column
    }

    public Direction direction = Direction.Row;
    public List<int> ratio;
    public int rows;
    public int columns;
    public Vector2 spacing;

    public override void CalculateLayoutInputHorizontal() {
        base.CalculateLayoutInputHorizontal();
        if (rectChildren == null) return;

        float parentWidth = rectTransform.rect.width - padding.left - padding.right;
        float parentHeight = rectTransform.rect.height - padding.top - padding.bottom;

        float cellWidth;
        float cellHeight;

        float xPos = spacing.x;
        float yPos = spacing.y;
        float prevX = padding.left;
        float prevY = padding.top;

        if (direction == Direction.Row) {
            int columns = rectChildren.Count;
            cellHeight = parentHeight;
            parentWidth -= spacing.x * (columns-1);

            if (columns <= ratio.Count && ratio.Count > 0) {
                float perRatio;
                int ratioSum = GetSum(ratio);

                perRatio = parentWidth / ratioSum;

                for (int i = 0; i < columns; i++) {
                    RectTransform item = rectChildren[i];
                    
                    cellWidth = perRatio * ratio[i];
                    yPos = prevY;
                    xPos = prevX;
                    prevX = xPos + cellWidth + spacing.x;

                    SetChildAlongAxis(item, 0, xPos, cellWidth);
                    SetChildAlongAxis(item, 1, yPos, cellHeight);
                }
            }
            else {
                cellWidth = parentWidth / columns;
                
                for (int i = 0; i < columns; i++) {
                    RectTransform item = rectChildren[i];

                    yPos = prevY;
                    xPos = prevX;
                    prevX = xPos + cellWidth + spacing.x;

                    SetChildAlongAxis(item, 0, xPos, cellWidth);
                    SetChildAlongAxis(item, 1, yPos, cellHeight);
                }
            }
        }

        if (direction == Direction.Column) {
            int rows = rectChildren.Count;
            cellWidth = parentWidth;
            parentHeight -= spacing.y * (rows-1);

            if (rows <= ratio.Count && ratio.Count > 0) {
                float perRatio;
                int ratioSum = GetSum(ratio);

                perRatio = parentHeight / ratioSum;

                for (int i = 0; i < rows; i++) {
                    RectTransform item = rectChildren[i];
                    
                    cellHeight = perRatio * ratio[i];
                    xPos = prevX;
                    yPos = prevY;
                    prevY = yPos + cellHeight + spacing.y;

                    SetChildAlongAxis(item, 0, xPos, cellWidth);
                    SetChildAlongAxis(item, 1, yPos, cellHeight);
                }
            }
            else {

                cellHeight = parentHeight / rows;
                
                for (int i = 0; i < rows; i++) {
                    RectTransform item = rectChildren[i];

                    xPos = prevX;
                    yPos = prevY;
                    prevY = yPos + cellHeight + spacing.y;

                    SetChildAlongAxis(item, 0, xPos, cellWidth);
                    SetChildAlongAxis(item, 1, yPos, cellHeight);
                }
            }
        }
    }

    public override void CalculateLayoutInputVertical() {

    }

    public override void SetLayoutHorizontal() {

    }

    public override void SetLayoutVertical() {

    }

    private int GetSum(List<int> arr) {
        int sum = 0;
        for (int i = 0; i < arr.Count; i++) {
            sum += arr[i];
        }
        return sum;
    }
}