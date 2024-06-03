using System.ComponentModel;

namespace HistoricalNewsUpdate.Common
{
    public class AppConstant
    {
        public const string GeneralSettingsPath = "GeneralSettingsPath";
        public const string AllowedFontFileExtensions = "AllowedFontFileExtensions";
        public const string CustomFonts = "CustomFonts";
        public const string FontFolder = "Font";
        public const string EurolandEmail = "@euroland.com";

        public const string CreateSuccessResult = "Create {0} success";
        public const string EditSuccessResult = "Edit {0} success";
        public const string UpdateSuccessResult = "Update {0} success";
        public const string DeleteSuccessResult = "Delete {0} success";
        public const string DoesNotExist = "{0} does not exist";
        public const string LocalIpAddress = "::1";
        public const string SettingsCompare = "SettingsCompare";
        public const string CompanyFolder = @"Company\";
        public const string CompareFormatDateTime = "yyyy-MM-dd HH:mm";
        public const string XmlExtension = ".xml";
        public const string CssExtension = ".css";
    }

    public class PermissionsType
    {
        public const string AllowsToCreateTools = "1";
        public const string AllowsToMigrateTools = "2";
        public const string AllowsToMigrateAndCreateTools = "3";
        public const string AllowsOnlyToSearchForThings = "4";
        public const string AllowsToMigrateData = "5";
        public const string AllowsToMigrateDataAndTools = "6";
        public const string AllowsToMigrateDataAndToolsAndCreateTools = "7";
        public const string Admin = "8";

    }

    /// <summary>
    /// Claim Type
    /// </summary>
    public class ClaimType
    {
        /// <summary>
        /// Permissions
        /// </summary>
        public const string Permissions = "Permissions";
        /// <summary>
        /// UserId
        /// </summary>
        public const string UserId = "UserId";
        
        /// <summary>
        /// Full name
        /// </summary>
        public const string UserName = "UserName ";

    }

    public class DataTableName
    {
        public const int Datetime = 1;
        public const int Share = 3;
        public const int CustomNames = 4;
        public const int Peers = 6;
        public const int Indices = 7;
    }

    public enum NumberFormatEnum
    {
        [Description("space")]
        space,
        [Description("dot")]
        dot,
        [Description("comma")]
        comma
    }

    public class MigrationType
    {
        public const int Migration = 1;
        public const int UpdateToolCenter = 2;
        public const int MigrationAndUpdateToolCenter = 3;
    }

}
