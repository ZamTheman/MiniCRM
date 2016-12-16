using System;
using System.Threading.Tasks;
using Windows.Storage;

namespace Utils
{
    public abstract class XMLFileIOBase
    {
        protected StorageFolder folder;
        
        protected XMLFileIOBase()
        {
            folder = ApplicationData.Current.LocalFolder;
        }

        protected async Task<StorageFile> GetFilePath()
        {
            return await folder.GetFileAsync("Data.xml");
        }
    }
}
