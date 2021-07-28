using System;
using System.Windows.Forms;
using System.Xml;
using LiveSplit.UI;
using System.Reflection;

namespace LiveSplit.SplitScreenshotLibrary {
    public partial class SplitScreenshotComponentSettings : UserControl {

        public string ScreenshotPath { get; set; }

        const string DEFAULT_SCREENSHOT_PATH = "";

        public SplitScreenshotComponentSettings() {
            InitializeComponent();
            ScreenshotPath = DEFAULT_SCREENSHOT_PATH;

            this.textBox1.DataBindings.Add("Text", this, nameof(ScreenshotPath), false, DataSourceUpdateMode.OnPropertyChanged);
        }

        internal XmlNode GetSettings(XmlDocument document) {
            XmlElement settingsNode = document.CreateElement("Settings");

            settingsNode.AppendChild(SettingsHelper.ToElement(document, "Version", Assembly.GetExecutingAssembly().GetName().Version.ToString(3)));
            settingsNode.AppendChild(SettingsHelper.ToElement(document, "ScreenshotPath", ScreenshotPath));

            return settingsNode;
        }

        internal void SetSettings(XmlNode settings) {
            ScreenshotPath = SettingsHelper.ParseString(settings["ScreenshotPath"], DEFAULT_SCREENSHOT_PATH);
        }

        private void UserControl1_Load(object sender, EventArgs e) {

        }
    }
}
