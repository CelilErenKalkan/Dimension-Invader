using System.Collections.Generic;
using UnityEngine;

namespace Game_Management
{
    public class BackgroundMusicManager : MonoBehaviour
    {
        public AudioSource audioSource;
        public List<AudioClip> musicClip;

        [SerializeField] private float volume;

        private void OnEnable()
        {

        }
        
        private void OnDisable()
        {

        }
        
        private void Start()
        {
            PlayMainMenuMusic();
        }
        
        private void SetAudioMod(bool isMuted)
        {
            audioSource.volume = isMuted ? 0 : volume;
        }

        private void PlayInGameMusic()
        {
            audioSource.pitch = 1f / Time.timeScale;
            audioSource.clip = musicClip[1];
            audioSource.Play();
        }

        private void PlayMainMenuMusic()
        {
            audioSource.pitch = 1f / Time.timeScale;
            audioSource.clip = musicClip[0];
            audioSource.Play();
        }
    }
}
