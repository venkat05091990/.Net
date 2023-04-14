using System;

namespace DesignPatterns
{
    public class Persistence
    {
        public void SaveToFile(string path, string content, bool overwrite = false)
        {
            if(overwrite || !File.Exists(path))
            {
                File.WriteAllText(path, content);
            }
        }
    }

    public class Journal
    {
        public List<string> entries = new List<string>();
        public static int count = 0;
        public Journal() { }

        public void AddEntry(string s)
        {
            string entry = $"{++count} : {s}";
            entries.Add(entry);
        }

        public void RemoveEntry(int p)
        {
            entries.RemoveAt(p);
            count--;
        }

        public void Clear() 
        { 
            entries.Clear(); 
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }

        public void Display()
        {
            Console.WriteLine(ToString());
        }
    }

    public class Demo
    {
        public static void Main(string[] args)
        {
            Journal journal = new Journal();
            journal.AddEntry("I wrote an article");
            journal.AddEntry("I read design pattern srp");
            journal.Display();

            Persistence persistence = new Persistence();
            persistence.SaveToFile(@"d:\dump\test.txt", journal.ToString(), true);
        }
    }
}