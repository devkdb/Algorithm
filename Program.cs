using System;

namespace Algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            board.Inittialize(25);

            Console.CursorVisible = false;

            const int WAIT_TICK = 1000 / 30;
            
            int lastTick = 0;            
            while (true) // 게임 실행.
            {
                #region 프레임 관리
                // FPS 프레임 (60 프레임 OK. 30 프레임 이하로 NO)
                // 프레임? 이 Loop(while문)이 1초에 몇번 실행되는가.
                // 화면을 갱신하는 랜더링 부분을 자주 실행할수록 게임이
                // 부드럽고. 반대로 루프가 조금씩 시간이 밀리면서 랜더링을
                // 30초 이하로 해주게 되면 사람 눈에 게임이 굉장히 어색해진다는 말.

                // Environment 환경.
                int currentTick = System.Environment.TickCount; // 현재 시간.
                // 절대 시간 아님. 어떤 특정 기준, 즉 시스템이 시작된 이후에 경과된 
                // 밀리세컨드를 벹어주는 상대적인 시간.
                // 현재시간과 경과시간의 차이만 알면 되기 때문에 그냥 이거 사용.

                // elapsed 경과
                //int elapsedTick = currentTick - lastTick;                

                // 만약에 경과한 시간이 1/30초보다 작다면. 1초는 1000 ms.
                if (currentTick - lastTick < WAIT_TICK)
                    continue;
                lastTick = currentTick;
                #endregion


                // 입력.  모든 INPUT 다 감지.

                // 로직. 입력에 따라 게임 로직 실행.

                // 랜더링. 최종 연산 된 게임 세상을 이쁘게 그려 준다.

                Console.SetCursorPosition(0, 0);
                board.Render();
            }
        }
    }
}
