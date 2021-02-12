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
        private StudioEventEmitter mPreviousEmitter = null;

        public void PlayCorrectNote(bool keepLastAlive = false)
        {
            if (mEmitterQueue.Count > 3)
            {
                var oldest = mEmitterQueue.Dequeue();
                oldest.Stop();
                Destroy(oldest.gameObject);
            }

            if (mPreviousEmitter != null && !keepLastAlive)
            {
                mPreviousEmitter.EventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                mPreviousEmitter = null;
            }
            
            var emitter = Instantiate(mEmitterPrefab);
            emitter.Play();
            emitter.SetParameter("magnet", mMagnetIndex);
            emitter.EventInstance.triggerCue();
            mPreviousEmitter = emitter;
            mEmitterQueue.Enqueue(emitter);
            ++mMagnetIndex;
        }

        public void ResetTrack()
        {
            mMagnetIndex = 0;
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