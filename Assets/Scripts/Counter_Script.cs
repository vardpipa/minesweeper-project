using UnityEngine;
using TMPro;

public class Counter_Script : MonoBehaviour
{
    [SerializeField] private GameManager_Script gameManager;
    [SerializeField] private GameObject counterInterface;
    [SerializeField] private GameObject winInterface;
    [SerializeField] private GameObject loseInterface;

    [SerializeField] private int counterMine;
    [SerializeField] private int counterFlags;
    [SerializeField] private int counterCells;

    [SerializeField] private TextMeshProUGUI counterMineText;
    [SerializeField] private TextMeshProUGUI counterFlagsText;
    [SerializeField] private TextMeshProUGUI counterCellsText;

    public void UpdateEvent()
    {
        if (gameManager.gameMode == GameMode.Pause)
        {
            counterInterface.SetActive(false);
            loseInterface.SetActive(false);
            winInterface.SetActive(false);
        }
        else if (gameManager.gameMode == GameMode.Game)
        {
            counterInterface.SetActive(true);
            GetInfo();
        }
        else if (gameManager.gameMode == GameMode.GameOver)
        {
            counterInterface.SetActive(false);
            loseInterface.SetActive(true);
        }
        else if (gameManager.gameMode == GameMode.GameWin)
        {
            counterInterface.SetActive(false);
            winInterface.SetActive(true);
        }
    }

    void GetInfo()
    {
        counterFlags = gameManager.GetFlagsAmount();
        counterMine = gameManager.GetMineAmount() - counterFlags;
        counterCells = gameManager.GetCellAmount();

        counterFlagsText.text = counterFlags.ToString();
        counterMineText.text = counterMine.ToString();
        counterCellsText.text = counterCells.ToString();

        if (counterMine < 0)
        {
            counterMineText.color = Color.red;
        }
        else
        {
            counterMineText.color = Color.white;
        }
    }
}
