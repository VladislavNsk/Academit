using System;
using System.IO;

namespace Csv
{
    class Csv
    {
        public static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Не верное количество аргументов");
                return;
            }

            if (!args[0].Contains(".csv"))
            {
                throw new IOException("Не верный файл для чтения, расширение должно быть \"csv\""); 
            }

            if (!args[1].Contains(".html"))
            {
                throw new IOException("Не верный файл для записи, расширение должно быть \"html\"");
            }

            using (StreamReader reader = new StreamReader(args[0]))
            {
                using (StreamWriter writer = new StreamWriter(args[1]))
                {
                    writer.WriteLine
                    (
                        "<!DOCTYPE html>" +
                        "<html lang=\"ru\">" +
                        "<head>" +
                        "<meta charset=\"utf-8\">" +
                        "</head>" +
                        "<body>" +
                        "<table border=\"1\">"
                     );

                    int quotesInCellCount = 0;

                    while (!reader.EndOfStream)
                    {
                        string currentLine = reader.ReadLine();

                        if (string.IsNullOrEmpty(currentLine))
                        {
                            continue;
                        }

                        if (quotesInCellCount % 2 == 0)
                        {
                            writer.Write ("<tr><td>");
                        }

                        foreach (char symbol in currentLine)
                        {
                            if (symbol == '\"')
                            {
                                quotesInCellCount++;

                                if (quotesInCellCount != 1)
                                {
                                    if (quotesInCellCount % 2 == 1)
                                    {
                                        writer.Write(symbol);
                                    }
                                }

                                continue;
                            }

                            if (symbol == ',')
                            {
                                if (quotesInCellCount % 2 == 0)
                                {
                                    writer.Write("</td><td>");

                                    quotesInCellCount = 0;
                                }
                                else
                                {
                                    writer.Write(symbol);
                                }

                                continue;
                            }

                            if (symbol == '&')
                            {
                                writer.Write("&amp;");
                            }
                            else if(symbol == '<')
                            {
                                writer.Write("&lt;");
                            }
                            else if (symbol == '>')
                            {
                                writer.Write("&gt;");
                            }
                            else
                            {
                                writer.Write(symbol);
                            }
                        }

                        if (currentLine[currentLine.Length - 1] == ',' || quotesInCellCount % 2 == 0)
                        {
                            writer.Write("</td></tr>");

                            quotesInCellCount = 0;
                            continue;
                        }

                        writer.Write("<br/>");
                    }

                    writer.Write( "</table></body></html>");
                }
            }
        }
    }
}
