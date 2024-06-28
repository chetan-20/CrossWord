
using UnityEngine;

public class GameService : MonoBehaviour
{
    [SerializeField] private GridGenerator gridGenerator;
    [SerializeField] private UIService uiService;
    [SerializeField] private LevelService levelService;
    [SerializeField] private HintController hintController;
    [SerializeField] private SoundManager soundManager;
    public PuzzleController puzzleController;
    public static GameService Instance { get { return instance; } }
    private static GameService instance;
    public GridGenerator GridGenerator {  get { return gridGenerator; } }
    public UIService UIService { get { return uiService; } }
    public LevelService LevelService { get {  return levelService; } }
    public PuzzleController PuzzleController { get {  return puzzleController; } }
    public HintController HintController { get { return hintController; } }
    public SoundManager SoundManager { get {  return soundManager; } }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;            
        }
        else
        {
           Destroy(gameObject);
        }
        if (puzzleController == null)
        {
            puzzleController = new PuzzleController();
        }
    }
   
}
