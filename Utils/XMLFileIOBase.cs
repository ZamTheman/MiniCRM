using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
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
            StorageFile file = null;
            try
            {
                file = await folder.GetFileAsync("Data.xml");
            }

            catch
            {
                file = await WriteEmptyFileAndReturnIt();
            }
            return file;
        }

        private async Task<StorageFile> WriteEmptyFileAndReturnIt()
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            var xDoc = new XDocument(
                new XDeclaration("1.0", "utf-16", "true"),
                new XElement("Companies"));
            
            StorageFile file = await folder.CreateFileAsync("Data.xml", CreationCollisionOption.ReplaceExisting);
            Stream stream = await file.OpenStreamForWriteAsync();

            using (stream)
            {
                xDoc.Save(stream);
            }

            return await folder.GetFileAsync("Data.xml");
        }
    }
}
