
using UnityEngine;
using DG.Tweening;
namespace Audio
{
    public class Sound : MonoBehaviour
    {
        private AudioSource source;
        private Clip clip;
        private float startTime;
        public bool voice = false;
        public bool IsPlaying { get => source.isPlaying; }
        public bool free { get; private set; }
        private bool slowStopping;
        private void Awake()
        {
            free = true;
          //  if (voice)
            {
                source = GetComponent<AudioSource>();
            }
        }
        private void Update()
        {
            if (!source.isPlaying && !voice)
            {
                free = true;
                gameObject.SetActive(false);
            }
            if (slowStopping) return;
            if (source.isPlaying)
            {
                SetCurveVolume();
            }
        }

        private void SetCurveVolume()
        {
            if (clip == null) return;
            if (clip.clip == null) return;
            var time = Time.time - startTime;
            var eval = clip.volumeCurv.Evaluate(time / (clip.clip.length - clip.startFrom * clip.clip.length));
        }
        public void SlowStopPlay()
        {
            source.DOFade(0, 1).OnComplete(() =>
             {
                 slowStopping = false;
                 source.Stop();
             });
        }
        public void StopPlay()
        {
            source.Stop();
        }
        public Sound Init(Clip clip)
        {
            if (clip.clip == null) return null;
            free = false;
            this.clip = clip;

            startTime = Time.time;
            if (source == null) source = GetComponent<AudioSource>();

            source.time = 0;//кастыль, ошибку выбивало
            source.Play();//кастыль, ошибку выбивало
            source.Stop();//кастыль, ошибку выбивало

            source.clip = clip.clip;
       

            gameObject.SetActive(true);
            float t = source.clip.length * clip.startFrom;
          //  if (t < 0.01f) t = 0.01f;
            source.time = t;
            source.Play();

          
            return this;


        }

        public void PlayVoice(Clip clip)
        {
            this.clip = clip;
            source.clip = clip.clip;
     
            source.Play();

        }
    
    
      

       
    }
}