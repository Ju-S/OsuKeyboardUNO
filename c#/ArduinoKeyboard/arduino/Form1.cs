using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace arduino
{
    public partial class Form1 : Form
    {
        KeyBoardThread KBThread = new KeyBoardThread();
        Dictionary<string, string> usbInfo = new Dictionary<string, string>();

        public Form1()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;
            NotifyOrForm(false);  //트레이아이콘으로
            Visible = false;
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread cpuChk = new Thread(new ThreadStart(CpuChk));
            cpuChk.Start();

            USBRefresh();
            if (portList.SelectedItem != null)  //시작시 연결이 확인된 경우 프로그램 바로실행
                KBThread.Run(portList.SelectedItem.ToString());
        }

        protected override void WndProc(ref Message m)  //usb연결 변경을 감지하여 usbrefresh를 함
        {
            UInt32 WM_DEVICECHANGE = 0x0219;
            UInt32 DBT_DEVICEARRIVAL = 0x8000;

            if ((m.Msg == WM_DEVICECHANGE) && (m.WParam.ToInt32() == DBT_DEVICEARRIVAL))  //usb연결 확인 되었을 때
            {
                USBRefresh();
                KBThread.Run(portList.SelectedItem.ToString());  //쓰레드 실행
            }
            base.WndProc(ref m);
        }

        private void USBRefresh()  //연결된 usb포트 새로고침
        {
            portList.Items.Clear();
            portList.Text = null;
            
            foreach (string str in SerialPort.GetPortNames())  //연결된 usb포트이름 가져오기
                portList.Items.Add(str);
            if (portList.Items.Count != 0)
                portList.SelectedIndex = 0;
        }

        private void runKeyBoard_Click(object sender, EventArgs e)  //프로그램실행 및 취소
        {
            if (portList.SelectedItem != null)
                KBThread.Run(portList.SelectedItem.ToString());
        }

        public void CpuChk()  //cpu의 할당율을 체크하여 쓰레드의 종료여부를 판단
        {
            while (true)
            {
                Thread.Sleep(300);
                Invoke(new MethodInvoker(USBRefresh));
            }
        }

        #region 트레이아이콘
        private void 종료ToolStripMenuItem_Click_1(object sender, EventArgs e)  //프로그램 종료
        {
            CloseProgram();
        }

        private void 열기ToolStripMenuItem_Click(object sender, EventArgs e)  //트레이아이콘 숨기기
        {
            NotifyOrForm(true);
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            NotifyOrForm(true);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)  //트레이아이콘 보이기
        {
            CloseProgram();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                notifyIcon1.Visible = true;
                ShowInTaskbar = false;
            }
        }

        private void CloseProgram()
        {
            Application.ExitThread();
            Environment.Exit(0);
        }

        private void NotifyOrForm(bool mode)  //true = 폼, false = 트레이아이콘
        {
            if (mode)
                WindowState = FormWindowState.Normal;
            else
                WindowState = FormWindowState.Minimized;
            ShowInTaskbar = mode;
            notifyIcon1.Visible = !mode;
        }
        #endregion
    }
}
