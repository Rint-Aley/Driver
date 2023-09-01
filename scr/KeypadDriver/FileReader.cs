using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeypadDriver
{
    static internal class FileReader
    {
        static private string path = Path.GetFullPath("Data\\Settings.txt"); //Расположение фалйа с настройками

        //Создаёт файл с укзанным в переменной количеством строчек
        static public void CreateFile()
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                for (int i = 0; i < Container.keysNuber; i++)
                {
                    sw.WriteLine(i + 1);
                }
            }
        }

        //Читает конкретную строчку в фале
        static public string readLine(int numStr)
        {
            string line = string.Empty;
            using (StreamReader sw = new StreamReader(path))
            {
                //string lines = File.ReadLines(path).Skip(numStr).Take(1).First();
                for (int i = 0; i < numStr; i++)
                {
                    line = sw.ReadLine();
                }
            }
            return line;
        }

        //Заменяет выбранный номер строки на новое значение
        static public void ChangeText(int numStr, string activity, string parameter)
        {
            string newText = string.Empty;
            string lastText = readLine(numStr);
            string text = File.ReadAllText(path);
            newText += $"{numStr} {activity} {parameter}";
            text = text.Replace(lastText, newText);
            File.WriteAllText(path, text);
        }
    }
}
