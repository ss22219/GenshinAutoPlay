using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace GenshinAutoPlay
{
    public class HotKey
    {
        private KeyModifiers _Modifiers;
        private Keys _Key;
        public KeyModifiers Modifiers
        {
            set { _Modifiers = value; } //& KeyModifiers.Mask; }
            get { return _Modifiers; }
        }
        public Keys Key
        {
            set { _Key = value; }
            get { return _Key; }
        }

        public static HotKey Parse(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;
            HotKey key = new HotKey();
            if (value.IndexOf("+") == -1)
                key.Key = (Keys)Enum.Parse(typeof(Keys), value);
            else
            {
                var modifiers = value.Split('+').Select( m => m.Trim() ).ToList();
                key.Key = (Keys)Enum.Parse(typeof(Keys), modifiers.Last());
                modifiers.RemoveAt(modifiers.Count - 1);
                foreach (var modifier in modifiers)
                {
                    switch (modifier)
                    {
                        case "Ctrl":
                            key.Modifiers |= KeyModifiers.Control;
                            break;
                        case "Shift":
                            key.Modifiers |= KeyModifiers.Shift;
                            break;
                        case "Alt":
                            key.Modifiers |= KeyModifiers.Shift;
                            break;
                        default:
                            throw new ArgumentException(modifier);
                    }
                }
            }
            return key;
        }
    }

    public class WinHotKey
    {
        private IntPtr Handle;
        private int Count = 0;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="Handle">窗口的句柄</param>
        public WinHotKey(IntPtr Handle)
        {
            this.Handle = Handle;
        }
        /// <summary>
        /// 自动清除快捷键
        /// </summary>
        ~WinHotKey()
        {
            for (int i = 0; i < Count; i++)
                UnregisterHotKey(Handle, i);
        }
        /// <summary>
        /// 设置一个快捷键
        /// </summary>
        /// <param name="HotKey">快捷键列表</param>
        public void SetHotKey(params HotKey[] @HotKey)
        {
            Count = @HotKey.Length;
            for (int i = 0; i < Count; i++)
                RegisterHotKey(Handle, i, @HotKey[i].Modifiers, @HotKey[i].Key);
        }

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, KeyModifiers fsModifiers, Keys vk);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

    }

    //[Flags()]
    public enum KeyModifiers
    {
        None = 0,
        Alt = 1,
        Control = 2,
        Shift = 4,
        Windows = 8,
        // Mask = 0xf
    }
    public class HotKeyTextBox : TextBox
    {
        private HotKey _HotKey = new HotKey();
        private Keys LastKey;


        public HotKeyTextBox()
        {
            this.KeyDown += new KeyEventHandler(HotKeyTextBox_KeyDown);
            this.KeyUp += new KeyEventHandler(HotKeyTextBox_KeyUp);
            this.KeyPress += new KeyPressEventHandler(HotKeyTextBox_KeyPress);
        }

        private void HotKeyTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void HotKeyTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (!(e.KeyValue == 18 | e.KeyValue == 17 | e.KeyValue == 16)) LastKey = Keys.None;
            if (_HotKey.Key == Keys.None)
            {
                string v = string.Empty;
                _HotKey.Modifiers = KeyModifiers.None;
                if (e.Control)
                {
                    _HotKey.Modifiers |= KeyModifiers.Control;
                    v += "Ctrl + ";
                }
                if (e.Shift)
                {
                    _HotKey.Modifiers |= KeyModifiers.Shift;
                    v += "Shift + ";
                }
                if (e.Alt)
                {
                    _HotKey.Modifiers |= KeyModifiers.Alt;
                    v += "Alt + ";
                }
                if (string.IsNullOrEmpty(v)) this.Text = Keys.None.ToString();
                else this.Text = v;
            }
        }

        private void HotKeyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 46)
            {
                _HotKey.Key = Keys.None;
                _HotKey.Modifiers = KeyModifiers.None;
                this.Text = string.Empty;
            }
            string v = string.Empty;
            _HotKey.Modifiers = KeyModifiers.None;
            if (e.Control)
            {
                _HotKey.Modifiers |= KeyModifiers.Control;
                v += "Ctrl + ";
            }
            if (e.Shift)
            {
                _HotKey.Modifiers |= KeyModifiers.Shift;
                v += "Shift + ";
            }
            if (e.Alt)
            {
                _HotKey.Modifiers |= KeyModifiers.Alt;
                v += "Alt + ";
            }
            if (e.KeyValue == 18 | e.KeyValue == 17 | e.KeyValue == 16)
            {
                if (LastKey == Keys.None)
                    _HotKey.Key = Keys.None;
                else
                    v += _HotKey.Key.ToString();
            }
            else
            {
                _HotKey.Key = e.KeyCode;
                v += _HotKey.Key.ToString();
            }
            LastKey = _HotKey.Key;
            this.Text = v;
        }

        public HotKey HotKey_
        {
            set
            {
                _HotKey = value;
                string v = string.Empty;
                if ((value.Modifiers & KeyModifiers.Control) == KeyModifiers.Control) v += "Ctrl + ";
                if ((value.Modifiers & KeyModifiers.Shift) == KeyModifiers.Shift) v += "Shift + ";
                if ((value.Modifiers & KeyModifiers.Alt) == KeyModifiers.Alt) v += "Alt + ";
                v += _HotKey.Key.ToString();
                this.Text = v;
            }
            get
            {
                return _HotKey;
            }
        }
    }
}
