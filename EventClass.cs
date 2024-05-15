using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WORD = System.UInt16;
using DWORD = System.UInt32;
using LLONG = System.UInt64;
using BYTE = System.Byte;
using Adress = System.UInt32;
using Offset = System.UInt32;
using Opposite_Ends = System.Boolean;

namespace PatcherYRpp
{
    [StructLayout(LayoutKind.Explicit, Size = 111)]
    public struct EventClass
    {
        private static IntPtr OutListPoint = new IntPtr(0xA802C8);
        public static ref EventList OutList => ref OutListPoint.Convert<EventList>().Ref;

        public bool AddEvent(ref EventClass Event)

        {
            if (OutList.Count >= 128)
            {
                return false;
            }

            Pointer<EventClass> pTail = OutList.GetEvent(false);
            pTail.Ref = Event;

            Pointer<int> pTime = OutList.GetTimings(false);
            pTime.Ref = (int)Import.timeGetTime();

            ++OutList.Count;
            OutList.Tail = (OutList.Tail + 1) & 127;
		    return true;
	    }

        #region 一些函数23333
        public unsafe Pointer<EventClass> EventClass_Special(int houseIndex, int id)
        {
            var func = (delegate* unmanaged[Thiscall]<ref EventClass, int, int, IntPtr>)0x4C65A0;
            return func(ref this, houseIndex, id);
        }

        public unsafe Pointer<EventClass> EventClass_Target(int houseIndex, NetworkEvents eventType, int id, int rtti)
        {
            var func = (delegate* unmanaged[Thiscall]<ref EventClass, int, NetworkEvents, int, int, IntPtr>)0x4C65E0;
            return func(ref this, houseIndex, eventType, id, rtti);
        }

        public unsafe Pointer<EventClass> EventClass_Sellcell(int houseIndex, NetworkEvents eventType, ref CellStruct cell)
        {
            var func = (delegate* unmanaged[Thiscall]<ref EventClass, int, NetworkEvents, ref CellStruct, IntPtr>)0x4C6650;
            return func(ref this, houseIndex, eventType, ref cell);
        }

        public unsafe Pointer<EventClass> EventClass_Archive_Planning_Connect(int houseIndex, NetworkEvents eventType, TargetClass src, TargetClass dest)
        {
            var func = (delegate* unmanaged[Thiscall]<ref EventClass, int, NetworkEvents, TargetClass, TargetClass, IntPtr>)0x4C6780;
            return func(ref this, houseIndex, eventType, src, dest);
        }

        public unsafe Pointer<EventClass> EventClass_Anim(int houseIndex, NetworkEvents eventType, Pointer<HouseClass> pHouse, ref CellStruct cell)
        {
            var func = (delegate* unmanaged[Thiscall]<ref EventClass, int, NetworkEvents, IntPtr, ref CellStruct, IntPtr>)0x4C6800;
            return func(ref this, houseIndex, eventType, pHouse, ref cell);
        }

        public unsafe Pointer<EventClass> EventClass_MegaMission(int houseIndex, TargetClass src, Mission mission, TargetClass target, TargetClass dest, TargetClass follow)
        {
            var func = (delegate* unmanaged[Thiscall]<ref EventClass, int, TargetClass, Mission, TargetClass, TargetClass, TargetClass, IntPtr>)0x4C6860;
            return func(ref this, houseIndex, src, mission, target, dest, follow);
        }

        public unsafe Pointer<EventClass> EventClass_MegaMission_F(int houseIndex, TargetClass src, Mission mission, TargetClass target, TargetClass dest, SpeedType speed, int/*MPHType*/ maxSpeed)
        {
            var func = (delegate* unmanaged[Thiscall]<ref EventClass, int, TargetClass, Mission, TargetClass, TargetClass, SpeedType, int, IntPtr>)0x4C68E0;
            return func(ref this, houseIndex, src, mission, target, dest, speed, maxSpeed);
        }

        public unsafe Pointer<EventClass> EventClass_Production(int houseIndex, NetworkEvents eventType, int rtti_id, int heap_id, bool is_naval)
        {
            var func = (delegate* unmanaged[Thiscall]<ref EventClass, int, NetworkEvents, int, int, bool, IntPtr>)0x4C6970;
            return func(ref this, houseIndex, eventType, rtti_id, heap_id, is_naval);
        }

        public unsafe Pointer<EventClass> EventClass_Unknown_Tuple(int houseIndex, NetworkEvents eventType, int unknown_0, int unknown_4, ref int unknown_c)
        {
            var func = (delegate* unmanaged[Thiscall]<ref EventClass, int, NetworkEvents, int, int, ref int, IntPtr>)0x4C6A60;
            return func(ref this, houseIndex, eventType, unknown_0, unknown_4, ref unknown_c);
        }

        public unsafe Pointer<EventClass> EventClass_Place(int houseIndex, NetworkEvents eventType, AbstractType rttitype, int heapid, int is_naval, ref CellStruct cell)
        {
            var func = (delegate* unmanaged[Thiscall]<ref EventClass, int, NetworkEvents, AbstractType, int, int, ref CellStruct, IntPtr>)0x4C6AE0;
            return func(ref this, houseIndex, eventType, rttitype, heapid, is_naval, ref cell);
        }

        public unsafe Pointer<EventClass> EventClass_SpecialPlace(int houseIndex, NetworkEvents eventType, int id, ref CellStruct cell)
        {
            var func = (delegate* unmanaged[Thiscall]<ref EventClass, int, NetworkEvents, int, ref CellStruct, IntPtr>)0x4C6B60;
            return func(ref this, houseIndex, eventType, id, ref cell);
        }

        public unsafe Pointer<EventClass> EventClass_Specific(int houseIndex, NetworkEvents eventType, AbstractType rttitype, int id)
        {
            var func = (delegate* unmanaged[Thiscall]<ref EventClass, int, NetworkEvents, AbstractType, int, IntPtr>)0x4C6BE0;
            return func(ref this, houseIndex, eventType, rttitype, id);
        }

        #endregion
        

        [FieldOffset(0)] public EventType Type;
        [FieldOffset(1)] public Bool IsExecuted;
        [FieldOffset(2)] public byte HouseIndex;
        [FieldOffset(3)] public uint Frame;
        [FieldOffset(7)] public EventData Data;
    }


    [StructLayout(LayoutKind.Explicit, Size = 14732)]
    public struct EventList
    {
        [FieldOffset(0)] public int Count;
        [FieldOffset(4)] public int Head;
        [FieldOffset(8)] public int Tail;

        [FieldOffset(12)] public BYTE List_Start;
        public IntPtr List_StartPtr => Pointer<byte>.AsPointer(ref List_Start);//EventList


        [FieldOffset(14220)] public BYTE Timings_Strat;
        public IntPtr Timings_StartPtr => Pointer<byte>.AsPointer(ref Timings_Strat);//IntList

        public Pointer<EventClass> GetEvent(Offset Index)
        {
            IntPtr oldPtr = EventClass.OutList.List_StartPtr;

            return new((Adress)oldPtr + (EventClass.OutList.Head + Index) * 111);
        }
        public Pointer<EventClass> GetEvent(Opposite_Ends HeadOrTail)
        {

            IntPtr oldPtr = EventClass.OutList.List_StartPtr;

            return HeadOrTail? oldPtr: new((Adress)oldPtr + EventClass.OutList.Tail * 111);
        }
        public Pointer<int> GetTimings(Offset Index)
        {

            IntPtr oldPtr = EventClass.OutList.Timings_StartPtr;

            return new((Adress)oldPtr + (EventClass.OutList.Head + Index) * 4);
        }
        public Pointer<int> GetTimings(Opposite_Ends HeadOrTail)
        {

            IntPtr oldPtr = EventClass.OutList.Timings_StartPtr;

            return HeadOrTail ? oldPtr : new((Adress)oldPtr + EventClass.OutList.Tail * 4);
        }

    }

    [StructLayout(LayoutKind.Explicit, Size = 104)]
    public struct EventData
    {
        #region yr原生EventData
        [FieldOffset(0)] public nothing nothing;
        [FieldOffset(0)] public Animation Animation;
        [FieldOffset(0)] public FrameInfo FrameInfo;
        [FieldOffset(0)] public Target Target;
        [FieldOffset(0)] public MegaMission MegaMission;
        [FieldOffset(0)] public MegaMission_F MegaMission_F;
        [FieldOffset(0)] public Production Production;
        [FieldOffset(0)] public Unknown_LongLong Unknown_LongLong;
        [FieldOffset(0)] public Unknown_Tuple Unknown_Tuple;
        [FieldOffset(0)] public Place Place;
        [FieldOffset(0)] public SpecialPlace SpecialPlace;
        [FieldOffset(0)] public Specific Specific;
        #endregion

        #region 自定义EventData
        #endregion

    }

    #region  yr原生EventData

    [StructLayout(LayoutKind.Explicit, Size = 104)]
    public struct nothing
    {
        [FieldOffset(0)] public BYTE Data;
    }

    [StructLayout(LayoutKind.Explicit, Size = 104)]
    public struct Animation
    {
        [FieldOffset(0)] public int ID;
        [FieldOffset(4)] public int AnimOwner;
        [FieldOffset(8)] public CellStruct Location;
        [FieldOffset(12)] public BYTE ExtraData;
    }

    [StructLayout(LayoutKind.Explicit, Size = 104)]
    public struct FrameInfo
    {
        [FieldOffset(0)] public BYTE CRC;
        [FieldOffset(4)] public WORD CommandCount;
        [FieldOffset(6)] public BYTE Delay;
        [FieldOffset(8)] public BYTE ExtraData;
    }

    [StructLayout(LayoutKind.Explicit, Size = 104)]
    public struct Target
    {
        [FieldOffset(0)] public TargetClass Whom;
        [FieldOffset(5)] public BYTE ExtraData;
    }

    [StructLayout(LayoutKind.Explicit, Size = 104)]
    public struct MegaMission
    {
        [FieldOffset(0)] public TargetClass Whom;
        [FieldOffset(5)] public BYTE Mission;
        [FieldOffset(6)] public BYTE _gap_;
        [FieldOffset(7)] public TargetClass Target;
        [FieldOffset(12)] public TargetClass Destination;
        [FieldOffset(17)] public TargetClass Follow;
        [FieldOffset(22)] public Bool IsPlanningEvent;
        [FieldOffset(23)] public BYTE ExtraData;
    }

    [StructLayout(LayoutKind.Explicit, Size = 104)]
    public struct MegaMission_F
    {
        [FieldOffset(0)] public TargetClass Whom;
        [FieldOffset(5)] public BYTE Mission;
        [FieldOffset(6)] public TargetClass Target;
        [FieldOffset(11)] public TargetClass Destination;
        [FieldOffset(16)] public int Speed;
        [FieldOffset(20)] public int MaxSpeed;
        [FieldOffset(24)] public BYTE ExtraData;
    }

    [StructLayout(LayoutKind.Explicit, Size = 104)]
    public struct Production
    {
        [FieldOffset(0)] public int RTTI_ID;
        [FieldOffset(4)] public int Heap_ID;
        [FieldOffset(8)] public int IsNaval;
        [FieldOffset(12)] public BYTE ExtraData;
    }

    [StructLayout(LayoutKind.Explicit, Size = 104)]
    public struct Unknown_LongLong
    {
        [FieldOffset(0)] public int Unknown_0;
        [FieldOffset(8)] public LLONG Data;
        [FieldOffset(16)] public int Unknown_C;
        [FieldOffset(24)] public BYTE ExtraData;
    }

    [StructLayout(LayoutKind.Explicit, Size = 104)]
    public struct Unknown_Tuple
    {
        [FieldOffset(0)] public int Unknown_0;
        [FieldOffset(4)] public int Unknown_4;
        [FieldOffset(8)] public int Data;
        [FieldOffset(12)] public int Unknown_C;
        [FieldOffset(16)] public BYTE ExtraData;
    }

    [StructLayout(LayoutKind.Explicit, Size = 104)]
    public struct Place
    {
        [FieldOffset(0)] public AbstractType RTTIType;
        [FieldOffset(4)] public int HeapID;
        [FieldOffset(8)] public int IsNaval;
        [FieldOffset(12)] public CellStruct Location;
        [FieldOffset(16)] public BYTE ExtraData;
    }

    [StructLayout(LayoutKind.Explicit, Size = 104)]
    public struct SpecialPlace
    {
        [FieldOffset(0)] public int ID;
        [FieldOffset(4)] public CellStruct Location;
        [FieldOffset(8)] public BYTE ExtraData;
    }

    [StructLayout(LayoutKind.Explicit, Size = 104)]
    public struct Specific
    {
        [FieldOffset(0)] public AbstractType RTTIType;
        [FieldOffset(4)] public int ID;
        [FieldOffset(8)] public BYTE ExtraData;
    }

    #endregion

    #region 自定义EventData
    #endregion

    public enum EventType : BYTE
    {
        EMPTY = 0,
        POWERON = 1,
        POWEROFF = 2,
        ALLY = 3,
        MEGAMISSION = 4,
        MEGAMISSION_F = 5,
        IDLE = 6,
        SCATTER = 7,
        DESTRUCT = 8,
        DEPLOY = 9,
        DETONATE = 10,
        PLACE = 11,
        OPTIONS = 12,
        GAMESPEED = 13,
        PRODUCE = 14,
        SUSPEND = 15,
        ABANDON = 16,
        PRIMARY = 17,
        SPECIAL_PLACE = 18,
        EXIT = 19,
        ANIMATION = 20,
        REPAIR = 21,
        SELL = 22,
        SELLCELL = 23,
        SPECIAL = 24,
        FRAMESYNC = 25,
        MESSAGE = 26,
        RESPONSTIME = 27,
        FRAMEINFO = 28,
        SAVEGAME = 29,
        ARCHIVE = 30,
        ADDPLAYER = 31,
        TIMING = 32,
        PROCESS_TIME = 33,
        PAGEUSER = 34,
        REMOVEPLAYER = 35,
        LATENCYFUDGE = 36,
        MEGAFRAMEINFO = 37,
        PACKETTIMING = 38,
        ABOUTTOEXIT = 39,
        FALLBACKHOST = 40,
        ADDRESSCHANGE = 41,
        PLANCONNECT = 42,
        PLANCOMMIT = 43,
        PLANNODEDELETE = 44,
        ALLCHEER = 45,
        ABANDON_ALL = 46,
        LAST_EVENT = 47
    }
}

[StructLayout(LayoutKind.Explicit, Size = 5)]
public struct TargetClass 
{
    [FieldOffset(0)] public int m_ID;
    [FieldOffset(4)] public BYTE m_RTTI;
}


/*
 * 需要在Unsorted中添加
     public static class Import
    {
        [DllImport("winmm")] public static extern uint timeGetTime();
    }
 *
 *
 *另外感谢俊的帮助(
 *
 *重载啥的懒了(
 */
