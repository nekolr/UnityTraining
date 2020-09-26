using System;
using System.Collections;
using UnityEngine;

namespace Utils
{
    /// <summary>
    /// 延迟调用类
    /// 
    /// 使用间接的方式实现非 Mono 类的延迟调用功能
    /// </summary>
    public static class DelayInvoke
    {
        // 定义一个 MonoBehaviour 组件
        private class TaskBehaviour : MonoBehaviour
        {
        }

        private static readonly TaskBehaviour TaskBehaviourInstance;

        // 静态构造函数
        static DelayInvoke()
        {
            // 创建 GameObject
            GameObject gameObject = new GameObject("DelayInvoke");
            // 场景切换不会销毁该对象
            UnityEngine.Object.DontDestroyOnLoad(gameObject);
            // 给 GameObject 添加 TaskBehaviour 组件，这样非 Mono 类也可以使用 Mono 中的一些方法
            TaskBehaviourInstance = gameObject.AddComponent<TaskBehaviour>();
        }

        // 开始协程，非 Mono 类可调用
        public static Coroutine StartCoroutine(IEnumerator routine)
        {
            if (routine == null)
                return null;
            return TaskBehaviourInstance.StartCoroutine(routine);
        }

        // 终止协程
        public static void StopCoroutine(ref Coroutine routine)
        {
            if (routine != null)
            {
                TaskBehaviourInstance.StopCoroutine(routine);
                routine = null;
            }
        }

        // 延迟调用
        public static Coroutine DelayInvokeBySecond(Action action, float delaySeconds)
        {
            return action == null
                ? null
                : TaskBehaviourInstance.StartCoroutine(StartDelayInvokeBySecond(action, delaySeconds));
        }

        // 延迟调用
        public static Coroutine DelayInvokeByFrame(Action action, int delayFrames)
        {
            return action == null
                ? null
                : TaskBehaviourInstance.StartCoroutine(StartDelayInvokeByFrame(action, delayFrames));
        }

        private static IEnumerator StartDelayInvokeBySecond(Action action, float delaySeconds)
        {
            if (delaySeconds > 0)
                yield return new WaitForSeconds(delaySeconds);
            else
                yield return null;
            action?.Invoke();
        }

        private static IEnumerator StartDelayInvokeByFrame(Action action, int delayFrames)
        {
            if (delayFrames > 1)
            {
                for (var i = 0; i < delayFrames; i++)
                {
                    yield return null;
                }
            }
            else
                yield return null;

            action?.Invoke();
        }
    }
}