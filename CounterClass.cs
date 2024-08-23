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

        public unsafe int GetCount(int Index)
        {
            var func = (delegate* unmanaged[Thiscall]<ref CounterClass, int, int>)0x49FAE0;// 0x49FA00;
            return func(ref this, Index);
        }

        public unsafe int Increment(int Index)
        {
            var func = (delegate* unmanaged[Thiscall]<ref CounterClass, int, int>)0x49FA00;// 0x49FA00;
            return func(ref this, Index);
        }

        public unsafe int Decrement(int Index)
        {
            var func = (delegate* unmanaged[Thiscall]<ref CounterClass, int, int>)0x49FA70;// 0x49FA00;
            return func(ref this, Index);
        }
    }
}
