using EventSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class EventView : MonoBehaviour
    {
        [SerializeField] private Image eventImage;
        [SerializeField] private TMP_Text eventName;
        [SerializeField] private TMP_Text eventText;
        [SerializeField] private TMP_Text firstOptionText;
        [SerializeField] private TMP_Text secondOptionText;
        [SerializeField] private Button firstOptionButton;
        [SerializeField] private Button secondOptionButton;
        [SerializeField] private Button backButton;
        [SerializeField] private EventManager eventManager;

        private void Start()
        {
            BindButtons();
            eventManager.OnEventChange += UpdateEvent;
            UpdateEvent();
        }

        private void BindButtons()
        {
            firstOptionButton.onClick.AddListener(() => eventManager.ChooseEventOption(0));
            secondOptionButton.onClick.AddListener(() => eventManager.ChooseEventOption(1));
            backButton.onClick.AddListener(eventManager.TurnBack);
        }

        private void UpdateEvent()
        {
            eventImage.sprite = eventManager.CurrentEventNode.EventSprite;
            eventName.text = eventManager.CurrentEventNode.EventName;
            eventText.text = eventManager.CurrentEventNode.EventText;
            firstOptionText.text = eventManager.CurrentEventNode.FirstOptionText;
            secondOptionText.text = eventManager.CurrentEventNode.SecondOptionText;
        }
    }
}