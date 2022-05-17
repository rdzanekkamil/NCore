using System;

namespace NCore.Extensions
{
    public static class GuidExtensions
    {
        public static bool IsEmpty(this Guid o) => o == Guid.Empty;

        public static bool IsNotEmpty(this Guid o) => o != Guid.Empty;
    }
}