using System;
using System.Text;
using System.Collections.Generic;

namespace Coding.Exercise
{

    public class CodeElement
    {
        public string codeElementText;
        public List<CodeElement> childrenCodeElements = new List<CodeElement>();
        public string accessSpecifier;
        public string type;
        public int indentSize = 2;

        public CodeElement()
        {

        }

        public CodeElement(string accessSpecifier, string type, string name)
        {
            this.accessSpecifier = accessSpecifier;
            this.type = type;
            this.codeElementText = name;
        }

        public string ToStringImpl(int indentation)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{new string(' ', indentSize * indentation)}{this.accessSpecifier} {this.type} {this.codeElementText}{(this.type == "class" ? "" : ";")}");
            if (this.type == "class")
                sb.AppendLine("{");
            foreach (var i in childrenCodeElements)
            {
                sb.Append($"{i.ToStringImpl(indentation+1)}");
            }
            if (this.type == "class")
                sb.AppendLine("}");
            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImpl(0);
        }
    }

    public class CodeBuilder
    {
        public CodeElement root;
        public string rootText;

        public CodeBuilder(string rootName)
        {
            this.rootText = rootName;
            root = new CodeElement("public", "class", rootName);
        }

        public CodeBuilder AddField(string name, string type)
        {
            this.root.childrenCodeElements.Add(new CodeElement("public", type, name));
            return this;
        }

        public override string ToString()
        {
            return root.ToString();
        }
    }

    public class Demo
    {
        public static void Main()
        {
            var cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
            Console.WriteLine(cb);
        }
    }
}
