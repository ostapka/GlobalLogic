using System.IO;
using System.Runtime.Serialization;

namespace Test
{
    /// <summary>
    /// Represent information about directory
    /// </summary>
    [DataContract]
    public class DirectoriesInfo
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string DateCreated { get; set; }
        [DataMember]
        public IFile[] Files { get; set; }
        [DataMember]
        public DirectoriesInfo[] Children { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="DirectoriesInfo"/> class
        /// </summary>
        /// <param name="path">Path to directory</param>
        public DirectoriesInfo(string path)
        {
            DirectoryInfo info = new DirectoryInfo(path);
            Name = info.Name;
            DateCreated = info.CreationTime.ToString();
            Files = GetFilesInfo(path);
            Children = GetDirectoriesInfos(path);
        }

        /// <summary>
        /// Form an array of files
        /// </summary>
        /// <param name="path"> Path to directory? which contains files</param>
        /// <returns>Array of files</returns>
        private IFile[] GetFilesInfo(string path)
        {
            string[] pathes = Directory.GetFiles(path);
            IFile[] array = new FilesInfo[pathes.Length];
            for (int i = 0; i < pathes.Length; i++)
            {
                array[i] = new FilesInfo(pathes[i]);
            }
            return array;
        }

        /// <summary>
        /// Form an array of directories
        /// </summary>
        /// <param name="path">Path to directory</param>
        /// <returns>Array of directories</returns>
        private DirectoriesInfo[] GetDirectoriesInfos(string path)
        {
            string[] pathes = Directory.GetDirectories(path);
            DirectoriesInfo[] array = new DirectoriesInfo[pathes.Length];
            for (int i = 0; i < pathes.Length; i++)
            {
                array[i] = new DirectoriesInfo(pathes[i]);
            }
            return array;
        }
    }
}
