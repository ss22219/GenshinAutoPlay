using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GenshinAutoPlay
{
    public class HotkeyUtility
    {
        private static KeyboardHook KeyboardHook { get; set; }


        public static bool AddHotKey(HotKey hotkey, Action handler)
        {
            foreach (var item in HotKeys.Keys)
            {
                if (item.Key == hotkey.Key && item.Modifiers == hotkey.Modifiers)
                    return false;
            }
            HotKeys[hotkey] = handler;
            return true;
        }
        public static void RemoveHotKey(HotKey hotkey)
        {
            HotKey key = null;
            if (!HotKeys.ContainsKey(hotkey))
                foreach (var item in HotKeys.Keys)
                {
                    if (item.Key == hotkey.Key && item.Modifiers == hotkey.Modifiers)
                        key = item;
                }
            if (key != null)
            {
                HotKeys.Remove(key);
            }
        }
        private static Dictionary<HotKey, Action> HotKeys = new Dictionary<HotKey, Action>();

        private static List<int> keyCodeList = new List<int>();
        private static void KeyboardHandler(HookStruct param, out bool handle)
        {
            handle = false;
            if (keyCodeList.Count > 0 && keyCodeList[keyCodeList.Count - 1] == param.vkCode)
                return;
            if (param.vkCode == (int)Keys.LWin || param.vkCode == (int)Keys.RWin || param.vkCode == (int)Keys.LControlKey || param.vkCode == (int)Keys.LShiftKey || param.vkCode == (int)Keys.LWin || param.vkCode == (int)Keys.LMenu || param.vkCode == (int)Keys.RMenu)
                keyCodeList.Add(param.vkCode);
            else
            {
                foreach (var item in HotKeys)
                {
                    if ((int)item.Key.Key == param.vkCode)
                    {
                        if (item.Key.Modifiers == KeyModifiers.None && keyCodeList.Count == 0)
                        {
                            handle = true;
                            item.Value();
                            break;
                        }
                        if (item.Key.Modifiers == KeyModifiers.None && keyCodeList.Count > 0)
                        {
                            continue;
                        }
                        if (item.Key.Modifiers != KeyModifiers.None && keyCodeList.Count == 0)
                        {
                            continue;
                        }
                        if (IsControl(item.Key.Modifiers) && keyCodeList.Where(k => IsControl(k)).Count() == 0)
                        {
                            continue;
                        }
                        if (IsShift(item.Key.Modifiers) && keyCodeList.Where(k => IsShift(k)).Count() == 0)
                        {
                            continue;
                        }
                        if (IsAlt(item.Key.Modifiers) && keyCodeList.Where(k => IsAlt(k)).Count() == 0)
                        {
                            continue;
                        }
                        if (IsWin(item.Key.Modifiers) && keyCodeList.Where(k => IsWin(k)).Count() == 0)
                        {
                            continue;
                        }
                        handle = true;
                        item.Value();
                        break;
                    }
                }
                keyCodeList.Clear();
            }
        }

        private static bool IsControl(KeyModifiers key)
        {
            return (key & KeyModifiers.Control) == KeyModifiers.Control;
        }
        private static bool IsControl(int key)
        {
            return key == (int)Keys.LControlKey || key == (int)Keys.RControlKey;
        }


        private static bool IsShift(KeyModifiers key)
        {
            return (key & KeyModifiers.Control) == KeyModifiers.Shift;
        }
        private static bool IsShift(int key)
        {
            return key == (int)Keys.LShiftKey || key == (int)Keys.RShiftKey;
        }


        private static bool IsAlt(KeyModifiers key)
        {
            return (key & KeyModifiers.Control) == KeyModifiers.Alt;
        }

        private static bool IsAlt(int key)
        {
            return key == (int)Keys.LMenu || key == (int)Keys.RMenu;
        }


        private static bool IsWin(KeyModifiers key)
        {

            return (key & KeyModifiers.Control) == KeyModifiers.Windows;
        }

        private static bool IsWin(int key)
        {
            return key == (int)Keys.LWin || key == (int)Keys.RWin;
        }

        static HotkeyUtility()
        {
            KeyboardHook = new KeyboardHook();
            KeyboardHook.InstallHook(KeyboardHandler);
        }

        public static void UnLoad()
        {
            if (KeyboardHook != null)
                KeyboardHook.UninstallHook();
        }
    }
}
