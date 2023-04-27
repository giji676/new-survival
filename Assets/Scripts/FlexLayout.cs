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

        float parentWidth = rectTransform.rect.width - spacing.x;
        float parentHeight = rectTransform.rect.height;

        float cellWidth;
        float cellHeight;

        if (direction == Direction.Row) {
            if (rectChildren.Count <= ratio.Count && ratio.Count > 0) {
                float perRatio;
                int ratioSum = GetSum(ratio);

                perRatio = parentWidth / ratioSum;
                cellHeight = parentHeight - spacing.y * 2;

                float xPos = spacing.x;
                float yPos = spacing.y;
                float prevX = 0;
                for (int i = 0; i < rectChildren.Count; i++) {
                    RectTransform item = rectChildren[i];
                    cellWidth = perRatio * ratio[i] - spacing.x;
                    xPos = prevX + spacing.x;
                    prevX = xPos + cellWidth;

                    SetChildAlongAxis(item, 0, xPos, cellWidth);
                    SetChildAlongAxis(item, 1, yPos, cellHeight);
                }
            }
            else {
                int columns = rectChildren.Count;

                cellWidth = parentWidth / columns - spacing.x;
                cellHeight = parentHeight - spacing.y * 2;
                
                float xPos = spacing.x;
                float yPos = spacing.y;
                float prevX = 0;
                for (int i = 0; i < rectChildren.Count; i++) {
                    RectTransform item = rectChildren[i];
                    xPos = prevX + spacing.x;
                    prevX = xPos + cellWidth;

                    SetChildAlongAxis(item, 0, xPos, cellWidth);
                    SetChildAlongAxis(item, 1, yPos, cellHeight);
                }
            }
        }
        
        if (direction == Direction.Column) {
            if (rectChildren.Count <= ratio.Count && ratio.Count > 0) {
                float perRatio;
                int ratioSum = GetSum(ratio);

                perRatio = parentHeight / ratioSum;
                cellWidth = parentWidth - spacing.x * 2;

                float xPos = spacing.x;
                float yPos = spacing.y;
                float prevY = 0;
                for (int i = 0; i < rectChildren.Count; i++) {
                    RectTransform item = rectChildren[i];
                    cellHeight = perRatio * ratio[i] - spacing.y;
                    yPos = prevY + spacing.y;
                    prevY = yPos + cellHeight;

                    SetChildAlongAxis(item, 0, xPos, cellWidth);
                    SetChildAlongAxis(item, 1, yPos, cellHeight);
                }
            }
            else {
                int rows = rectChildren.Count;

                cellWidth = parentWidth - spacing.x * 2;
                cellHeight = parentHeight / rows - spacing.y;
                
                float xPos = spacing.x;
                float yPos = spacing.y;
                float prevY = 0;
                for (int i = 0; i < rectChildren.Count; i++) {
                    RectTransform item = rectChildren[i];
                    yPos = prevY + spacing.y;
                    prevY = yPos + cellHeight;

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