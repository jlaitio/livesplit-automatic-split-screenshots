using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.SplitScreenshotLibrary
{
    public class SplitScreenshotComponent : LogicComponent {
        public override string ComponentName => "Automatic Split Screenshots";

        public SplitScreenshotComponentSettings Settings { get; set; }
        Boolean splitFlag = false;

        public SplitScreenshotComponent(LiveSplitState state) {
            Settings = new SplitScreenshotComponentSettings();
            state.OnSplit += handleSplit;
        }

        private void takeScreenshot() {
            var proc = Process.GetCurrentProcess();
            var rect = new User32.Rect();

            User32.GetWindowRect(proc.MainWindowHandle, ref rect);

            int width = rect.right - rect.left;
            int height = rect.bottom - rect.top;
            var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            using (Graphics graphics = Graphics.FromImage(bmp)) {
                graphics.CopyFromScreen(rect.left, rect.top, 0, 0, new Size(width, height), CopyPixelOperation.SourceCopy);
            }

            string fileName = Settings.ScreenshotPath + DateTime.UtcNow.ToString("yyyy-MM-ddTHHmmssZ") + ".png";
            bmp.Save(fileName, ImageFormat.Png);
        }

        private void handleSplit(object sender, EventArgs e) {
            splitFlag = true;
        }

        public override XmlNode GetSettings(XmlDocument document) => Settings.GetSettings(document);

        public override Control GetSettingsControl(LayoutMode mode) => Settings;

        public override void SetSettings(XmlNode settings) => Settings.SetSettings(settings);

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode) {
            if (splitFlag) {
                splitFlag = false;
                if (state.CurrentPhase == TimerPhase.Ended) {
                    takeScreenshot();
                }                
            }
        }

        public override void Dispose() { }
    }

    class User32 {
        [StructLayout(LayoutKind.Sequential)]
        public struct Rect {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);
    }

}

