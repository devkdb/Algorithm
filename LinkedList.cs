using System;
using System.Collections.Generic;
using System.Text;

// Ctrl + Shift + F  : Find , Replace
namespace Algorithm
{
    // Generic type. 방안에 사람, 동물.. 뭐든 다 들어갈수 있으니깐.
    class MyLinkedListNode<T>
    {
        public T Data;
        public MyLinkedListNode<T> Next;  // 참조. 다음 방 주소.
        public MyLinkedListNode<T> Prev;
    }

    class MyLinkedList<T>
    {
        // 꼭 알고 있어야 한다.
        public MyLinkedListNode<T> Head = null;
        public MyLinkedListNode<T> Tail = null;

        public int Count = 0;

        // O(1)  상수시간. 데이터가 많더라도 딱히.. 오래걸리거나 하지 않음.
        public MyLinkedListNode<T> AddLast(T data)
        {
            MyLinkedListNode<T> newRoom = new MyLinkedListNode<T>();
            newRoom.Data = data;

            if (Head == null)
                Head = newRoom;

            // 101, 102, 103(기존Tail), 104(newRoom)
            if (Tail != null)
            {
                Tail.Next = newRoom;
                newRoom.Prev = Tail;
            }

            Tail = newRoom;
            Count++;
            return newRoom;
        }

        // O(1)
        public void Remove(MyLinkedListNode<T> room)
        {
            if (Head == room)
                Head = Head.Next;

            if (Tail == room)
                Tail = Tail.Prev;

            if (room.Prev != null)
                room.Prev.Next = room.Next;

            if (room.Next != null)
                room.Next.Prev = room.Prev;

            Count--;
        }
    }
    class LinkedList
    {
        public MyLinkedList<int> _data3 = new MyLinkedList<int>(); // 연결 리스트
        public void Initialize()
        {
            _data3.AddLast(101);
            _data3.AddLast(102);
            MyLinkedListNode<int> node = _data3.AddLast(103);
            _data3.AddLast(104);
            _data3.AddLast(105);
            _data3.AddLast(106);
            _data3.AddLast(107);

            _data3.Remove(node);

            // _data3[2] 안됨. <-- 인덱서를 지원하지 않는다.  
            // 데이터 100만 개. 50만번째 인덱스 접근하고 싶다. 50만번 돈다.

        }
    }
}
