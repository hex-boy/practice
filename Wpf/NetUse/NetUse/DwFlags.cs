namespace NetUse {
    internal class DwFlags
    {

        /// <summary>
        /// If this flag is set, the operating system is instructed to remember the mapping of the drive letter in the user's profile. 
        /// This means that if the user logs off, when they log on again at a later date, 
        /// an attempt to restore the connection will be made.
        /// </summary>
        public const uint CONNECT_UPDATE_PROFILE = 0x1;

        /// <summary>
        /// When this flag is set, the operating system is permitted to ask the user for authentication information 
        /// before attempting to map the drive letter.
        /// </summary>
        public const uint CONNECT_INTERACTIVE = 0x8;

        /// <summary>
        /// When set, this flag indicates that any default user name and password credentials will not be used 
        /// without first giving the user the opportunity to override them. 
        /// This flag is ignored if CONNECT_INTERACTIVE is not also specified.
        /// </summary>
        public const uint CONNECT_PROMPT = 0x10;

        /// <summary>
        /// This flag forces the redirection of a local device when making the connection. 
        /// For the functionality described in this article the flag has no effect. It is included here for completeness.
        /// </summary>
        public const uint CONNECT_REDIRECT = 0x80;

        /// <summary>
        /// This flag indicates that if the operating system needs to ask for a user name and password, 
        /// it should do so using the command line rather than by using dialog boxes. 
        /// This flag is ignored if CONNECT_INTERACTIVE is not also specified. 
        /// It is not available to Windows 2000 or earlier versions of the operating system.
        /// </summary>
        public const uint CONNECT_COMMANDLINE = 0x800;

        /// <summary>
        /// If set, this flag specifies that any credentials entered by the user will be saved. 
        /// If it is not possible to save the credentials or the CONNECT_INTERACTIVE is not also specified then the flag is ignored.
        /// </summary>
        public const uint CONNECT_CMD_SAVECRED = 0x1000;

    }
}