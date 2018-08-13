using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio;
using NAudio.Wave;

namespace ToolTestIPCamera_Ver1.Function
{
    public class Speaker
    {
        IWavePlayer waveOutDevice = null;
        AudioFileReader audioFileReader = null;
        
        public Speaker() {
            waveOutDevice = new WaveOut();
            audioFileReader = new AudioFileReader("Sounds//soundtest.wav");
            waveOutDevice.Init(audioFileReader);
        }

        public bool PlaySound(ref string _message) {
            try {
                waveOutDevice.Play();
                _message = "Play sound OK.";
                return true;
            } catch (Exception ex) {
                _message = ex.ToString();
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

        public bool PlayStop(ref string _message) {
            try {
                waveOutDevice.Stop();
                audioFileReader.Dispose();
                waveOutDevice.Dispose();
                _message = "Stop sound OK.";
                return true;
            }
            catch (Exception ex) {
                _message = ex.ToString();
                return false;
            }
        }

    }
}
