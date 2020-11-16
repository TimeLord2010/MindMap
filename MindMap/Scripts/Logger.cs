using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MindMap.Scripts {

    public class Logger {

        public readonly string FileName;

        public Logger(string path) {
            FileName = path;
        }

        public void DeleteFile() {
            try {
                if (File.Exists(FileName)) {
                    File.Delete(FileName);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        public void WriteLine(string input, string label = "Info") {
            try {
                using (var s = new StreamWriter(FileName, true, Encoding.UTF8)) {
                    var dt = DateTime.Now;
                    string b = dt.ToString("dd/MM/yyyy HH:mm:ss");
                    s.WriteLine($"{b} [{label}] - {input}");
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, $"Error");
            }
        }

        public void Info(string input) {
            WriteLine(input, "Info");
        }

        public void Error(string input) {
            WriteLine(input, "Error");
        }

    }
}
