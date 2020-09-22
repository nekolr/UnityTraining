using UnityEngine;

namespace OldScripts
{
    public class Saw : MonoBehaviour
    {
        public float moveSpeed = 2;

        public float moveTime = 3;

        private bool directionRight = true;

        private float timer;

        // Update is called once per frame
        void Update()
        {
            if (directionRight)
            {
                // 向右移动
                transform.Translate(moveSpeed * Time.deltaTime * Vector2.right);
            }
            else
            {
                // 向左移动
                transform.Translate(moveSpeed * Time.deltaTime * Vector2.left);
            }

            timer += Time.deltaTime;

            if (timer > moveTime)
            {
                directionRight = !directionRight;
                // reset
                timer = 0;
            }
        }
    }
}