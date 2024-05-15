﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PatcherYRpp
{
    [StructLayout(LayoutKind.Explicit, Size = 1824)]
    public struct BuildingClass : IOwnAbstractType<BuildingTypeClass>
    {
        Pointer<BuildingTypeClass> IOwnAbstractType<BuildingTypeClass>.OwnType => Type;
        Pointer<AbstractTypeClass> IOwnAbstractType.AbstractType => Type.Convert<AbstractTypeClass>();

        public unsafe uint GetCurrentFrame()
        {
            var func = (delegate* unmanaged[Thiscall]<ref BuildingClass, uint>)0x43EF90;
            return func(ref this);
        }

        public unsafe uint GetFWFlags()
        {
            var func = (delegate* unmanaged[Thiscall]<ref BuildingClass, uint>)0x455B90;
            return func(ref this);
        }

        public unsafe CoordStruct GetTargetCoords()
        {
            var temp = new CoordStruct(0, 0, 0);
            ((delegate* unmanaged[Thiscall]<ref BuildingClass, ref CoordStruct, IntPtr>)0x4500A0)(ref this, ref temp);

            return temp;
        }


        [FieldOffset(0)] public TechnoClass Base;
        [FieldOffset(0)] public RadioClass BaseRadio;
        [FieldOffset(0)] public MissionClass BaseMission;
        [FieldOffset(0)] public ObjectClass BaseObject;
        [FieldOffset(0)] public AbstractClass BaseAbstract;

        [FieldOffset(1312)] public Pointer<BuildingTypeClass> Type;
        [FieldOffset(1316)] public Pointer<FactoryClass> Factory;

        [FieldOffset(1632)] public Bool HasPower;
        [FieldOffset(1633)] public Bool IsOverpowered;

        [FieldOffset(1757)] public Bool unknown_bool_6DD;

    }
}
