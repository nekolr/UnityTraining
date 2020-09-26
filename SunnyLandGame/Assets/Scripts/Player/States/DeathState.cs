﻿using System.Collections;
using MyStateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Player.States
{
    public class DeathState : AbstractState
    {
        private readonly AudioSource _audioSource;
        public DeathState(PlayerEntry playerEntry) : base(playerEntry.StateMachine, playerEntry.StateDictionary)
        {
            _audioSource = playerEntry.GetComponent<AudioSource>();
        }

        public override void Enter()
        {
            _audioSource.Stop();
            // 重新加载当前场景
            DelayInvoke.StartCoroutine(ReloadCurrentScene());
        }

        IEnumerator ReloadCurrentScene()
        {
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}