using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public GameObject levelHolder;
    public GameObject levelIcon;

    private int numberOfLevels = 56;
    private Rect panelDimensions;
    private Rect iconDimensions;
    private int iconsPerPage;
    private int gridPadding = 100;
    private int iconSpacing = 50;

    void Start()
    {
        panelDimensions = levelHolder.GetComponent<RectTransform>().rect;
        iconDimensions = levelIcon.GetComponent<RectTransform>().rect;
        int maxInARow = Mathf.FloorToInt(
            (panelDimensions.width - (2 * gridPadding)) / (iconDimensions.width + iconSpacing)
        );
        int maxInACol = Mathf.FloorToInt(
            panelDimensions.height / (iconDimensions.height + iconSpacing)
        );
        iconsPerPage = maxInARow * maxInACol;
        int totalPages = Mathf.CeilToInt((float)numberOfLevels / (float)iconsPerPage);
        LoadPanels(totalPages);
    }

    void LoadPanels(int numberOfPanels)
    {
        GameObject panelClone = Instantiate(levelHolder) as GameObject;
        PageSwiper swiper = levelHolder.AddComponent<PageSwiper>();
        swiper.totalPages = numberOfPanels;

        for (int i = 1; i <= numberOfPanels; i++)
        {
            GameObject panel = Instantiate(panelClone) as GameObject;
            panel.transform.SetParent(gameObject.transform, false);
            panel.transform.SetParent(levelHolder.transform);
            panel.name = "Page " + i;
            panel.GetComponent<RectTransform>().localPosition =
                new Vector2(panelDimensions.width * (i - 1), 0);
            bool lastPanel = i == numberOfPanels;
            SetUpGrid(panel, lastPanel);
            LoadIcons(iconsPerPage, panel, i);
        }

        Destroy(panelClone);
    }

    void SetUpGrid(GameObject panel, bool isLastPanel)
    { 
        GridLayoutGroup grid = panel.AddComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(iconDimensions.width, iconDimensions.height);
        grid.spacing = new Vector2(iconSpacing, iconSpacing);
        grid.padding.right = grid.padding.left = gridPadding;
        grid.childAlignment = (isLastPanel)
            ? TextAnchor.MiddleLeft
            : TextAnchor.MiddleCenter;
    }

    void LoadIcons(int numberOfIcons, GameObject parentObject, int page)
    {
        int startIndex = (page - 1) * iconsPerPage;
        for (int i = startIndex + 1; i <= (startIndex + numberOfIcons); i++)
        {
            if (i > numberOfLevels) { break; }
            GameObject icon = Instantiate(levelIcon) as GameObject;
            icon.name = i.ToString();
            icon.transform.SetParent(gameObject.transform, false);
            icon.transform.SetParent(parentObject.transform);
            icon.GetComponentInChildren<TextMeshProUGUI>().SetText(i.ToString());
            icon.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Star_count_1");
        }
    }

    public void StartLevel()
    {
        SceneManager.LoadScene("Game");
    }
}
