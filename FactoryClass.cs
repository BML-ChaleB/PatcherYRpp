using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PatcherYRpp
{
    [StructLayout(LayoutKind.Explicit, Size = 116)]
    public struct FactoryClass
    {
        [FieldOffset(0)] public AbstractClass Base;

        [FieldOffset(36)] public ProgressTimer Production;


        [FieldOffset(64)] public byte _queuedObjects;
        public ref DynamicVectorClass<Pointer<TechnoTypeClass>> QueuedObjects => ref Pointer<byte>.AsPointer(ref _queuedObjects).Convert<DynamicVectorClass<Pointer<TechnoTypeClass>>>().Ref;

        [FieldOffset(88)] public IntPtr _object;
        public Pointer<TechnoClass> Object { get => _object; set => _object = value; }


        [FieldOffset(92)] public byte OnHold;
        [FieldOffset(93)] public byte IsDifferent;
        [FieldOffset(96)] public int Balance;
        [FieldOffset(100)] public int OriginalBalance;
        [FieldOffset(104)] public int SpecialItem;


        [FieldOffset(108)] public IntPtr _owner;
        public Pointer<HouseClass> Owner { get => _owner; set => _owner = value; }


        [FieldOffset(112)] public byte IsSuspended;
        [FieldOffset(113)] public byte IsManual;

    }
}