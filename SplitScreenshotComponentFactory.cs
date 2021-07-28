using LiveSplit.Model;
using LiveSplit.UI.Components;
using System;

namespace LiveSplit.SplitScreenshotLibrary {
    public class SplitScreenshotComponentFactory : IComponentFactory {

        public string ComponentName => "Automatic Split Screenshots";

        public string Description => "Saves a screenshot of the splits on run completion";

        public ComponentCategory Category => ComponentCategory.Other;

        public string UpdateName => this.ComponentName;

        public string XMLURL => "https://example.org";

        public string UpdateURL => "https://example.org";

        public Version Version => new Version("0.0.1");

        public IComponent Create(LiveSplitState state) => new SplitScreenshotComponent(state);
        
    }
}
