using System;
using System.Diagnostics;
using System.Threading;

namespace RenderMe.Engine
{
    public class FrameLimiter
    {
        private int _frameLimit { get; set; }
        public int FrameLimit
        {
            get
            {
                return _frameLimit;
            }
            set
            {
                _frameLimit = value;
                TargetMs = (float)1000 / value;
            }
        }
        private float TargetMs { get; set; }

        private Stopwatch Stopwatch { get; set; }
        private long LastFrameTime { get; set; }

        private int FPS { get; set; }
        private long LastFPS { get; set; }

        public bool Debug { get; set; }

        public FrameLimiter(int limit, bool debug = false)
        {
            Debug = debug;
            FrameLimit = limit;
            Stopwatch = new Stopwatch();
            Stopwatch.Start();
            LastFrameTime = Stopwatch.ElapsedMilliseconds;
        }

        private void Nothing()
        {

        }

        public bool LimitFrame(float delta)
        {
            long dif = Stopwatch.ElapsedMilliseconds - LastFrameTime;
            if (dif < TargetMs)
            {
                while ((Stopwatch.ElapsedMilliseconds - LastFrameTime) < TargetMs)
                {
                    Nothing();
                }
                return true;
            }

            if (Stopwatch.ElapsedMilliseconds - LastFPS >= 1000)
            {
                if (Debug)
                {
                    Console.WriteLine(FPS);
                }

                LastFPS = Stopwatch.ElapsedMilliseconds;
                FPS = 0;
            }

            LastFrameTime = Stopwatch.ElapsedMilliseconds;
            FPS++;
            return false;
        }
    }
}