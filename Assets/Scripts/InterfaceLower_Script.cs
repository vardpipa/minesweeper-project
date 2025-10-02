using UnityEngine;
using TMPro;

public class InterfaceLower_Script : MonoBehaviour
{
    [SerializeField] private GameManager_Script gameManager;
    [SerializeField] private Timer_Script timerScript;
    [SerializeField] private GameObject interfacePause;
    [SerializeField] private GameObject interfaceGame;

    public TMP_InputField cellAmountText;
    [SerializeField] private TextMeshProUGUI cellTotalAmountText;
    public TMP_InputField mineAmountText;
    [SerializeField] private TextMeshProUGUI cellWarningText;
    [SerializeField] private TextMeshProUGUI mineWarningText;

    void Update()
    {
        if (gameManager.gameMode == GameMode.Pause)
        {
            if (cellAmountText.text.Length > 0)
            {
                int total_ = 0;
                int.TryParse(cellAmountText.text, out total_);

                if (total_ != 0 || (total_ == 0 && cellAmountText.text == "0"))
                {
                    cellTotalAmountText.text = "(" + total_ * total_ + ")";
                }
                else
                {
                    cellTotalAmountText.text = "(???)";
                }
            }
            else
            {
                cellTotalAmountText.text = "";
            }
        }
    }

    public void UpdateEvent()
    {
        if (gameManager.gameMode == GameMode.Pause)
        {
            interfacePause.SetActive(true);
            interfaceGame.SetActive(false);

            timerScript.Stop();
        }
        else if (gameManager.gameMode == GameMode.Game)
        {
            interfacePause.SetActive(false);
            interfaceGame.SetActive(true);

            timerScript.Play();
        }
        else if (gameManager.gameMode == GameMode.GameOver || gameManager.gameMode == GameMode.GameWin)
        {
            timerScript.Stop();
        }
    }

    public bool Validation()
    {
        int cellAmount_;
        int.TryParse(cellAmountText.text, out cellAmount_);

        if (cellAmount_ <= 0)
        {
            cellWarningText.enabled = true;
            mineWarningText.enabled = false;
            return false;
        }
        else
        {
            int mineAmount_;
            int.TryParse(mineAmountText.text, out mineAmount_);

            if (mineAmount_ == 0 || mineAmount_ > cellAmount_*cellAmount_)
            {
                cellWarningText.enabled = false;
                mineWarningText.enabled = true;

                return false;
            }
        }

        cellWarningText.enabled = false;
        mineWarningText.enabled = false;

        return true;
    }
}
