
using UnityEngine;

public class GameService : MonoBehaviour
{
    [SerializeField] private GridGenerator gridGenerator;
    [SerializeField] private UIService uiService;
    [SerializeField] private LevelService levelService;
    public static GameService Instance { get { return instance; } }
    private static GameService instance;
    public GridGenerator GridGenerator {  get { return gridGenerator; } }
    public UIService UIService { get { return uiService; } }
    public LevelService LevelService { get {  return levelService; } }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

}
