using UnityEngine;
using UnityEngine.UI;

namespace OldScripts
{
    public class GameController : MonoBehaviour
    {
        public int totalScore;

        public Text scoreText;

        public static GameController Instance;

        // Start is called before the first frame update
        void Start()
        {
            Instance = this;
        }

        public void UpdateTotalScore()
        {
            this.scoreText.text = totalScore.ToString();
        }
    }
}