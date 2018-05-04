using System.Runtime.InteropServices;


namespace NetUse {
    [StructLayout(LayoutKind.Sequential)]
    public struct NetResource
    {
        public uint dwScope;
        public uint dwType;
        public uint dwDisplayType;
        public uint dwUsage;
        public string lpLocalName;
        public string lpRemoteName;
        public string lpComment;
        public string lpProvider;
    }
}