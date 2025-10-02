using System.Linq;
using UnityEngine;

public class ExplotionManager_Script : MonoBehaviour
{
    [SerializeField] private GameObject mine;
    private Cell_Class[] cells;
    private Cell_Class cellFirst;
    private float[] timers;

    private bool activate;

    void Update()
    {
        if (activate)
        {
            for (int i = 0; i < timers.Length; ++i)
            {
                if (timers[i] >= 0)
                {
                    timers[i] -= Time.deltaTime;

                    if (timers[i] <= 0)
                    {
                        if (cells[i] != null && cells[i] != cellFirst)
                        {
                            Instantiate(mine, cells[i].transform);
                        }
                    }
                }
            }
        }
    }

    public void Activate(Cell_Class cf)
    {
        activate = true;
        cellFirst = cf;
        
        Instantiate(mine, cf.transform);
    }

    public void Restart()
    {
        activate = false;
        cellFirst = null;
        cells = GameObject.FindObjectsOfType<Cell_Class>().Where(x => x.cell == Cell.Mine).ToArray();

        timers = new float[cells.Length];

        for (int i = 0; i < cells.Length; ++i)
        {
            timers[i] = 0.5f + Random.Range(0f, 1.5f);
        }
    }
}
