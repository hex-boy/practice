using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NetUse
{
    public class NetUse
    {
        private const uint RESOURCETYPE_DISK = 1;

        [DllImport("mpr.dll")]
        static extern UInt32 WNetAddConnection2(
            ref NetResource lpNetResource, string lpPassword, string lpUsername, uint dwFlags);

        [DllImport("mpr.dll")]
        static extern UInt32 WNetAddConnection3(
            IntPtr hWndOwner, ref NetResource lpNetResource, string lpPassword, string lpUserName,
            uint dwFlags);

        [DllImport("mpr.dll")]
        static extern uint WNetCancelConnection2(string lpName, uint dwFlags, bool bForce);


        public uint UnMapDrive(string driveLetter)
        {
            return WNetCancelConnection2(GetDriveLetterString(driveLetter), DwFlags.CONNECT_UPDATE_PROFILE, true);
        }

        public uint UnMapResource(string resourcePath)
        {
            return WNetCancelConnection2(resourcePath, 0, true);
        }


        /// <summary>
        /// Maps the with drive letter.
        /// </summary>
        /// <param name="driveLetter">The drive letter.</param>
        /// <param name="resourcePath">The resource path.</param>
        /// <returns>System error code</returns>
        public uint MapWithDriveLetter(string driveLetter, string resourcePath)
        {
            return Map(driveLetter, resourcePath, null, null, 0);
        }

        public uint MapWithoutDriveLetter(string resourcePath)
        {
            return Map(null, resourcePath, null, null, 0);
        }


        private uint Map(string driveLetter, string resourcePath, string user, string password, uint flags)
        {
            string driveLetterLocal = null;
            if (!string.IsNullOrEmpty(driveLetter))
                driveLetterLocal = GetDriveLetterString(driveLetter);

            var networkResource = new NetResource
            {
                dwType = RESOURCETYPE_DISK,
                lpLocalName = driveLetterLocal,
                lpRemoteName = resourcePath,
                lpProvider = null // The system will then pick privider automatically
            };

            return WNetAddConnection2(ref networkResource, user, password, flags);
        }

        private string GetDriveLetterString(string driveLetter)
        {
            return $"{driveLetter}:";
        }

    }
}
