using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RightClickButton : MonoBehaviour, IPointerClickHandler
{
    private Button button;
    [SerializeField] private UnityEvent OnRightClick;

    void Start()
    {
        button = GetComponent<Button>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick.Invoke();
        }
    }
}