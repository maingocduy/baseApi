namespace TaskMonitor.Configuaration
{
    public class GlobalSettings
    {
        public static AppSettings AppSettings { get; set; }
        public static void IncludeConfig(AppSettings appSettings)
        {
            AppSettings = appSettings;
        }

        public static int MATHF_ROUND_DIGITS = 5;

        public static int OTP_COUNTDOWN_MINUTES = 2;
        public static int OTP_EXPIRED_MINUTES = 2;

        public static readonly string[] IMAGES_UPLOAD_EXTENSIONS = { ".png", ".jpeg", ".jpg" };
        public static readonly string[] EXCEL_UPLOAD_EXTENSIONS = { ".xls", ".xlsx" };

        public static readonly string FOLDER_EXPORT = "resources";
        public static readonly string SUB_FOLDER_EXCEL = "excels";
        public static readonly string SUB_FOLDER_AVATAR = "avatars";
        public static readonly string SUB_FOLDER_IMAGE = "images";
    }
}
