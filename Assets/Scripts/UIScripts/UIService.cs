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
    [SerializeField] private Button exitButton;
    [SerializeField] private Button levelBackButton;
    [SerializeField] private Button levelResetButton;   
    [SerializeField] private Button levelHintButton;
    [SerializeField] private GameObject levelOverObject;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button levelOverRestartButton;
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
        levelHintButton.onClick.AddListener(DisplayHint);
        menuButton.onClick.AddListener(OnBackButtonClicked);
        levelOverRestartButton.onClick.AddListener(OnLevelOverResetButtonClicked);
        AssignLevelButtons();
    }
    private void ShowMenu()
    {
        menuObject.SetActive(true);
        levelButtonObject.SetActive(false);
        levelSelectorObject.SetActive(false);
        levelOverObject.SetActive(false);
    }     
    private void EnableLevelSelector()=> levelSelectorObject.SetActive(true);          
    private void DisableLevelSelector()=> levelSelectorObject.SetActive(false);
    private void DisableLevelButtons() => levelButtonObject.SetActive(false);
    private void EnableLevelButtons() => levelButtonObject.SetActive(true);
    private void OnResetButtonClicked()
    {
        GameService.Instance.SoundManager.PlaySound(Sounds.ButtonClickSound);
        GameService.Instance.GridGenerator.ResetGrid();
    }
    private void OnPlayClicked() 
    {
        GameService.Instance.SoundManager.PlaySound(Sounds.ButtonClickSound);
        ShowLevelSelector();
    }
    private void OnExitClicked()
    {
        GameService.Instance.SoundManager.PlaySound(Sounds.ButtonClickSound);
        Application.Quit();
    }
    private void AssignLevelButtons()
    {
        Button[] buttons = levelSelectorObject.GetComponentsInChildren<Button>();
        for(int i=0; i<buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(()=>OnLevelSelectClicked(index));
        }
    }
    private void OnLevelSelectClicked(int level)
    {
        GameService.Instance.SoundManager.PlaySound(Sounds.ButtonClickSound);
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
        GameService.Instance.SoundManager.PlaySound(Sounds.ButtonClickSound);
        GameService.Instance.LevelService.DisableGrid();
        GameService.Instance.GridGenerator.RemoveCells();       
        DisableLevelButtons();
        levelOverObject.SetActive(false);
        ShowMenu();
    }
    public void DisplayHint()
    {
        GameService.Instance.HintController.ShowHint(GameService.Instance.puzzleController.GetRandomHint());
        GameService.Instance.SoundManager.PlaySound(Sounds.ButtonClickSound);
    }
    public void OnLevelOver()
    {
        DisableLevelButtons();
        levelOverObject.SetActive(true);
        GameService.Instance.LevelService.DisableGrid();
        GameService.Instance.SoundManager.PlaySound(Sounds.LevelOverSound);
    }
    private void OnLevelOverResetButtonClicked() 
    {
        GameService.Instance.SoundManager.PlaySound(Sounds.ButtonClickSound);
        levelOverObject.SetActive(false);
        GameService.Instance.LevelService.EnableGrid();
        GameService.Instance.GridGenerator.ResetGrid();
        EnableLevelButtons();
    }
}
