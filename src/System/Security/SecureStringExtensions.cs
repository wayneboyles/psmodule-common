using System.Runtime.InteropServices;

namespace System.Security
{
    public static class SecureStringExtensions
    {
        public static string ConvertToString(this SecureString secureString)
        {
            if (secureString == null)
            {
                throw new ArgumentNullException(nameof(secureString));
            }

            var intPtr = IntPtr.Zero;

            try
            {
                intPtr = Marshal.SecureStringToBSTR(secureString);
                return Marshal.PtrToStringBSTR(intPtr);
            }
            finally
            {
                Marshal.ZeroFreeBSTR(intPtr);
            }
        }
    }
}
