namespace mCellmapManager
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct GsmCellInfo
    {
        public double lat;
        public double lng;
    }
}

