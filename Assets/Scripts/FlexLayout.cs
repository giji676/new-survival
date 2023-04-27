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

        float parentWidth = rectTransform.rect.width - padding.left - padding.right;
        float parentHeight = rectTransform.rect.height - padding.top - padding.bottom;

        float cellWidth;
        float cellHeight;

        float xPos = spacing.x;
        float yPos = spacing.y;
        float prevX = padding.left;
        float prevY = padding.top;

        if (direction == Direction.Row) {
            cellHeight = parentHeight;

            if (rectChildren.Count <= ratio.Count && ratio.Count > 0) {
                float perRatio;
                int ratioSum = GetSum(ratio);

                perRatio = parentWidth / ratioSum;

                for (int i = 0; i < rectChildren.Count; i++) {
                    RectTransform item = rectChildren[i];
                    
                    cellWidth = perRatio * ratio[i] - spacing.x / 2;
                    yPos = prevY;
                    xPos = prevX;
                    prevX = xPos + cellWidth + spacing.x;

                    SetChildAlongAxis(item, 0, xPos, cellWidth);
                    SetChildAlongAxis(item, 1, yPos, cellHeight);
                }
            }
            else {
                int columns = rectChildren.Count;

                cellWidth = parentWidth / columns - spacing.x / 2;
                
                for (int i = 0; i < rectChildren.Count; i++) {
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
            cellWidth = parentWidth;

            if (rectChildren.Count <= ratio.Count && ratio.Count > 0) {
                float perRatio;
                int ratioSum = GetSum(ratio);

                perRatio = parentHeight / ratioSum;
                
                for (int i = 0; i < rectChildren.Count; i++) {
                    RectTransform item = rectChildren[i];

                    cellHeight = perRatio * ratio[i] - spacing.y / 2;
                    yPos = prevY;
                    xPos = prevX;
                    prevY = yPos + cellHeight + spacing.y;

                    SetChildAlongAxis(item, 0, xPos, cellWidth);
                    SetChildAlongAxis(item, 1, yPos, cellHeight);
                }
            }
            else {
                int rows = rectChildren.Count;

                cellHeight = parentHeight / rows - spacing.y / 2;
                
                for (int i = 0; i < rectChildren.Count; i++) {
                    RectTransform item = rectChildren[i];
                    
                    yPos = prevY;
                    xPos = prevX;
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