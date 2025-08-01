using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game_Management
{
    public class AudioManager : MonoBehaviour
    {
        private Pool _pool;
        public List<AudioClip> soundList;

        private void OnEnable()
        {

        }
        
        private void OnDisable()
        {

        }

        private void Start()
        {
            _pool = Pool.Instance;
        }

        private void OnButtonTapped()
        {
            PlaySound(0);
        }

        private void PlaySound(int index)
        {
            float time = soundList[index].length + 0.1f;
            var audioObject = _pool.SpawnObject(transform.position, PoolItemType.AudioSource, null, time);
            if (audioObject.TryGetComponent(out AudioSource audioSource))
            {
                audioSource.pitch = 1f / Time.timeScale;
                audioSource.clip = soundList[index];
                audioSource.Play();
            }
        }
    }
    
}
