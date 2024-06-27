using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIService : MonoBehaviour
{
    [SerializeField] private GameObject backGround;
    [SerializeField] private GameObject menuObject;
    [SerializeField] private GameObject levelSelectorObject;
    [SerializeField] private GameObject levelButtonObject;
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button levelBackButton;
    [SerializeField] private Button levelResetButton;
    [SerializeField] private Button levelNextButton;
    [SerializeField] private Button levelHintButton;

    private void Start()
    {
        playButton.onClick.AddListener(SelectLevel);
        exitButton.onClick.AddListener(Application.Quit);
        levelBackButton.onClick.AddListener(OnBackButtonPressed);
        levelResetButton.onClick.AddListener(onResetButtonPressed);
        AssignLevelstoButtons();
        menuObject.SetActive(true);
        levelButtonObject.SetActive(false);
        levelSelectorObject.SetActive(false);
    }
    private void EnableMenu()=> menuObject.SetActive(true);    
    private void EnableLevelSelector()=> levelSelectorObject.SetActive(true);          
    private void DisableLevelSelector()=> levelSelectorObject.SetActive(false);
    private void DisableLevelButtons() => levelButtonObject.SetActive(false);
    private void EnableLevelButtons() => levelButtonObject.SetActive(true);
    private void SelectLevel()
    {
        menuObject.SetActive(false);
        EnableLevelSelector();
    }        
    private void AssignLevelstoButtons()
    {
        Button[] buttons = levelSelectorObject.GetComponentsInChildren<Button>();
        for(int i=0; i<buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(()=>OnLevelSelect(index));
        }
    }
    private void OnLevelSelect(int level)
    {
        DisableLevelSelector();        
        GameService.Instance.GridGenerator.gameData = GameService.Instance.LevelService.puzzleData[level];
        GameService.Instance.GridGenerator.GenerateLevel();
        GameService.Instance.LevelService.EnableGrid();
        EnableLevelButtons();
    }
    private void OnBackButtonPressed()
    {
        GameService.Instance.LevelService.DisableGrid();
        GameService.Instance.GridGenerator.RemoveCells();
        EnableMenu();
        DisableLevelButtons();
    }
    private void onResetButtonPressed()
    {
        GameService.Instance.GridGenerator.ResetGrid();
    }
}
