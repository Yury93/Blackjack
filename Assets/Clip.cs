using UnityEngine;

namespace Audio
{
    [System.Serializable]
    public class Clip
    {
        public AudioClip clip;
        public float volume = 0.5f;
        public AnimationCurve volumeCurv = AnimationCurve.EaseInOut(1, 1, 1, 1);
        [Range(0,1)]
        public float startFrom = 0;

        
    }
}