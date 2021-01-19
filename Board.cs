using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithm
{       
    class MyList<T>
    {
        const int DEFAULT_SIZE = 1;
        T[] _data = new T[DEFAULT_SIZE];

        public int Count; // 실제로 사용중인 데이터 개수
        public int Capacity { get { return _data.Length; } } // 예약된 데이터 개수

        // 시간 복잡도 분석 : 총시간복잡도는 O(N) ? 1번항은 제외하고 실시. 
        // 그래서 상수시간 복잡도 O(1).
        // 예외 케이스 : 이사 비용은 무시한다.
        public void Add(T item)
        {
            // 1. 공간이 충분히 남이 있는지 확인한다.
            if (Count >= Capacity)
            {
                // 공간을 다시 늘려서 확보한다.
                // 기존의 데이터 크기에 비례해서 늘린다.
                T[] newArray = new T[Count * 2];

                // 기존 데이터 이동
                for (int i = 0; i < Count; i++)
                    newArray[i] = _data[i];

                _data = newArray;
            }

            // 2. 공간에다가 데이터를 넣어준다.
            _data[Count] = item;
            Count++;
        }

        // 인덱서. O(1)
        public T this[int index]
        {
            get { return _data[index]; }  // int temp = _data2[2];
            set { _data[index] = value; } // ex) _data2[2]= 3;
        }

        // 뒤에거 부터 한칸씩 땡긴다. O(N) <- index가 0일수도 (Count-1)이어서 한번만 실행 할 수도. 이럴땐 최악의 경우 생각.
        public void RemoveAt(int index)
        {
            for (int i = index; i < Count - 1; i++)
                _data[i] = _data[Count - 1];

            _data[Count - 1] = default(T);  // 정수형이면 0으로, 클래스 타입이면 null로 밀어버린다.

            Count--;
        }
    }

    // Map. 대부분 고정. 게임 도중에 사이즈가 늘었다 줄었다 안한다.
    // 타일, 맵 초기화, 랜더, 등등.. 맵에 관한 모든것.
    class Board
    {
        #region 동적 배열 연구
        public MyList<int> _data2 = new MyList<int>(); // 동적 배열
        public void TestMyList()
        {
            _data2.Add(101);
            _data2.Add(102);
            _data2.Add(103);
            _data2.Add(104);
            _data2.Add(105);

            int temp = _data2[2];

            _data2.RemoveAt(2);
        }
        #endregion

        const char CIRCLE = '\u25cf';

        // 정보를 어떻게 들고 있을 것인가? 어떤 데이터 타입을 사용할 것인가?
        public TileType[,] _tile; // 배열
        public int _size;

        public enum TileType
        {
            Empty,
            Wall,
        }

        public void Inittialize(int size)
        {
            if (size % 2 == 0) // 짝수일 경우, 길 랜덤 생성시 마지막 좌표에서 out of bound. x+1 oder y+1.
                return;
      
            _tile = new TileType[size, size];
            _size = size;

            // Mazes for Programmers. 미로에 관련된 재밌는 책.

            GenerateByBinaryTree();
        }
        void GenerateByBinaryTree() // 첫번째 미로 생성 알고리즘.
        {
            // 일단 길을 다 막아버리는 작업.
            for (int y = 0; y < _size; y++) // y 좌표를 돌고
            {
                for (int x = 0; x < _size; x++) // y 좌표 마다 x 좌표를 돈다.
                {
                    // if (x == 0 || x == _size - 1 || y == 0 || y == _size - 1) // 외곽 부분일 경우, 벽으로 만든다.
                    if (x % 2 == 0 || y % 2 == 0) // 짝수인 경우.
                    {
                        _tile[y, x] = TileType.Wall;
                    }
                    else
                        _tile[y, x] = TileType.Empty;
                }
            }

            // 랜덤으로 우측 혹은 아래로 길을 뚫는 작업.
            // Binary Tree Algorithm
            Random rand = new Random();
            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        continue; // 아무 작업도 안함.  초록색 점에서만 실행.

                    if (x == _size - 2 && y == _size - 2)
                        continue;

                    // 맨 외곽 벽에 구멍 안뚫리게.
                    if (y == _size - 2)
                    {
                        _tile[y, x + 1] = TileType.Empty;
                        continue;
                    }

                    if (x == _size - 2)
                    {
                        _tile[y + 1, x] = TileType.Empty;
                        continue;
                    }

                    // 오른쪽으로 한칸 전진.
                    if (rand.Next(0, 2) == 0)
                    {
                        _tile[y, x + 1] = TileType.Empty;
                    }
                    else
                    {
                        _tile[y + 1, x] = TileType.Empty;
                    }
                }
            }
        }

        public void Render()
        {
            ConsoleColor prevColor = Console.ForegroundColor;

            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    Console.ForegroundColor = GetTileColor(_tile[y, x]);
                    Console.Write(CIRCLE); // 원을 그린다.
                }
                Console.WriteLine();
            }

            Console.ForegroundColor = prevColor;
        }

        ConsoleColor GetTileColor(TileType type)
        {
            switch(type)
            {
                case TileType.Empty:
                    return ConsoleColor.Green;
                case TileType.Wall:
                    return ConsoleColor.Red;
                default:
                    return ConsoleColor.Green;
            }
        }
    }
}
