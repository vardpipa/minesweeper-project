using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum GameMode
{
    Game,
    Pause,
    GameOver,
    GameWin
}

public class GameManager_Script : MonoBehaviour
{
    public AudioSource digAudio;
    public GameObject digEffect;

    public GameMode gameMode;
    [SerializeField] private UpdateCells_Script updateCellsScript;
    public Counter_Script counterScript;
    public InterfaceLower_Script interfaceLowerScript;
    public ExplotionManager_Script explotionManager;
    [SerializeField] private GameObject numbers;

    private List<Cell_Class> cells;
    private List<Cell_Class> cellsErase;

    public void SetCellsToMine()
    {
        int amount = GetMineAmount();

        while (amount > 0)
        {
            int mineIndex = Random.Range(0, cells.Count);

            if (cells[mineIndex].cell != Cell.Mine)
            {
                cells[mineIndex].cell = Cell.Mine;
                --amount;
            }
        }
    }
    public void AddCell(Cell_Class cell_) { cells.Add(cell_); }
    public Cell_Class FindCell(int x, int y)
    {
        Cell_Class cell_;

        try
        {
            cell_ = cells.Find(i => i.GetCoordinationX() == x && i.GetCoordinationY() == y);
        }
        catch
        {
            cell_ = null;
        }

        return cell_;
    }
    public void EraseCell(int x, int y)
    {
        Cell_Class cell = cells.Find(i => i.GetCoordinationX() == x && i.GetCoordinationY() == y);

        cells.Remove(cell);
    }
    public void EraseCell(Cell_Class cell)
    {
        cells.Remove(cell);
    }
    public void EraseCell()
    {
        for (int i = 0; i < cellsErase.Count; ++i)
        {
            cellsErase.Remove(cellsErase[i]);
        }
    }
    public void Clear()
    {
        if (cells.Count > 0)
        {
            for (int i = 0; i < cells.Count; ++i)
            {
                Destroy(cells[i].gameObject);
            }

            cells.Clear();
        }
    }

    private int mineCounter = 0;
    public void SetMine(int counter_) { mineCounter = counter_; }
    public void MinusMine() { --mineCounter; UpdateCounter(mineCounterText, mineCounter); }
    public void PlusMine() { ++mineCounter; UpdateCounter(mineCounterText, mineCounter); }
    private int flagCounter = 0;
    public void SetFlag(int counter_) { flagCounter = counter_; }
    public void MinusFlag() { --flagCounter; UpdateCounter(flagCounterText, flagCounter); }
    public void PlusFlag() { ++flagCounter; UpdateCounter(flagCounterText, flagCounter); }
    private int cellCounter = 0;
    public void SetCell(int counter_) { cellCounter = counter_; }
    public void MinusCell() { --cellCounter; UpdateCounter(cellCounterText, cellCounter); }
    public void PlusCell() { ++cellCounter; UpdateCounter(cellCounterText, cellCounter); }

    [SerializeField] private TextMeshProUGUI mineCounterText;
    [SerializeField] private TextMeshProUGUI flagCounterText;
    [SerializeField] private TextMeshProUGUI cellCounterText;
    public int GetMineAmount()
    {
        if (mineCounter <= 0)
        {
            int mines;

            if (int.TryParse(interfaceLowerScript.mineAmountText.text, out mines))
            {
                SetMine(mines);

                return mines;
            }
            else
            {
                Debug.Log("Это не цифры");

                return -1;
            }
        }
        else
        {
            return mineCounter;
        }
    }
    [SerializeField] private TMP_InputField cellAmountText;
    public int GetCellAmount()
    {
        if (cellCounter <= 0)
        {
            int cells_;

            if (int.TryParse(cellAmountText.text, out cells_))
            {
                SetCell(cells_);

                return cells_;
            }
            else
            {
                Debug.Log("Это не цифры");

                return -1;
            }
        }
        else
        {
            return cellCounter;
        }
    }
    public int GetFlagsAmount()
    {
        return flagCounter;
    }

    private void UpdateCounter(TextMeshProUGUI text_, int counter)
    {
        text_.text = counter.ToString();
        counterScript.UpdateEvent();
    }

    public void Start()
    {
        cells = new List<Cell_Class>();
        cellsErase = new List<Cell_Class>();

        GameModeUpdate(GameMode.Pause);
    }

    public void OpenCell(int x_, int y_)
    {
        int mineCounter_ = 0;

        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if ((dx == 0 && dy != 0) || (dx != 0 && dy == 0) || (dx != 0 && dy != 0))
                {
                    Cell_Class cell = FindCell(x_ + dx, y_ + dy);
                    if (cell != null && cell.cell == Cell.Mine)
                    {
                        mineCounter_++;
                    }
                }
            }
        }

        Debug.Log(mineCounter_);

        Cell_Class cell_ = FindCell(x_, y_);
        Instantiate(digEffect, cell_.transform);
        GameObject c = Instantiate(numbers, cell_.transform);
        c.GetComponent<Number_Class>().SetNumber(mineCounter_);

        if (cell_.flag)
        {
            cell_.RigthClick();
        }

        cell_.isOpened = true;
        cellsErase.Add(cell_);
        cells.Remove(cell_);
        MinusCell();

        if (mineCounter_ == 0)
        {
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if ((dx == 0 && dy != 0) || (dx != 0 && dy == 0) || (dx != 0 && dy != 0))
                    {
                        Cell_Class cell = FindCell(x_ + dx, y_ + dy);
                        if (cell != null && !cell.isOpened)
                        {
                            OpenCell(cell.GetCoordinationX(), cell.GetCoordinationY());
                        }
                    }
                }
            }
        }
    }
    public void Pause()
    {
        GameModeUpdate(GameMode.Pause);
    }

    public void GameModeUpdate(GameMode gm)
    {
        gameMode = gm;
        counterScript.UpdateEvent();
        interfaceLowerScript.UpdateEvent();

        if (gm == GameMode.Pause)
        {
            Cell_Class[] cells_ = GameObject.FindObjectsOfType<Cell_Class>();

            for (int i = 0; i < cells_.Length; ++i)
            {
                Destroy(cells_[i].gameObject);
            }

            cells.Clear();

            SetCell(0);
            SetMine(0);
            SetFlag(0);
        }
        else if (gm == GameMode.Game)
        {
            explotionManager.Restart();
        }
        else if (gm == GameMode.GameOver)
        {
            Cell_Class[] cells_ = GameObject.FindObjectsOfType<Cell_Class>();

            for (int i = 0; i < cells_.Length; ++i)
            {
                cells_[i].Block();
            }
        }
    }

    public void CheckWin()
    {
        if (GetCellAmount() == GetMineAmount())
        {
            GameModeUpdate(GameMode.GameWin);
        }
    }
}
