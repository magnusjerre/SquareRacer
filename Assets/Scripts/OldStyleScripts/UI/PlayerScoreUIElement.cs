using UnityEngine;
using UnityEngine.UI;

namespace Jerre
{
    public class PlayerScoreUIElement : MonoBehaviour
    {
        public int numberInLine;

        private Text scoreText;
        private Image backgroundImage;

        // Use this for initialization
        void Start()
        {
            scoreText = GetComponentInChildren<Text>();
            backgroundImage = GetComponent<Image>();
            Reset(0, 10);
            SetNumberInLine(numberInLine);
        }

        public void Reset(int score, int maxScore)
        {
            SetScore(score, maxScore);
        }

        public void SetScore(int score, int maxScore)
        {
            scoreText.text = string.Format("{0} / {1}", score, maxScore);
        }

        public void SetBackgroundColor(Color color)
        {
            backgroundImage.color = color;
        }

        public void SetNumberInLine(int numberZeroIndexed)
        {
            if (numberZeroIndexed < 0)
            {
                return;
            }
            this.numberInLine = numberZeroIndexed;
            var width = backgroundImage.rectTransform.rect.width;
            backgroundImage.rectTransform.anchoredPosition = new Vector2(numberZeroIndexed * width, 0);
        }
    }
}
