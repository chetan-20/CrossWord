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
        InitializeButtons();
        ShowMenu();
    }

    private void InitializeButtons()
    {
        playButton.onClick.AddListener(OnPlayClicked);
        exitButton.onClick.AddListener(OnExitClicked);
        levelBackButton.onClick.AddListener(OnBackButtonClicked);
        levelResetButton.onClick.AddListener(OnResetButtonClicked);
        AssignLevelButtons();
    }

    private void ShowMenu()
    {
        menuObject.SetActive(true);
        levelButtonObject.SetActive(false);
        levelSelectorObject.SetActive(false);
    }     
    private void EnableLevelSelector()=> levelSelectorObject.SetActive(true);          
    private void DisableLevelSelector()=> levelSelectorObject.SetActive(false);
    private void DisableLevelButtons() => levelButtonObject.SetActive(false);
    private void EnableLevelButtons() => levelButtonObject.SetActive(true);
    private void OnResetButtonClicked()=>GameService.Instance.GridGenerator.ResetGrid();   
    private void OnPlayClicked() => ShowLevelSelector();    
    private void OnExitClicked()=> Application.Quit();
         
    private void AssignLevelButtons()
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
    private void ShowLevelSelector()
    {
        menuObject.SetActive(false);
        EnableLevelSelector();
    }  
    private void OnBackButtonClicked()
    {
        GameService.Instance.LevelService.DisableGrid();
        GameService.Instance.GridGenerator.RemoveCells();
        ShowMenu();
        DisableLevelButtons();
    }
        
}
