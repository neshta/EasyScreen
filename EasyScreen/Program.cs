using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

[assembly: SuppressIldasm()]

namespace EasyScreen
{
    static class Program
    {
        public static byte globalState = 0;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        //[STAThread]

        static void Main()
        {
            if (AlreadyWorking() == true) 
                Environment.Exit(0);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            KeyHook hook = new KeyHook();
            hook.SetHook();
            Form6 f6 = new Form6();
            f6.Hide();
            f6.WindowState = FormWindowState.Minimized;
            f6.Visible = false;
            Application.Run(f6);
            hook.Unhook();
        }

        public static bool Form1_init = false;

        static bool AlreadyWorking()
        {
            int i = 0;
            Process[] proclist = Process.GetProcesses();
            foreach (var proc in proclist)
            {
                // the same as Process.GetCurrentProcess().Kill();
                if (proc.ProcessName.Equals(Process.GetCurrentProcess().ProcessName))
                {
                    i++;
                    if (i >= 2) 
                        return true;
                }
            }
            return false;
        }
    }

    /*
    internal class HookKeys
    {
        private const int WH_KEYBOARD_SS = 13;

        static public bool sets = false;

        Form1 f1 = new Form1();

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


        private IntPtr LowLevelKeyboardHookProc(
            int nCode, IntPtr wParam, IntPtr lParam)
        {
            //MessageBox.Show("PRINTSCREEN!");
            if (nCode < 0)
            {
                return CallNextHookEx(m_hHook, nCode, wParam, lParam);
            }
            else
            {
                var khs = (KeyboardHookStruct)
                          Marshal.PtrToStructure(lParam,
                          typeof(KeyboardHookStruct));
                //MessageBox.Show("PRINTSCREEN!\nHook: Code: {0}, WParam: {1},{2},{3},{4} ", nCode, wParam, lParam, khs.VirtualKeyCode, khs.ScanCode, khs.Flags, khs.Time);
                f1.label1.Text = "Hook\nCODE: " + nCode + "\nWParam: " + wParam + "\nLParam: " +  lParam + "\nVK_CODE: " + khs.VirtualKeyCode + "\nScanCode: " + khs.ScanCode + "\nFlags:" + khs.Flags + "\nTime: " + khs.Time;
                //string s = "Hook: Code: {0}, WParam: {1},{2},{3},{4} ", nCode, wParam, lParam, khs.VirtualKeyCode, khs.ScanCode, khs.Flags, khs.Time;
                //Log(s);

                if (khs.VirtualKeyCode == 44 && khs.ScanCode == 55 && khs.Flags == 129)
                {
                    Form2 f2 = new Form2();
                    f2.Show();
                    //MessageBox.Show("PRINTSCREEN!");
                }

                IntPtr val = new IntPtr(1);
                return val;
                //return CallNextHookEx(m_hHook, nCode, wParam, lParam);
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
        }


        public void Unhook()
        {
            UnhookWindowsHookEx(m_hHook);
        }

        
        static void Log(string log)
        {
            System.IO.File.AppendAllText(System.IO.Directory.GetCurrentDirectory() + "\\LOG.log", "[" + System.DateTime.Now.ToLongTimeString() + "." + ((System.DateTime.Now.Millisecond < 100) ? ("0" + System.DateTime.Now.Millisecond.ToString()) : System.DateTime.Now.Millisecond.ToString()) + "] " + log + "\r\n");
            return;
        }


    }
     * */
}
