using UnityEngine;
using UnityEngine.UI;

public class Flag_Class : MonoBehaviour
{
    [SerializeField] private Cell_Class cellOwner;
    [SerializeField] private Image flagSprite;
    [SerializeField] private Button flagButton;
    private GameManager_Script gameManager;

    void Start()
    {
        gameManager = GameObject.FindFirstObjectByType<GameManager_Script>();
    }

    public void Activater()
    {
        cellOwner.flag = true;
        flagSprite.enabled = true;
        flagButton.enabled = true;

        gameManager.PlusFlag();
    }

    public void Deactivater()
    {
        cellOwner.flag = false;
        flagSprite.enabled = false;
        flagButton.enabled = false;
        
        gameManager.MinusFlag();
    }
}
