namespace Structure.Utils
{
    public static class Constants
    {
        #region Culture

        public const string frenchCulture = "fr-FR";
        public const string englishCulture = "en-US";

        #endregion

        #region Configuration

        public const string configuration = "configuration";
        public const string login = "login";
        public const string password = "password";
        public const string language = "language";
        public const string passwordLost = "passwordLost";
        public const string save = "save";
        public const string ok = "ok";
        public const string ko = "ko";

        #endregion

        #region Network Connection
        public const string NetInfo = "netInfo";
        public const string NetNotAvailable = "netNotAvailable";
        public const string LoginError = "loginError";
        public const string LoginMsg = "loginMsg";

        #endregion

        #region ApiCredentials
        public const string ApiUserName = "astek.tracker";
        public const string ApiPassword = "3B]U/2Z8w7fDce=(";
        #endregion

        #region ApiUri
        
       public const string GetCommunicationrequestUri = @"https://api-tray.intragest.info/api/MobileGetListComms?ClientKey=";
        public const string GetListNotificationrequestUri = @"https://api-tray.intragest.info/api/MobileGetListNotifs?ClientKey=";
        public const string PostCommunication = @"https://api-tray.intragest.info/api/MobilePostCommunication";
        #endregion

        #region EmailCredentials
        public const string EmailAddress = "app-structure@cygest.fr";
        public const string EmailPassword = "rGLwBJHzT8#$ky&x";

        #endregion

        #region Documents
        public const string Temporary = "temporary";
        #endregion
    }
}
