using System.Collections.Generic;
using UnityEngine;
using FMOD;
using FMODUnity;

namespace Elephantile
{
    public class SoundTrack : MonoBehaviour
    {
        [SerializeField] private StudioEventEmitter mEmitterPrefab;
        private int mMagnetIndex = 0;
        private bool mStartedPlaying = false;
        private Queue<StudioEventEmitter> mEmitterQueue = new Queue<StudioEventEmitter>();

        public void PlayCorrectNote()
        {
            if (mEmitterQueue.Count > 3)
            {
                var oldest = mEmitterQueue.Dequeue();
                oldest.Stop();
                Destroy(oldest.gameObject);
            }

            var emitter = Instantiate(mEmitterPrefab);
            ++mMagnetIndex;
            emitter.Play();
            emitter.SetParameter("magnet", mMagnetIndex);
            emitter.EventInstance.triggerCue();
            
            mEmitterQueue.Enqueue(emitter);
        }

        public void ResetTrack()
        {
            // if (mEmitterPrefab == null)
            // {
            //     mStartedPlaying = false;
            // }
            // else
            // {
            //     Destroy(mEmitter.gameObject);
            //     mEmitter.EventInstance.start();
            //     mEmitter = Instantiate(mEmitterPrefab);
            // }
        }

        public void Stop()
        {
            while (mEmitterQueue.Count > 0)
            {
                var emitter = mEmitterQueue.Dequeue();
                emitter.EventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                Destroy(emitter.gameObject);
            }
            ResetTrack();
        }
    }
}