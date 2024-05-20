namespace SchoolManager.Database.Tools
{
    internal class FileHelper
    {
        public static string GetDbPath()
        {
            string localAppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string localDbPath = Path.Combine(localAppDataFolder, "CourseManager");

            return localDbPath;
        }
    }
}
