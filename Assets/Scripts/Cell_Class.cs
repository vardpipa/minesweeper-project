using UnityEngine;
using UnityEngine.UI;

public enum Cell
{
    Empty,
    Mine
}

public class Cell_Class : MonoBehaviour
{
    public Cell cell;
    [SerializeField] private Flag_Class flagClass;
    private int x, y;
    public void SetCoordinations(int x_, int y_) { x = x_; y = y_; }
    public int GetCoordinationX() { return x; }
    public int GetCoordinationY() { return y; }
    public bool flag;
    public bool isBlocked;
    public bool isOpened;
    [SerializeField] private GameManager_Script gameManager;

    void Start()
    {
        gameManager = GameObject.FindFirstObjectByType<GameManager_Script>();
    }

    public void Click()
    {
        if (!flag && !isBlocked)
        {
            if (cell == Cell.Empty)
            {
                gameManager.OpenCell(x, y);
                gameManager.EraseCell();
                gameManager.digAudio.Play();
                gameManager.CheckWin();
            }
            else if (cell == Cell.Mine)
            {
                gameManager.explotionManager.Activate(this);
                gameManager.GameModeUpdate(GameMode.GameOver);
            }
        }
    }

    public void RigthClick()
    {
        if (!flag && !isBlocked)
        {
            flagClass.Activater();
        }
        else
        {
            flagClass.Deactivater();
        }
    }

    public void Block()
    {
        isBlocked = true;
    }
}
