using TMPro;
using UnityEngine;

public class Number_Class : MonoBehaviour
{
    [SerializeField] private int number;
    [SerializeField] private TextMeshProUGUI text;

    public void SetNumber(int number_)
    {
        number = number_;
        text.text = number_.ToString();

        switch (number_)
        {
            case (0):
            {
                text.color = new Color(255, 255, 255);
                break;
            }
            case (1):
                {
                    text.color = new Color(0, 0, 180);
                    break;
                }
            case (2):
                {
                    text.color = new Color(0, 180, 0);
                    break;
                }
            case (3):
                {
                    text.color = new Color(180, 0, 0);
                    break;
                }
            case (4):
                {
                    text.color = new Color(0, 0, 140);
                    break;
                }
            case (5):
                {
                    text.color = new Color(180, 0, 180);
                    break;
                }
            case (6):
                {
                    text.color = new Color(0, 180, 180);
                    break;
                }
            case (7):
                {
                    text.color = new Color(0, 0, 0);
                    break;
                }
            case (8):
                {
                    text.color = new Color(255, 255, 255);
                    break;
                }
        }
    }
}
