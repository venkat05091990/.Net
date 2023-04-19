using System;
using System.Reflection.Metadata;
using System.Text;

namespace DesignPatterns
{

    public class HTMLElement
    {
        public string Name, Text;
        public List<HTMLElement> Elements = new();
        private const int indentSize = 2;

        
        public HTMLElement()
        {

        }

        public HTMLElement(string name, string text)
        {
            Name = name ?? throw new ArgumentOutOfRangeException(paramName: nameof(name));
            Text = text ?? throw new ArgumentNullException(paramName: nameof(text));
        }

        private string ToStringImpl(int indent)
        {
            StringBuilder sb = new();
            var i = new string(' ', indentSize * indent);

            sb.AppendLine($"{i}<{Name}>");

            if (!string.IsNullOrEmpty(Text))
            {
                sb.Append(new string(' ', indentSize * (indent + 1)));
                sb.AppendLine(Text);
            }

            // loop over the htmlelemnets list
            foreach (var element in Elements)
            {
                sb.AppendLine($"{element.ToStringImpl(indentSize * (indent + 1))}");
            }

            sb.AppendLine($"{i}</{Name}>");
            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImpl(0);
        }
    }

    public class HTMLBuilder
    {
        public HTMLElement root = new HTMLElement();
        public string rootText;
        public HTMLBuilder(string root, string text)
        {
            this.root.Name = root;
            this.root.Text = rootText = text;
        }

        // Fluent Builder Pattern, Return this instance so that chaining is possible from the client
        public HTMLBuilder AddChild(string child, string text)
        {
            root.Elements.Add(new(child, text));
            return this;
        }

        public void ClearHTML()
        {
            this.root = new(this.root.Name, "");
        }

        public override string? ToString()
        {
            return this.root.ToString();
        }
    }


    class Demo
    {
        static void Main(string[] args)
        {
            HTMLBuilder hTMLBuilder = new HTMLBuilder("html", "");
            hTMLBuilder.AddChild("li", "Apple").AddChild("li", "Banana").AddChild("li", "Mango");
            Console.WriteLine(hTMLBuilder);

            hTMLBuilder.ClearHTML();
            hTMLBuilder.AddChild("li", "Sachin").AddChild("li", "Sourav").AddChild("li", "Rahul");
            Console.WriteLine(hTMLBuilder);

        }
    }
}