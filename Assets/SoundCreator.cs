
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class SoundCreator : MonoBehaviour
    {
        private static SoundCreator instance;
        [SerializeField] private GameObject soundPrefab;
        Coroutine pitchRutine;

        public List<Sound> soundsPool = new List<Sound>();
        private int iterator;
        private void Awake()
        {

            if (instance != null)
            {
                if (instance != this)
                {
                    Destroy(this);
                }
            }
            else
            {
                DontDestroyOnLoad(this);
                instance = this;
            }


        }

        public static Sound Create(Clip clip)
        {
            Sound sound = GetFreeSound();
            sound.Init(clip);
            return sound;
        }
        private static Sound GetFreeSound()
        {
            foreach (var item in instance.soundsPool)
            {
                if (item.free) return item;
            }
            var s = Instantiate(instance.soundPrefab, instance.transform).GetComponent<Sound>();
            instance.soundsPool.Add(s);
            s.gameObject.name += " " + instance.iterator++;
            return s;
        }
        public static void PitchEffect(float duration = 3)
        {
            if (instance.pitchRutine != null) instance.StopCoroutine(instance.pitchRutine);
   
        }
     
    }
}