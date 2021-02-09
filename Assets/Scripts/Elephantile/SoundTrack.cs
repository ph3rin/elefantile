using UnityEngine;
using FMOD;
using FMODUnity;

namespace Elephantile
{
    public class SoundTrack : MonoBehaviour
    {
        [SerializeField] private StudioEventEmitter mEmitterPrefab;
        private StudioEventEmitter mEmitter;
        private bool mStartedPlaying = false;
        
        private void Awake()
        {
            if (mEmitterPrefab == null)
            {
                mEmitter = GetComponent<StudioEventEmitter>();
            }
            else
            {
                mEmitter = Instantiate(mEmitterPrefab);
            }
        }

        public void PlayCorrectNote()
        {
            if (!mStartedPlaying)
            {
                mEmitter.Play();
                mStartedPlaying = true;
            }
            else
            {
                mEmitter.EventInstance.triggerCue();
            }
        }

        public void PlayCorrectNoteMultiInstrument()
        {
            mEmitter.Play();
        }

        public void ResetTrack()
        {
            if (mEmitterPrefab == null)
            {
                mStartedPlaying = false;
            }
            else
            {
                Destroy(mEmitter.gameObject);
                mEmitter.EventInstance.start();
                mEmitter = Instantiate(mEmitterPrefab);
            }
        }

        public void Stop()
        {
            mEmitter.EventInstance.setPaused(true);
            ResetTrack();
        }
    }
}