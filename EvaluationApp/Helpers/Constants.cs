namespace EvaluationApp.Helpers
{
    public static class Constants
    {
        public const string DatabaseFileName = "Evaluations_v1_2.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            SQLite.SQLiteOpenFlags.ReadWrite |
            SQLite.SQLiteOpenFlags.Create |
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath =
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFileName);
    }
}
