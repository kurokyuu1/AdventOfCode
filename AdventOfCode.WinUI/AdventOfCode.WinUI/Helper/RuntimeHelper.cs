using Windows.Win32;
using Windows.Win32.Foundation;

namespace AdventOfCode.WinUI.Helper;

public class RuntimeHelper
{
    public static bool IsMsix
    {
        get
        {
            var length = 0u;
            return PInvoke.GetCurrentPackageFullName(ref length, null) != WIN32_ERROR.APPMODEL_ERROR_NO_PACKAGE;
        }
    }
}
