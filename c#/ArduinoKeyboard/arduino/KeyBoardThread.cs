using System.Threading;

namespace arduino
{
    class KeyBoardThread
    {
        Thread KB;
        KeyBoard keyBoard;

        public void Run(string portName)  //키보드실행 쓰레드
        {
            if(ChkThread())
                AbortKB();
            keyBoard = new KeyBoard(portName, 9600);
            KB = new Thread(new ThreadStart(keyBoard.KeyBoardOn));
            KB.Start();
        }

        public bool ChkThread()  //쓰레드 실행여부 판단
        {
            if (KB != null)
                return true;
            else
                return false;
        }

        public void AbortKB()  //쓰레드 삭제
        {
            KB.Abort();
        }
    }
}
