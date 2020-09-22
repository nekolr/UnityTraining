using UnityEngine;

namespace OldScripts
{
    public class FruitsItem : MonoBehaviour
    {
        public GameObject collectedEffect;

        private SpriteRenderer _spriteRenderer;

        private CircleCollider2D _circleCollider2D;

        // 默认一个 100 分
        public int Score = 100;

        // Start is called before the first frame update
        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _circleCollider2D = GetComponent<CircleCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // 碰撞体为玩家
            if (other.gameObject.tag == "Player")
            {
                // 玩家碰撞时，物体消失
                _spriteRenderer.enabled = false;
                // 碰撞体消失
                _circleCollider2D.enabled = false;
            
                // 收集动画激活
                collectedEffect.SetActive(true);

                GameController.Instance.totalScore += Score;
                GameController.Instance.UpdateTotalScore();
            
                // 0.2 秒后物体和动画全部销毁
                Destroy(gameObject, 0.2f);
            }
        }
    }
}