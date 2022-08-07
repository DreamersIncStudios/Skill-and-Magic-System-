using DreamersInc.Utils;
using SkillMagicSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridModalWindow : MonoBehaviour
{
    public GameObject window;
    public GameObject HeaderTitle;
    public GameObject GridPanel;
    public Button GridButtonImage;

    public void DisplayGrid(GridGeneric<MagicGridObject> grid) {
        for (int x = 0; x < grid.GetWidth(); x++)
        {
            int i = x;
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                int j = y;
                GridLayoutGroup gridLayout = GridPanel.GetComponent<GridLayoutGroup>();
                gridLayout.cellSize = new Vector2(35, 35);
                gridLayout.constraint = GridLayoutGroup.Constraint.FixedRowCount;
                gridLayout.constraintCount = grid.GetWidth(); //TODO Expect change laters
                Button gridButton = Instantiate(GridButtonImage, GridPanel.transform);
                gridButton.navigation = new Navigation() { mode = Navigation.Mode.None };
                gridButton.onClick.AddListener(() => {
                    Debug.Log($"grid {i} , {j} current grid Status is {grid.GetGridObject(i, j).GetGridStatus()}" );
                });
            }
        }
    
    }
}
