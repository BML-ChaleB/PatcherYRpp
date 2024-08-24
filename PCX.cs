using System;
using System.Runtime.InteropServices;

namespace PatcherYRpp
{
    [StructLayout(LayoutKind.Explicit, Size = 4)]
    public struct PCX
    {
     
        private static IntPtr InstancePointer = new IntPtr(0xAC4848);
        public static Pointer<PCX> Instance { get => ((Pointer<Pointer<PCX>>)InstancePointer).Data; set => ((Pointer<Pointer<PCX>>)InstancePointer).Ref = value; }

        private unsafe bool ForceLoadFile(string pFileName, int flag1, int flag2)
        {
            var func = (delegate* unmanaged[Thiscall]<IntPtr, AnsiString, int, int, bool>)0x6B9D00;
            return func(this.GetThisPointer(), pFileName, flag1, flag2);
        }

        public bool LoadFile(string fileName, int flag1 = 2, int flag2 = 0)
	    {
            if (Instance.Ref.GetSurface(fileName, Pointer<BytePalette>.Zero).IsNotNull)
            {
                return true;
            }
            return Instance.Ref.ForceLoadFile(fileName, flag1, flag2);
        }

        public unsafe Pointer<BSurface> GetSurface(string pFileName, Pointer<BytePalette> pPalette)
		{
            var func = (delegate* unmanaged[Thiscall]<IntPtr, AnsiString, IntPtr,IntPtr>)0x6BA140;
            return func(this.GetThisPointer(),pFileName,pPalette);
        }

        public unsafe bool BlitToSurface(Pointer<RectangleStruct> boundingRect,Pointer<DSurface> targetSurface, Pointer<BSurface> PCXSurface, int transparentColor = 0xF81F)
        {
            var func = (delegate* unmanaged[Thiscall]<IntPtr, IntPtr, IntPtr, IntPtr, int, bool>)0x6BA580;
            return func(this.GetThisPointer(), boundingRect, targetSurface,PCXSurface,transparentColor);
        }
    }

    [StructLayout(LayoutKind.Sequential, Size = 4)]
    public struct PCXPointer
    {
        private int _pointer;

        private static PCXPointer Instance = new() { _pointer = 0xAC4848 };


        private unsafe bool ForceLoadFile(string pFileName, int flag1, int flag2, int buffer = 0)
            => ((delegate* unmanaged[Thiscall]<int, PCXPointer, IntPtr, IntPtr, int, int, bool>)ASM.FastCallTransferStation)
               (0x6B9D00, this, buffer.GetThisPointer(), new AnsiString(pFileName), flag1, flag2);


        public bool LoadFile(string fileName, int flag1 = 2, int flag2 = 0)
            => Instance.GetSurface(fileName, Pointer<BytePalette>.Zero).IsNotNull
                || Instance.ForceLoadFile(fileName, flag1, flag2);


        public unsafe Pointer<BSurface> GetSurface(string pFileName, Pointer<BytePalette> pPalette)
            => ((delegate* unmanaged[Thiscall]<PCXPointer, IntPtr, IntPtr, IntPtr>)0x6BA140)(this, new AnsiString(pFileName), pPalette);


        public unsafe bool BlitToSurface(Pointer<RectangleStruct> boundingRect, Pointer<Surface> targetSurface, Pointer<BSurface> PCXSurface, int transparentColor = 0xF81F)
            => ((delegate* unmanaged[Thiscall]<PCXPointer, IntPtr, IntPtr, IntPtr, int, bool>)0x6BA580)(this, boundingRect, targetSurface, PCXSurface, transparentColor);


        public static bool BlitCameo(string pFileName, Point2D Pos, Pointer<Surface> targetSurface, int Range = 0)
        {
            var SafeRect = targetSurface.Ref.GetRect();
            SafeRect.Width -= 60 + Range * 2;
            SafeRect.Height -= 32 + 48 + Range * 2;
            SafeRect.X += Range;
            SafeRect.Y += Range;

            if (!SafeRect.InRect(Pos)) return false;

            Pointer<BSurface> PCXSurface;
            if ((PCXSurface = Instance.GetSurface(pFileName, Pointer<BytePalette>.Zero)).IsNull)
            {
                Instance.ForceLoadFile(pFileName, 2, 0);
                if ((PCXSurface = Instance.GetSurface(pFileName, Pointer<BytePalette>.Zero)).IsNull) return false;
            }
            RectangleStruct Rect = new(Pos.X, Pos.Y, 60, 48);

            return Instance.BlitToSurface(Rect.GetThisPointer(), targetSurface, PCXSurface); ;
        }


        public static bool Blit(string pFileName, Point2D Pos, Pointer<Surface> targetSurface, int Range = 0, int Width = 60, int Height = 48)
        {
            var SafeRect = targetSurface.Ref.GetRect();
            SafeRect.Width -= Width + Range * 2;
            SafeRect.Height -= Height + Range * 2;
            SafeRect.X += Range;
            SafeRect.Y += Range;

            if (!SafeRect.InRect(Pos)) return false;

            Pointer<BSurface> PCXSurface;
            if ((PCXSurface = Instance.GetSurface(pFileName, Pointer<BytePalette>.Zero)).IsNull)
            {
                Instance.ForceLoadFile(pFileName, 2, 0);
                if ((PCXSurface = Instance.GetSurface(pFileName, Pointer<BytePalette>.Zero)).IsNull) return false;
            }
            RectangleStruct Rect = new(Pos.X, Pos.Y, Width, Height);

            return Instance.BlitToSurface(Rect.GetThisPointer(), targetSurface, PCXSurface); ;
        }

    }
}
