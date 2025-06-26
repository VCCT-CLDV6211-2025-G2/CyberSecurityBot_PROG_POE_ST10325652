/*
 * Michaela Ferraris
 * ST10325652
 * PROG6211 POE part 1
 */
using System;
using System.IO;
using System.Media;
using System.Threading.Tasks;


namespace CyberSecurityBot_PROG_POE_ST10325652.CoreLogic
{
    class VoiceGreeting
    {
        //Path to the voice greeting audio file 
        private readonly string audioFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "ttsMP3.com_VoiceText_2025-3-17_17-29-10.wav");

        //---------------------------------------------------------------------------------------------------------------------//

        //Method to play the audio voice greeting asynchronously on a seperate thread 
        public Task PlayAudioAsync()
        {
            return Task.Run(() =>
            {
                if (!File.Exists(audioFilePath))
                    throw new FileNotFoundException($"Audio file not found: {audioFilePath}", audioFilePath);

                try
                {
                    using (var player = new SoundPlayer(audioFilePath))
                    {
                        player.PlaySync();
                    }
                    
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"Playback error: {ex.Message}", ex);
                }
            });
        }
        //--------------------------------------------------------------------------------------------------------------------------------------//

    }
}

 
