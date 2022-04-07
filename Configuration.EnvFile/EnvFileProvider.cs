using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;
using Microsoft.Extensions.Primitives;

namespace Configuration.EnvFile
{
    public class EnvFileProvider : IFileProvider
    {
        private static readonly char[] _pathSeparators = new char[2]
        {
            Path.DirectorySeparatorChar,
            Path.AltDirectorySeparatorChar
        };

        private readonly Func<PhysicalFilesWatcher> _fileWatcherFactory;

        private PhysicalFilesWatcher _fileWatcher;

        private bool _fileWatcherInitialized;

        private object _fileWatcherLock = new object();

        internal PhysicalFilesWatcher FileWatcher
        {
            get
            {
                return LazyInitializer.EnsureInitialized(ref _fileWatcher, ref _fileWatcherInitialized, ref _fileWatcherLock, _fileWatcherFactory);
            }
            set
            {
                _fileWatcherInitialized = true;
                _fileWatcher = value;
            }
        }
        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            throw new NotImplementedException();
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            return new PhysicalFileInfo(new FileInfo(subpath));
        }

        public IChangeToken Watch(string filter)
        {
            if (filter == null)
            {
                return NullChangeToken.Singleton;
            }

            filter = filter.TrimStart(_pathSeparators);
            return FileWatcher.CreateFileChangeToken(filter);
        }
    }
}
