using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using SpeechLib;

namespace tnpd.Controllers
{
    public class VoiceController : Controller
    {
        //
        // GET: /Voice/

        public ActionResult Index()
        {
            // 生成音频文件
            var code = "Love";
            SpVoice voice = new SpVoice();
            string voiceStr = "";
            for (int i = 0; i < code.Length; i++)
            {
                voiceStr += code[i] + " ";
            }
            voice.Volume = 100;
            voice.Rate = 1;
            //voice.Speak(voiceStr, SpeechVoiceSpeakFlags.SVSFlagsAsync); //播放音频文件
            //voice.Speak(voiceStr, SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak);
            SpeechStreamFileMode SpFileMode = SpeechStreamFileMode.SSFMCreateForWrite;
            SpFileStream SpFileStream = new SpFileStream();
            string savePath = Server.MapPath("~/wav/") + code + ".wav";
            SpFileStream.Open(savePath, SpFileMode, false);

            voice.AudioOutputStream = SpFileStream;
            voice.Speak(voiceStr, SpeechVoiceSpeakFlags.SVSFlagsAsync);
            voice.WaitUntilDone(Timeout.Infinite);
            SpFileStream.Close();
            return Content("Success");
        }

    }
}
