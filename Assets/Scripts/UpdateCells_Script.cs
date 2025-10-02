using UnityEngine;

public class UpdateCells_Script : MonoBehaviour
{
    [SerializeField] private GameManager_Script gameManager;
    [SerializeField] private GameObject cells;
    public RectTransform filldZone;

    public void NewGame()
    {
        if (gameManager.interfaceLowerScript.Validation())
        {
            Fill();
            gameManager.GameModeUpdate(GameMode.Game);
        }
    }

    void Fill()
    {
        gameManager.Clear();

        int size = gameManager.GetCellAmount();
        if (size <= 0)
        {
            return;
        }

        float scale = 10 / (float)size;

        float y = 50;

        for (int i = 0; i < size; ++i)
        {
            float x = -50;
            for (int j = 0; j < size; ++j)
            {
                GameObject cell = Instantiate(cells, filldZone.transform);

                cell.GetComponent<Cell_Class>().SetCoordinations(i, j);
                cell.transform.localPosition = new Vector2(x, y);
                cell.transform.localScale = new Vector2(cell.transform.localScale.x * scale, cell.transform.localScale.y * scale);

                gameManager.AddCell(cell.GetComponent<Cell_Class>());

                x += scale * 10;
            }

            y -= scale * 10;
        }

        gameManager.SetCell(size * size);
        gameManager.SetCellsToMine();
    }
}
