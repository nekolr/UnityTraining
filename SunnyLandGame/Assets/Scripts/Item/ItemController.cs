using UnityEngine;

namespace Item
{
    public class ItemController : MonoBehaviour
    {
        private const int Score = 100;

        private BoxCollider2D _boxCollider2D;
        private AudioSource _audioSource;
        private Animator _animator;

        private void Awake()
        {
            _boxCollider2D = GetComponent<BoxCollider2D>();
            _audioSource = GetComponent<AudioSource>();
            _animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                // 播放动画
                _animator.Play("Cherry_Collect");

                // 碰撞体消失
                _boxCollider2D.enabled = false;
                
                // 更新 UI
                GameController.Instance.AddScore(Score);
                
                // 播放收集声音
                _audioSource.Play();

                // 0.2 秒后销毁物体
                Destroy(gameObject, 0.2f);
            }
        }
    }
}
