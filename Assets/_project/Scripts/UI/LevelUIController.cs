using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PointAndClick.UI 
{
    public class LevelUIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _hamburguerNumberTMP;

        [SerializeField] private Button _decreaseSpeedButton;
        [SerializeField] private Button _increaseSpeedButton;

        private void OnEnable()
        {
            GameEvents.OnScoreUpdateEvent += UpdateCollectiblesInfo;

            _decreaseSpeedButton.onClick.AddListener(UIEvents.NotifyDecreaseSpeedButtonClicked);
            _increaseSpeedButton.onClick.AddListener(UIEvents.NotifyIncreaseSpeedButtonClicked);
        }

        private void OnDisable()
        {
            GameEvents.OnScoreUpdateEvent -= UpdateCollectiblesInfo;   

            _decreaseSpeedButton.onClick.RemoveListener(UIEvents.NotifyDecreaseSpeedButtonClicked);
            _increaseSpeedButton.onClick.RemoveListener(UIEvents.NotifyIncreaseSpeedButtonClicked);
        }

        private void UpdateCollectiblesInfo(int score) 
        {
            _hamburguerNumberTMP.text = score.ToString();
        }
    }
}
