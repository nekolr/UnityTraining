using UnityEngine;
using UnityEngine.SceneManagement;

namespace OldScripts
{
    public class PlayerDie : MonoBehaviour
    {

        public static PlayerDie Instance;
    
        public bool isDie = false;

        // 闪烁次数
        private int flashNum;

        // 闪烁时间
        private float flashGapTime;
        private bool isDisplay = true;
        private Renderer player;


        // Start is called before the first frame update
        void Start()
        {
            Instance = this;
            player = GetComponent<Renderer>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Spike")
            {
                isDie = true;
            }

            if (other.gameObject.tag == "Saw")
            {
                isDie = true;
            }
        }

        private void Update()
        {
            if (isDie)
            {
                Flash();
            }
        }

        public void Flash()
        {
            flashGapTime += Time.deltaTime;
            // 闪烁时间 0.5 秒
            if (flashGapTime >= 0.1f)
            {
                if (isDisplay)
                {
                    player.enabled = false;
                    isDisplay = false;
                }
                else
                {
                    player.enabled = true;
                    isDisplay = true;
                }

                // reset
                flashGapTime = 0f;
                if (flashNum == 3)
                {
                    // 重新开始
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }

                flashNum++;
            }
        }
    }
}