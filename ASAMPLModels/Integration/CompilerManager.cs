using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Integration
{
    public class CompilerManager
    {
        private static string compilerpath;//змініть на свій шлях до білда AsamplCompiler (https://github.com/Asampl-development-team/Asampl/tree/master)

        public CompilerManager()
        {
        }

        public string GenerateAsamplRepresentation(
            ObservableCollection<string> libraryRepresentation,
            ObservableCollection<string> handlerRepresentation,
            ObservableCollection<string> renderRepresentation,
            ObservableCollection<string> sourcesRepresentation,
            ObservableCollection<string> setsRepresentation,
            ObservableCollection<string> elementsRepresentation,
            ObservableCollection<string> tuplesRepresentation,
            ObservableCollection<string> aggregateRepresentation,
            ObservableCollection<string> actionsRepresentation
        )
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("PROGRAM name {");
            sb.Append(GenerateSection("Libraries", libraryRepresentation));
            sb.Append(GenerateSection("Handlers", handlerRepresentation));
            sb.Append(GenerateSection("Renderers", renderRepresentation));
            sb.Append(GenerateSection("Sources", sourcesRepresentation));
            sb.Append(GenerateSection("Sets", setsRepresentation));
            sb.Append(GenerateSection("Elements", elementsRepresentation));
            sb.Append(GenerateSection("Tuples", tuplesRepresentation));
            sb.Append(GenerateSection("Aggregates", aggregateRepresentation));
            sb.Append(GenerateSection("Actions", actionsRepresentation));

            sb.AppendLine("}");

            return sb.ToString();
        }

        private string GenerateSection(string sectionName, ObservableCollection<string> items)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"  {sectionName} {{");

            foreach (var item in items)
            {
                sb.AppendLine($"    {item}");
            }

            sb.AppendLine("  }");

            return sb.ToString();
        }

        public void Compile(string AsamplCode, string libpath, string hanpath)
        {
            string CompilationOutput = string.Empty;
            try
            {
                string tempFilePath = Path.GetTempFileName();
                File.WriteAllText(tempFilePath, AsamplCode);

                string batchFilePath = Path.GetTempFileName() + ".bat";
                string command = $"\"{compilerpath}\" {tempFilePath} {hanpath} {libpath}\npause";
                File.WriteAllText(batchFilePath, command);

                using (Process compilerProcess = new Process())
                {
                    compilerProcess.StartInfo.FileName = batchFilePath;
                    compilerProcess.StartInfo.UseShellExecute = true;

                    compilerProcess.Start();
                }

                // File.Delete(tempFilePath);
                // Зверніть увагу, що batch-файл видалити не можна одразу,
            }
            catch (Exception ex)
            {
                CompilationOutput = "Помилка при запуску компілятора: " + ex.Message;
                throw new InvalidOperationException();
            }
        }
    }
}