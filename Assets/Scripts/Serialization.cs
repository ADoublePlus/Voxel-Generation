using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace VoxelGeneration
{
    public static class Serialization
    {
        public static string saveFolderName = "voxelGameSaves";

        public static string SaveLocation (string worldName)
        {
            string SaveLocation = saveFolderName + "/" + worldName + "/";

            if (!Directory.Exists(SaveLocation))
            {
                Directory.CreateDirectory(SaveLocation);
            }

            return SaveLocation;
        }

        public static string FileName (WorldPos chunkLocation)
        {
            string FileName = chunkLocation.x + "," + chunkLocation.y + "," + chunkLocation.z + ".bin";

            return FileName;
        }

        public static void SaveChunk (Chunk chunk)
        {
            Save save = new Save(chunk);

            if (save.blocks.Count == 0)
            {
                return;
            }

            string saveFile = SaveLocation(chunk.world.worldName);

            saveFile += FileName(chunk.pos);

            IFormatter formatter = new BinaryFormatter();

            Stream stream = new FileStream(saveFile, FileMode.Create, FileAccess.Write, FileShare.None);

            formatter.Serialize(stream, save);

            stream.Close();
        }

        public static bool Load (Chunk chunk)
        {
            string saveFile = SaveLocation(chunk.world.worldName);

            saveFile += FileName(chunk.pos);

            if (!File.Exists(saveFile))
            {
                return false;
            }

            IFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(saveFile, FileMode.Open);

            Save save = (Save)formatter.Deserialize(stream);

            foreach (var blocks in save.blocks)
            {
                chunk.blocks[blocks.Key.x, blocks.Key.y, blocks.Key.z] = blocks.Value;
            }

            stream.Close();

            return true;
        }
    }
}