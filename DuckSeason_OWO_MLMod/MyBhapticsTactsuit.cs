using bHapticsLib;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace MyBhapticsTactsuit
{
    public class TactsuitVR
    {
        public bool suitDisabled = true;
        public bool systemInitialized = false;

        public bool armsConnected = false;
        //private static ManualResetEvent HeartBeat_mrse = new ManualResetEvent(false);
        public Dictionary<String, FileInfo> FeedbackMap = new Dictionary<String, FileInfo>();

        private static RotationOption defaultRotationOption = new RotationOption(0.0f, 0.0f);

        /// <summary>
        /// Writes a sensation to the owo interpreter
        /// </summary>
        /// <param name="key"></param>
        /// <param name="altKey"></param>
        /// <param name="scaleOption"></param>
        /// <param name="rotationOption"></param>
        public static void WriteRegisteredToOwo(string key, string altKey = null, ScaleOption scaleOption = null, RotationOption rotationOption = null)
        {
            if (scaleOption == null)
                scaleOption = new ScaleOption();

            if (rotationOption == null) 
                rotationOption = new RotationOption();

            var outStr = "[" + DateTime.UtcNow.Ticks + "] " + key + ";;" + altKey + ";;" + scaleOption.ToString() + ";;" + rotationOption.ToString();
            var outFile = Directory.GetCurrentDirectory() + "\\Mods\\owo\\playsense.log";

            var hasWritten = false;

            while (!hasWritten)
            {
                try
                {
                    File.AppendAllText(outFile, "\r\n" + outStr);

                    hasWritten = true;
                } catch (System.IO.IOException ex)
                {
                    Thread.Sleep(10);
                }
            }
        }

        public TactsuitVR()
        {
            LOG("Deleting previous sense file, if it exists..");

            var senseFile = Directory.GetCurrentDirectory() + "\\Mods\\owo\\playsense.log";
            if (File.Exists(senseFile))
                File.Delete(senseFile);

            LOG("Starting HeartBeat thread...");
            Thread HeartBeatThread = new Thread(HeartBeatFunc);
            HeartBeatThread.Start();
        }

        public void HeartBeatFunc()
        {
            while (true)
            {
                Thread.Sleep(1000);
                //HeartBeat_mrse.WaitOne();

                WriteRegisteredToOwo("HeartBeat");
            }
        }

        public void LOG(string logStr)
        {
            MelonLogger.Msg(logStr);
        }

        public void PlaybackHaptics(String key, float intensity = 1.0f, float duration = 1.0f)
        {
            ScaleOption scaleOption = new ScaleOption(intensity, duration);
            WriteRegisteredToOwo(key, key, scaleOption, defaultRotationOption);
        }

        public void PlayBackHit(String key, float xzAngle, float yShift)
        {
            ScaleOption scaleOption = new ScaleOption(1f, 1f);
            RotationOption rotationOption = new RotationOption(xzAngle, yShift);
            WriteRegisteredToOwo(key, key, scaleOption, rotationOption);
        }

        public void GunRecoil(bool isRightHand, float intensity = 1.0f, bool twoHanded = false, bool supportHand = false)
        {
            float duration = 1.0f;
            var scaleOption = new ScaleOption(intensity, duration);
            var rotationFront = new RotationOption(0f, 0f);
            string postfix = "_L";
            string otherPostfix = "_R";
            if (isRightHand) { postfix = "_R"; otherPostfix = "_L"; }
            string keyArm = "Recoil" + postfix;
            string keyVest = "RecoilVest" + postfix;
            string keyHands = "RecoilHands" + postfix;
            string keyArmOther = "Recoil" + otherPostfix;
            string keyVestOther = "RecoilVest" + otherPostfix;
            string keyHandsOther = "RecoilHands" + otherPostfix;
            WriteRegisteredToOwo(keyHands, keyHands, scaleOption, rotationFront);
            WriteRegisteredToOwo(keyArm, keyArm, scaleOption, rotationFront);
            WriteRegisteredToOwo(keyVest, keyVest, scaleOption, rotationFront);
            if (twoHanded || supportHand)
            {
                WriteRegisteredToOwo(keyHandsOther, keyHandsOther, scaleOption, rotationFront);
                WriteRegisteredToOwo(keyArmOther, keyArmOther, scaleOption, rotationFront);
            }
            if (twoHanded) WriteRegisteredToOwo(keyVestOther, keyVestOther, scaleOption, rotationFront);
        }

        public void SwordRecoil(bool isRightHand, float intensity = 1.0f)
        {
            float duration = 1.0f;
            var scaleOption = new ScaleOption(intensity, duration);
            var rotationFront = new RotationOption(0f, 0f);
            string postfix = "_L";
            if (isRightHand) { postfix = "_R"; }
            string keyArm = "Sword" + postfix;
            string keyVest = "SwordVest" + postfix;
            string keyHands = "RecoilHands" + postfix;
            WriteRegisteredToOwo(keyHands, keyHands, scaleOption, rotationFront);
            WriteRegisteredToOwo(keyArm, keyArm, scaleOption, rotationFront);
            WriteRegisteredToOwo(keyVest, keyVest, scaleOption, rotationFront);
        }

        public void StartHeartBeat()
        {
            //HeartBeat_mrse.Set();
        }

        public void StopHeartBeat()
        {
            //HeartBeat_mrse.Reset();
        }

        public bool IsPlaying(String effect)
        {
            return false; //bHapticsManager.IsPlaying(effect);
        }

        public void StopHapticFeedback(String effect)
        {
            bHapticsManager.StopPlaying(effect);
        }

        public void StopAllHapticFeedback()
        {
            StopThreads();
            foreach (String key in FeedbackMap.Keys)
            {
                bHapticsManager.StopPlaying(key);
            }
        }

        public void StopThreads()
        {
            StopHeartBeat();
        }


    }
}
