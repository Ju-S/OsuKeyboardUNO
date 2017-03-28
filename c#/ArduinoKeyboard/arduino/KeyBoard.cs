using System;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;

namespace arduino
{
    class KeyBoard
    {
        static SerialPort arduino = new SerialPort();
        KeyBoardThread KBThread = new KeyBoardThread();

        public KeyBoard(string PortName, int BaudRate)  //포트 번호 및 통신 속도 설정
        {
            if (!arduino.IsOpen)
            {
                arduino.PortName = PortName;  //포트 번호
                arduino.BaudRate = BaudRate;  //통신 속도
                arduino.Open();  //포트열기
            }
        }

        public void KeyBoardOn()  //키보드실행
        {
            while (true)
            {
                Thread.Sleep(1);
                string s = null;
                try
                {
                    s = arduino.ReadExisting();  //아두이노의 신호받기
                }
                catch (UnauthorizedAccessException) { return; }
                catch (InvalidOperationException) { return; }

                if (s.Length > 1)
                    s = s.Substring(0, s.Length - 2);  //시리얼 신호의 마지막을 반영

                #region switch
                switch (s)  //조금 더 좋은 방법으로 바꾸자
                {
                    case "a":
                        KeyUp(Keys.Q);
                        break;
                    case "A":
                        KeyDown(Keys.Q);
                        break;
                    case "b":
                        KeyUp(Keys.W);
                        break;
                    case "B":
                        KeyDown(Keys.W);
                        break;
                    case "c":
                        KeyUp(Keys.E);
                        break;
                    case "C":
                        KeyDown(Keys.E);
                        break;
                    case "d":
                        KeyUp(Keys.R);
                        break;
                    case "D":
                        KeyDown(Keys.R);
                        break;
                    case "e":
                        KeyUp(Keys.T);
                        break;
                    case "E":
                        KeyDown(Keys.T);
                        break;
                    case "f":
                        KeyUp(Keys.Y);
                        break;
                    case "F":
                        KeyDown(Keys.Y);
                        break;
                    case "g":
                        KeyUp(Keys.U);
                        break;
                    case "G":
                        KeyDown(Keys.U);
                        break;
                    case "h":
                        KeyUp(Keys.I);
                        break;
                    case "H":
                        KeyDown(Keys.I);
                        break;
                }
                #endregion
            }
        }

        [DllImport("user32.dll")]  //키보드 에뮬레이터
        static extern uint keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        private static void KeyDown(Keys key)  //첫번째 인자는 동작시키려는 키 값
        {
            keybd_event((byte)key, 0, 0, 0);  //3번째 인자가 0이면 누를때
        }

        private static void KeyUp(Keys key)
        {
            keybd_event((byte)key, 0, 0x2, 0);  //0x2이면 땔때
        }
    }
}
