using System.Linq;
using Helpers.EditorHelper;
using Helpers.Modules;
using UnityEngine;

namespace Audio
{
    [Module(PrefabPath = "AudioManager")]
    public class AudioManager : MonoBehaviour, IModule
    {
        [SerializeField]
        private EditorHelper.PairMusicClip[] _clips;
        private AudioSource _source;

        public bool IsInitialized { get; set; }
        public bool CurrentMusic { get; private set; }

        public void Initialize()
        {
            _source = gameObject.GetComponent<AudioSource>();
            Play(MusicType.MainMenu);

            IsInitialized = true;
        }

        public void Play(MusicType type)
        {
            var clip = _clips.FirstOrDefault(p => p.Item0 == type);
            if (clip == null)
            {
                return;
            }
            _source.Stop();
            _source.clip = clip.Item1;
            _source.Play();
        }

        public void SetVolume(float volume)
        {
            _source.volume = volume;
        }

        public void Dispose()
        {
            IsInitialized = false;
        }
    }
}
