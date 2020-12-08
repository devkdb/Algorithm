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
    class Board
    {
        // 정보를 어떻게 들고 있을 것인가? 어떤 데이터 타입을 사용할 것인가?
        public int[] _data = new int[25]; // 배열
        public MyList<int> _data2 = new MyList<int>(); // 동적 배열

        public void Inittialize()
        {
            // 동적 배열 연구
            _data2.Add(101);
            _data2.Add(102);
            _data2.Add(103);
            _data2.Add(104);
            _data2.Add(105);

            int temp = _data2[2];

            _data2.RemoveAt(2);
        }
    }
}
