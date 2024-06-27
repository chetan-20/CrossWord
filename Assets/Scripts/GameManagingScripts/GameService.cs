
using UnityEngine;

public class GameService : MonoBehaviour
{
    [SerializeField] private GridGenerator gridGenerator;
    [SerializeField] private UIService uiService;   
    public static GameService Instance { get { return instance; } }
    private static GameService instance;
    public GridGenerator GridGenerator {  get { return gridGenerator; } }
    public UIService UIService { get { return uiService; } }

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
    }

}
