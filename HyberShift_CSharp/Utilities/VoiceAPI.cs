using System;
using System.IO;
using System.Media;
using System.Threading;
using System.Windows.Threading;
using NAudio.Wave;
using Newtonsoft.Json.Linq;
using Quobject.SocketIoClientDotNet.Client;

namespace HyberShift_CSharp.Utilities
{
    // VoiceAPI for voice chat function
    // Singleton class
    public class VoiceAPI
    {
        private static VoiceAPI instance;
        private byte[] dataArray;
        private readonly string path = Environment.CurrentDirectory + "\\buffer.wav"; //path to buffer
        private Thread recThread;
        private readonly Socket socket;

        private WaveIn sourceStream;
        private DispatcherTimer timer;
        private WaveFileWriter waveWriter;

        //getter and setter
        //public string ID { get; set; }

        // constructor
        public VoiceAPI()
        {
            socket = SocketAPI.GetInstance().GetSocket();
        }

        public static VoiceAPI GetInstance()
        {
            if (instance == null)
                instance = new VoiceAPI();
            return instance;
        }

        //public VoiceAPI(string id): this()
        //{
        //    ID = id;
        //}

        #region Send

        public void Send(string id)
        {
            if (timer == null) timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(500);
            timer.Tick += (sender, e) =>
            {
                Dispose();
                SendBytes(id);
            };
            Recordwav();
        }

        public void StopSending()
        {
            Dispose();
        }

        private void Recordwav()
        {
            sourceStream = new WaveIn();
            var devicenum = 0;

            for (var i = 0; i < WaveIn.DeviceCount; i++)
                if (WaveIn.GetCapabilities(i).ProductName.Contains("icrophone"))
                    devicenum = i;
            sourceStream.DeviceNumber = devicenum;
            sourceStream.WaveFormat = new WaveFormat(22000, WaveIn.GetCapabilities(devicenum).Channels);
            sourceStream.DataAvailable += sourceStreamDataAvailable;

            waveWriter = new WaveFileWriter(path, sourceStream.WaveFormat);

            sourceStream.StartRecording();

            timer.Start();
        }

        private void sourceStreamDataAvailable(object sender, WaveInEventArgs e)
        {
            if (waveWriter == null) return;

            waveWriter.Write(e.Buffer, 0, e.BytesRecorded);
            waveWriter.Flush();
        }

        //private void TimerTick(object sender, EventArgs e)
        //{
        //    this.Dispose();
        //    SendBytes();
        //}

        private void SendBytes(string id)
        {
            // if id=null, emit to "public" client
            dataArray = File.ReadAllBytes(path);
            // TO-DO: add JSONObject here
            if (id == null)
            {
                //socket.Emit("stream_audio", dataArray);
                Debug.LogOutput(" stream audio: room id is null");
            }
            else
            {
                var audio = new JObject();
                audio.Add("id", id);
                audio.Add("content", dataArray);
                socket.Emit("stream_audio", audio);
            }

            Recordwav();
        }

        private void Dispose()
        {
            timer.Stop();
            if (sourceStream != null)
            {
                sourceStream.StopRecording();
                sourceStream.Dispose();
            }

            if (waveWriter != null) waveWriter.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Receive

        public void Receive()
        {
            recThread = new Thread(HandleReceive);
            recThread.Start();
        }

        public void StopReceiving()
        {
            socket.Off("stream_audio");
        }

        private void HandleReceive()
        {
            socket.On("stream_audio", arg =>
            {
                Debug.LogOutput("recived audio");
                var json = (JObject) arg;
                var data = (byte[]) json.GetValue("content");
                Stream stream = new MemoryStream(data);
                var sp = new SoundPlayer(stream);
                sp.Play();
            });
        }

        #endregion
    }
}