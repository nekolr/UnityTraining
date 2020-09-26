using UnityEngine;

namespace Item
{
    public class ItemController : MonoBehaviour
    {
        private const int Score = 100;

        private SpriteRenderer _spriteRenderer;
        private BoxCollider2D _boxCollider2D;
        private AudioSource _audioSource;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                // 玩家碰撞时，物体消失
                _spriteRenderer.enabled = false;
                
                // 碰撞体消失
                _boxCollider2D.enabled = false;
                
                // 更新 UI
                GameController.Instance.AddScore(Score);
                
                // 播放收集特效和声音
                _audioSource.Play();
                
                // 0.2 秒后销毁物体
                Destroy(gameObject, 0.2f);
            }
        }
    }
}
