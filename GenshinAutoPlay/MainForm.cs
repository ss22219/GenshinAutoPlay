using System;
using System.Threading;
using System.Windows.Forms;

namespace GenshinAutoPlay
{
    public partial class MainForm : Form
    {
        HotKey _hotKey;
        bool _playing = false;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadConfig();
        }


        private void LoadConfig()
        {
            textBox1.Text = Config.Instance.Content;
            numKeySpeed.Value = Config.Instance.KeySpeed;
            numSpaceSpeed.Value = Config.Instance.SpaceSpeed;
            hotKeyTextBox1.HotKey_ = HotKey.Parse(Config.Instance.HotKey);
            SetHotKey(hotKeyTextBox1.HotKey_);

        }

        public void SetHotKey(HotKey hotHey)
        {
            ClearHotKey();
            _hotKey = hotHey;
            if (_hotKey == null)
                return;
            if (!HotkeyUtility.AddHotKey(_hotKey, Play))
            {
                HotkeyUtility.RemoveHotKey(_hotKey);
                HotkeyUtility.AddHotKey(_hotKey, Play);
            }
        }

        public void ClearHotKey()
        {
            if (_hotKey != null)
                HotkeyUtility.RemoveHotKey(_hotKey);
        }

        Thread _playThread = null;
        private void Play()
        {
            _playing = !_playing;
            if (!_playing) return;
            if (_playThread != null && (_playThread.ThreadState != ThreadState.Stopped || _playThread.ThreadState != ThreadState.Aborted || _playThread.ThreadState != ThreadState.AbortRequested))
                _playThread.Abort();
            _playThread = new Thread(() =>
            {
                var keys = textBox1.Text;
                var lines = keys.Split('\r', '\n');
                string combo = null;
                foreach (var line in lines)
                {
                    BeginInvoke(new Action(() =>
                    {
                        lbCurrent.Text = line;
                    }));
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (!_playing) return;
                        var c = line[i];
                        if (c == ' ')
                        {
                            Thread.Sleep(Config.Instance.SpaceSpeed);
                            continue;
                        }
                        else if (c == '(')
                            combo = string.Empty;
                        else if (combo != null && c >= 'A' && c <= 'Z')
                            combo += $"{{{c}}}";
                        else if (c == ')')
                        {
                            SendKeys.SendWait(combo);
                            combo = null;
                            Thread.Sleep(Config.Instance.KeySpeed);
                        }
                        else if (c >= 'A' && c <= 'Z')
                        {
                            SendKeys.SendWait($"{{{c}}}");
                            Thread.Sleep(Config.Instance.KeySpeed);
                        }
                    }
                }
            });
            _playThread.Start();
        }

        private void numSpaceSpeed_ValueChanged(object sender, EventArgs e)
        {
            if (Config.Instance.SpaceSpeed == (int)numSpaceSpeed.Value)
                return;
            Config.Instance.SpaceSpeed = (int)numSpaceSpeed.Value;
            Config.Save();
        }

        private void numKeySpeed_ValueChanged(object sender, EventArgs e)
        {
            if (Config.Instance.KeySpeed == (int)numKeySpeed.Value) return;
            Config.Instance.KeySpeed = (int)numKeySpeed.Value;
            Config.Save();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (Config.Instance.Content == textBox1.Text) return;
            Config.Instance.Content = textBox1.Text;
            Config.Save();
        }

        private void hotKeyTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (hotKeyTextBox1.Text == Config.Instance.HotKey) return;
            Config.Instance.HotKey = hotKeyTextBox1.Text;
            SetHotKey(hotKeyTextBox1.HotKey_);
            Config.Save();
        }
    }
}
