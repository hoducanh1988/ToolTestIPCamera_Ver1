using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio;
using NAudio.Wave;

namespace ToolTestIPCamera_Ver1.Function.Instrument {

    public class Speaker {

        private string SoundPath = @"";
        public IWavePlayer waveOutDevice = null; 
        public AudioFileReader audioFileReader = null;

        public Speaker() {
            waveOutDevice = new WaveOut();
            audioFileReader = new AudioFileReader(SoundPath);
            waveOutDevice.Init(audioFileReader);
        }

        public bool PlaySound() {
            try {
                waveOutDevice.Play();
                return true;
            }
            catch {
                return false;
            }
        }

        public bool IsPlaying() {
            try {
                return waveOutDevice.PlaybackState == PlaybackState.Playing;
            } catch {
                return false;
            }
        }

        //Close Sound
        public bool Close() {
            try {
                waveOutDevice.Stop();
                audioFileReader.Dispose();
                waveOutDevice.Dispose();
                return true;
            } catch {
                return false;
            }
        }

    }
}
