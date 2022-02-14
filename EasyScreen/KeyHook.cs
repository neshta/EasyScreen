using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;

namespace EasyScreen
{
    class KeyHook
    {
        Form2 f2 = new Form2();
        //Form1 f1 = new Form1();

        private const int WH_KEYBOARD_SS = 13;
        static public bool sets = false;
        private LowLevelKeyboardProcDelegate m_callback;
        private IntPtr m_hHook;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(
            int idHook,
            LowLevelKeyboardProcDelegate lpfn,
            IntPtr hMod, int dwThreadId);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("Kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetModuleHandle(IntPtr lpModuleName);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr CallNextHookEx(
            IntPtr hhk,
            int nCode, IntPtr wParam, IntPtr lParam);

        private IntPtr LowLevelKeyboardHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0)
            {
                return CallNextHookEx(m_hHook, nCode, wParam, lParam);
            }
            else
            {
                var khs = (KeyboardHookStruct) Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));

                if (khs.VirtualKeyCode == 44 && khs.ScanCode == 55 && khs.Flags == 129)
                {
                    f2.Show();
                    f2.OnShown();
                    //MessageBox.Show("PRINTSCREEN!");
                    IntPtr val = new IntPtr(1);
                    return val;
                }
                else if (khs.VirtualKeyCode == 27 && khs.Flags == 128 && Form2.Form_init == true && global::EasyScreen.Program.globalState == 1)
                {
                    f2.RestoreWorkSpace();
                    IntPtr val = new IntPtr(1);
                    //Environment.Exit(0);
                    return val;
                }
                else if (khs.VirtualKeyCode == 32 && khs.ScanCode == 57 && khs.Flags == 128 && Form2.Form_init == true && global::EasyScreen.Program.globalState == 1)
                {
                    f2.ResetPictureSize();
                    IntPtr val = new IntPtr(1);
                    return val;
                    //MessageBox.Show("SPACE!");
                }
                return CallNextHookEx(m_hHook, nCode, wParam, lParam);
            }
        }


        [StructLayout(LayoutKind.Sequential)]
        private struct KeyboardHookStruct
        {
            public readonly int VirtualKeyCode;
            public readonly int ScanCode;
            public readonly int Flags;
            public readonly int Time;
            public readonly IntPtr ExtraInfo;
        }


        private delegate IntPtr LowLevelKeyboardProcDelegate(
            int nCode, IntPtr wParam, IntPtr lParam);


        public void SetHook()
        {
            m_callback = LowLevelKeyboardHookProc;
            m_hHook = SetWindowsHookEx(WH_KEYBOARD_SS,
                m_callback,
                GetModuleHandle(IntPtr.Zero), 0);
            f2.notifyIcon1.ContextMenuStrip = new ContextMenuStrip();
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\" + Process.GetCurrentProcess().ProcessName + ".exe";
            bool enabled = Controller.CheckForAutoRun();

            var item = new ToolStripMenuItem("Автозагрузка");
            item.Checked = true;
            item.Click += (o, ee) =>
            {
                switch (item.Checked)
                {
                    case true:
                        {
                            Controller.SetAutoRun(false);
                            Controller.SetAutorunValue(dir, false);
                            item.Checked = false;
                            break;
                        }
                    case false:
                        {
                            Controller.SetAutoRun(true);
                            Controller.Install(dir, true);
                            Controller.SetAutorunValue(dir, true);
                            item.Checked = true;
                            break;
                        }
                }
            };

            if (enabled)
            {
                Controller.SetAutoRun(true);
                Controller.Install(dir, true);
                Controller.SetAutorunValue(dir, true);
                item.Checked = true;
            }
            else
            {
                Controller.SetAutoRun(false);
                Controller.SetAutorunValue(dir, false);
                item.Checked = false;
            }
            f2.notifyIcon1.ContextMenuStrip.Items.Add(item);
            
            f2.notifyIcon1.ShowBalloonTip(5000, "EasyScreen", "Для выделения области экрана нажмите PrintScreen.\nДля сохранения скриншота на Рабочий стол нажмите Space.\nДля отмены нажмите Esc или ПКМ.\n", ToolTipIcon.Info);

            var itm = new ToolStripMenuItem("Помощь");
            itm.Click += (o, ee) => MessageBox.Show("Для выделения области экрана нажмите PrintScreen.\nДля сохранения скриншота на Рабочий стол нажмите Space.\nДля отмены нажмите Esc или ПКМ.\n", "Помощь", MessageBoxButtons.OK, MessageBoxIcon.Information);
            f2.notifyIcon1.ContextMenuStrip.Items.Add(itm);

            var itm2 = new ToolStripMenuItem("Выход");
            itm2.Click += (o, ee) =>
            {
                f2.notifyIcon1.Visible = false;
                Environment.Exit(0);
            };

            f2.notifyIcon1.ContextMenuStrip.Items.Add(itm2);
            
        }


        public void Unhook()
        {
            UnhookWindowsHookEx(m_hHook);
            f2.notifyIcon1.Visible = false;
        }
    }
}
