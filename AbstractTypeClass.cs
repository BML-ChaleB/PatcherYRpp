﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PatcherYRpp
{
	[StructLayout(LayoutKind.Explicit, Size = 152, Pack = 1)]
	public struct AbstractTypeClass
	{
		[FieldOffset(0)]
		public AbstractClass Base;

		//[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x18)]
		//[FieldOffset(36)] public string ID;

		// offset 60 is ok, but it is 61
		//[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
		//[FieldOffset(61)] public string UINameLabel;

		//[MarshalAs(UnmanagedType.LPWStr)]
		//[FieldOffset(96)] public string UIName;

		//[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x31)]
		//[FieldOffset(100)] public string Name;

		[FieldOffset(36)] public byte ID_first;
		public string GetID() => Marshal.PtrToStringAnsi(Pointer<byte>.AsPointer(ref ID_first));

		[FieldOffset(61)] public byte UINameLabel_first;
		[FieldOffset(96)] public IntPtr UIName;
		public string GetUIName() => Marshal.PtrToStringUni(UIName);

		[FieldOffset(100)] public byte Name_first;
	}
}
