using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PatcherYRpp
{
    [StructLayout(LayoutKind.Sequential, Size = 0x14)]
    public struct CounterClass
    {
        private IntPtr Vfptr;
        public Vector<int> Base;
        public int Total;

        public int GetItemCount(int Index)
        {
            return Base[Index];
        }
        public int Increment(int Index)
        {
            ++Total;
            return ++Base[Index];
        }
        public int Decrement(int Index)
        {
            --Total;
            return --Base[Index];
        }
    }
}
