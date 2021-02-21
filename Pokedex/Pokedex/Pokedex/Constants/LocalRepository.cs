using System;
using System.IO;

namespace Pokedex.Constants
{
    public static class LocalRepository
    {
        public const string DatabaseFilename = "PokeAPI.db3";
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
