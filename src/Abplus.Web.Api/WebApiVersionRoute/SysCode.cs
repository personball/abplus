using System;

namespace Abplus.WebApiVersionRoute
{
    [Flags]
    public enum SysCode : long
    {
        H5 = 0x1 << 0,

        Android = 0x1 << 1,

        IPhone = 0x1 << 2
    }
}