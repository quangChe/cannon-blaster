using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelSelectController : MonoBehaviour
{
    public GameObject levelHolder;
    public GameObject levelButton;

    private int numberOfLevels;
    private Rect panelDimensions;
    private Rect btnDimensions;
    private int btnsPerPage;
    private readonly int gridPadding = 100;
    private readonly int btnSpacing = 50;

    void Start()
    {
        numberOfLevels = 112;
        //numberOfLevels = GameManager.Instance.levels.Length;
        panelDimensions = levelHolder.GetComponent<RectTransform>().rect;
        btnDimensions = levelButton.GetComponent<RectTransform>().rect;
        int maxInARow = Mathf.FloorToInt(
            (panelDimensions.width - (2 * gridPadding)) / (btnDimensions.width + btnSpacing)
        );
        int maxInACol = Mathf.FloorToInt(
            panelDimensions.height / (btnDimensions.height + btnSpacing)
        );
        btnsPerPage = maxInARow * maxInACol;
        int totalPages = Mathf.CeilToInt((float)numberOfLevels / (float)btnsPerPage);
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
            LoadIcons(btnsPerPage, panel, i);
        }

        Destroy(panelClone);
    }

    void SetUpGrid(GameObject panel, bool isLastPanel)
    { 
        GridLayoutGroup grid = panel.AddComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(btnDimensions.width, btnDimensions.height);
        grid.spacing = new Vector2(btnSpacing, btnSpacing);
        grid.padding.right = grid.padding.left = gridPadding;
        grid.childAlignment = (isLastPanel)
            ? TextAnchor.MiddleLeft
            : TextAnchor.MiddleCenter;
    }

    void LoadIcons(int numberOfIcons, GameObject parentObject, int page)
    {
        int startIndex = (page - 1) * btnsPerPage;
        for (int i = startIndex + 1; i <= (startIndex + numberOfIcons); i++)
        {
            if (i > numberOfLevels) { break; }
            GameObject btn = Instantiate(levelButton) as GameObject;
            btn.name = i.ToString();
            btn.transform.SetParent(gameObject.transform, false);
            btn.transform.SetParent(parentObject.transform);
            btn.GetComponentInChildren<TextMeshProUGUI>().SetText(i.ToString());

            if (i > GameManager.Instance.levels.Length)
            {
                btn.GetComponent<Image>().sprite = Resources.Load<Sprite>("sprites/locked_stage");
                Destroy(btn.transform.GetChild(1).GetComponent<Image>());
            }
            else
            {

                // Load star data here
                btn.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("sprites/stars1");

                int levelNumber = i;
                btn.GetComponent<Button>().onClick.AddListener(() =>
                {
                    GameManager.Instance.LoadLevel(levelNumber);
                    SceneManager.LoadScene("Game");
                });
            }

        }
    }
}
