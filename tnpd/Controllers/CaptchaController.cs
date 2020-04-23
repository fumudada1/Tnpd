using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using TnpdModels;
using SpeechLib;
using tnpd.Models;

namespace tnpd.Controllers
{
    public class CaptchaController : Controller
    {
        //
        // GET: /Captcha/

        public ActionResult CaptchaImage()
        {
            FileContentResult result;
            Captcha v = new Captcha();
            string code = v.CreateVerifyCode();         // 取随机码
            Session["CheckCode"] = code;
            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            Bitmap image = v.CreateImageCode(code);

            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            result = this.File(ms.GetBuffer(), "image/jpeg");

            ms.Close();

            ms = null;

            image.Dispose();

            image = null;

            string filename = System.Guid.NewGuid().ToString().ToLower();
            Response.Cookies.Add(new HttpCookie("CheckCode", filename));
            // 生成一个guid作为文件名称,这样防止通过cookie直接看到验证码，这样就没意义了呀~
            //by http://www.jizhuni.com
            if (Request.Cookies["CheckCode"] != Request.Cookies["OldCode"])
            {
                if (Request.Cookies["OldCode"] != null)
                {
                    string delPath = Server.MapPath(".") + "\\wav\\" + Request.Cookies["OldCode"].Value + ".wav";
                    if (System.IO.File.Exists(delPath))
                    {
                        System.IO.File.Delete(delPath);
                    }
                    Response.Cookies.Remove("OldCode");
                    Response.Cookies.Add(new HttpCookie("OldCode", filename));
                }
                else
                {
                    Response.Cookies.Add(new HttpCookie("OldCode", filename));
                }
            }
           

            try
            {
                // 生成音频文件
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
                string savePath = Server.MapPath("~/wav/") + filename + ".wav";
                SpFileStream.Open(savePath, SpFileMode, false);

                voice.AudioOutputStream = SpFileStream;
                voice.Speak(voiceStr, SpeechVoiceSpeakFlags.SVSFlagsAsync);
                voice.WaitUntilDone(Timeout.Infinite);
                SpFileStream.Close();
            }
            catch (Exception e)
            {
                return result;
               
            }
          


            return result;
        }

    }
}
