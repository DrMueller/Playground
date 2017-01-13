using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System.Windows.Forms;

namespace ProgramPlaner2
{

    static class FileHandler
    {
        public static SaveObject LoadOptions(string file)
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (Stream input = File.OpenRead(file))
                {
                    return (bf.Deserialize(input) as SaveObject);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Datei konnte nicht geladen werden!", "Hinweis", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        public static void SaveOptions(SaveObject saveObject, string file)
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (Stream output = File.OpenWrite(file))
                {
                    bf.Serialize(output, saveObject);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Datei konnte nicht gespeichert werden!", "Hinweis", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public static List<String> LoadVariableTypes()
        {
            string file = getVariableSaveFile();

            if (File.Exists(file))
            {
                try
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    using (Stream input = File.OpenRead(file))
                    {
                        return (bf.Deserialize(input) as List<String>);
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return new List<string>();
        }

        private static string getVariableSaveFile()
        {
            string filename = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase));
            filename = filename.Replace("file:\\", "");
            filename = Path.Combine(filename, "VariableTypes.VT");
            return filename;
        }

        public static void SaveVariableTypes(List<String> variableTypes)
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (Stream output = File.OpenWrite(getVariableSaveFile()))
                {
                    bf.Serialize(output, variableTypes);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static void DeleteVariableTypes()
        {
            string file = getVariableSaveFile();
            if (File.Exists(file))
            {
                try
                {
                    File.Delete(file);
                }
                catch (Exception ex)
                {
                }
            }
            
        }

    }
}
