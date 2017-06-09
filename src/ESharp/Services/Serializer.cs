using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ESharp
{
    internal class Serializer
    {
        private readonly BinaryFormatter _formatter;
        private const string DATADIR = "data.dat";
        private const string PATHDIR = "path.dat";
        private const string OLDChapterDIR = "oldChapter.dat";
        private const string OLDArticleDIR = "oldArticle.dat";

        public Serializer()
        {
            _formatter = new BinaryFormatter();
        }

        public void SerializeData(byte[] data)
        {
            using (var fs = new FileStream(DATADIR, FileMode.OpenOrCreate))
            {
                _formatter.Serialize(fs, data);
            }
        }

        public void SerializePath(string data)
        {
            using (var fs = new FileStream(PATHDIR, FileMode.OpenOrCreate))
            {
                _formatter.Serialize(fs, data);
            }
        }

        public void SerializeOldChapter(string chapter)
        {
            using (var fs = new FileStream(OLDChapterDIR, FileMode.OpenOrCreate))
            {
                _formatter.Serialize(fs, chapter);
            }
        }

        public void SerializeOldArticle(string article)
        {
            using (var fs = new FileStream(OLDArticleDIR, FileMode.OpenOrCreate))
            {
                _formatter.Serialize(fs, article);
            }
        }

        public byte[] DeserializeData()
        {
            using (var fs = new FileStream(DATADIR, FileMode.OpenOrCreate))
            {
                return (byte[])_formatter.Deserialize(fs);
            }
        }

        public string DeserializePath()
        {
            using (var fs = new FileStream(PATHDIR, FileMode.OpenOrCreate))
            {
                return (string)_formatter.Deserialize(fs);
            }
        }

        public string DeserializeOldChapterPath()
        {
            using (var fs = new FileStream(OLDChapterDIR, FileMode.OpenOrCreate))
            {
                return (string)_formatter.Deserialize(fs);
            }
        }

        public string DeserializeOldArticlePath()
        {
            using (var fs = new FileStream(OLDArticleDIR, FileMode.OpenOrCreate))
            {
                return (string)_formatter.Deserialize(fs);
            }
        }
    }
}