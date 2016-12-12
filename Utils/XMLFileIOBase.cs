using System;
using System.Threading.Tasks;
using Windows.Storage;

namespace Utils
{
    public abstract class XMLFileIOBase
    {
        protected StorageFolder folder;
        protected StorageFile file;

        Task Initialization { get; set; }

        protected XMLFileIOBase()
        {
            folder = ApplicationData.Current.LocalFolder;
            Initialization = SetFilePath();
        }

        private async Task SetFilePath()
        {
            file = await folder.GetFileAsync("Data.xml");
        }
    }
}
