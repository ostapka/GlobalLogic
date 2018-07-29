using System.IO;
using System.Runtime.Serialization;

namespace Test
{
    /// <summary>
    /// Represent information about file
    /// </summary>
    [DataContract]
    [KnownType(typeof(FilesInfo))]
    public class FilesInfo : IFile
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Size { get; set; }
        [DataMember]
        public string FilePath { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FilesInfo"/> class
        /// </summary>
        /// <param name="path">Path to file</param>
        public FilesInfo(string path)
        {
            FileInfo info = new FileInfo(path);
            Name = info.Name;
            Size = info.Length + " B";
            FilePath = info.FullName;
        }
    }
}
