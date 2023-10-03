using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;

namespace ektp_reader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPG Files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            openFileDialog.Title = "Select a File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the selected file's path and display it
                string selectedFilePath = openFileDialog.FileName;
                pictureBox1.Load(selectedFilePath);
                textBox1.Text = selectedFilePath;

                // You can now perform actions with the selected file, e.g., read its contents.
            }
        }
        private void WriteLineToTextBox(string text)
        {
            textBox2.AppendText(text + Environment.NewLine);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //label1.Text = "Read file....";
            using (var engine = new TesseractEngine(@".\tessdata", "ind", EngineMode.Default))
            {
                using (var image = Pix.LoadFromFile(textBox1.Text))
                {
                    using (var page = engine.Process(image))
                    {
                        string extractedText = page.GetText();
                        textBox2.Text = extractedText;
                    }
                }
            }
            MessageBox.Show("Done");

        }
    }
}
