using System.Threading;
using System.Windows.Forms;
using FluentAssertions;
using GitCommands;
using GitUI.Properties;
using GitUI.UserControls;
using NUnit.Framework;

namespace GitUITests.UserControls
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class TelemetryToolStripButtonTests
    {
        private static readonly int EyeOpenedImageHashCode = Images.EyeOpened.RawFormat.GetHashCode();
        private static readonly int EyeClosedImageHashCode = Images.EyeClosed.RawFormat.GetHashCode();
        private TelemetryToolStripButton _button;

        [SetUp]
        public void Setup()
        {
            _button = new TelemetryToolStripButton();
        }

        [Test]
        public void ctor_should_be_activated_by_default()
        {
            _button.Checked.Should().BeTrue();
            _button.CheckOnClick.Should().BeTrue();
            _button.Image.RawFormat.GetHashCode().Should().Be(EyeOpenedImageHashCode);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void OnParentChanged_should_read_state_from_AppSettings_TelemetryEnabled(bool telemetryEnabled)
        {
            var originalValue = AppSettings.TelemetryEnabled;
            try
            {
                AppSettings.TelemetryEnabled = telemetryEnabled;

                using (var form = new Form())
                {
                    var toolstrip = new ToolStrip();
                    toolstrip.Items.Add(_button);
                    form.Controls.Add(toolstrip);

                    _button.Checked.Should().Be(telemetryEnabled);
                    if (telemetryEnabled)
                    {
                        _button.Image.RawFormat.GetHashCode().Should().Be(EyeOpenedImageHashCode);
                    }
                    else
                    {
                        _button.Image.RawFormat.GetHashCode().Should().Be(EyeClosedImageHashCode);
                    }
                }
            }
            finally
            {
                AppSettings.TelemetryEnabled = originalValue;
            }
        }

        [TestCase(false)]
        [TestCase(true)]
        public void OnClick_should_change_state_from_active_to_inactive(bool telemetryEnabled)
        {
            var originalValue = AppSettings.TelemetryEnabled;
            try
            {
                AppSettings.TelemetryEnabled = !telemetryEnabled;
                _button.Checked = !telemetryEnabled;

                _button.PerformClick();

                _button.Checked.Should().Be(telemetryEnabled);
                if (telemetryEnabled)
                {
                    _button.Image.RawFormat.GetHashCode().Should().Be(EyeOpenedImageHashCode);
                }
                else
                {
                    _button.Image.RawFormat.GetHashCode().Should().Be(EyeClosedImageHashCode);
                }
            }
            finally
            {
                AppSettings.TelemetryEnabled = originalValue;
            }
        }
    }
}
