namespace MyAudio.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Class containing app constants.
    /// </summary>
    internal static class Constants
    {
        /// <summary>
        /// Name of database file.
        /// </summary>
        public const string DatabaseFilename = "myAudioSQLite.db3";

        /// <summary>
        /// The modes of opening access to database.
        /// </summary>
        public const SQLite.SQLiteOpenFlags Flags =

            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |

            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |

            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        /// <summary>
        /// Gets the databse path.
        /// </summary>
        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }
    }
}
