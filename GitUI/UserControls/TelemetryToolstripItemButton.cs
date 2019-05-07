using System;
using System.Drawing;
using System.Windows.Forms;
using GitCommands;

namespace GitUI.UserControls
{
    internal class TelemetryToolStripButton : ToolStripButton
    {
        private string _textActive;
        private string _textInactive;

        public TelemetryToolStripButton()
        {
            Checked = true;
            CheckOnClick = true;
            DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            Image = Properties.Images.EyeOpened;
            Text = _textActive = _textInactive = "Telemetry capture on/off";
        }

        public string TextActive
        {
            get { return _textActive; }
            set
            {
                if (_textActive == value)
                {
                    return;
                }

                _textActive = value;
                InvalidateMe();
            }
        }

        public string TextInactive
        {
            get { return _textInactive; }
            set
            {
                if (_textInactive == value)
                {
                    return;
                }

                _textInactive = value;
                InvalidateMe();
            }
        }

        protected override void OnParentChanged(ToolStrip oldParent, ToolStrip newParent)
        {
            base.OnParentChanged(oldParent, newParent);

            if (newParent == null)
            {
                return;
            }

            Checked = AppSettings.TelemetryEnabled;
            InvalidateMe();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            AppSettings.TelemetryEnabled = Checked;
            InvalidateMe();
        }

        private void InvalidateMe()
        {
            Image = AppSettings.TelemetryEnabled ? Properties.Images.EyeOpened : Properties.Images.EyeClosed;
            Text = AppSettings.TelemetryEnabled ? _textActive : _textInactive;
        }
    }
}
