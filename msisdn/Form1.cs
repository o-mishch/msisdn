using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace msisdn
{
    public partial class Form1 : Form
    {
        Msisdn Range = new Msisdn();
        public Form1()
        {
            InitializeComponent();
            openFileDialog1.InitialDirectory = "d:\\T08 Performance test; T09 Load Balancing Test\\";
            openFileDialog1.Filter = "Text Files(*.txt)|*.txt|All Files (*.*)|*.*";
            //openFileDialog1.FileName = "msisdn_less_sorted.txt";
            saveFileDialog1.InitialDirectory = "d:\\T08 Performance test; T09 Load Balancing Test\\";
            saveFileDialog1.Filter = "Text Files(*.txt)|*.txt|All Files (*.*)|*.*";
            //saveFileDialog1.FileName = "msisdn_less_range.txt";
            label_kit.Text = "";
            label_left.Text = "";
            label1.Text = "";
            label2.Text = "";
            label4.Text = "";
            label5.Text = "";
        }

        private void open_button_Click(object sender, EventArgs e)
        {
            //try
            //{
            bool success;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                success = Range.Open(openFileDialog1);
                if (success)
                {
                    if (listBox_massif.Items.Count > 0) listBox_massif.Items.Clear();
                    label1.Text = Range.Massif.Count.ToString();
                    label4.Text = Range.OpenFileInfo.Name;
                    if (Range.Massif.Count < 5440)
                    listBox_massif.Items.AddRange(Range.Massif.Select(x => x.ToString()).ToArray().ToArray());
                    saveFileDialog1.FileName = Range.OpenFileInfo.Name;
                }
                else
                {
                    label1.Text = "";
                    label4.Text = "";
                    MessageBox.Show("Не привильное сожержимое файла.\nФайл должен сожержать только MSISDN.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            /*}
            catch (FormatException)
            {
                label1.Text = "";
                label4.Text = "";
                MessageBox.Show("Не привильное сожержимое файла.\nФайл должен сожержать только MSISDN.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }

        private void uniq_button_Click(object sender, EventArgs e)
        {
            if (Range.Massif != null)
            {
                Range.Uniq();
                listBox_massif.Items.Clear();
                if (Range.Massif.Count < 5440)
                listBox_massif.Items.AddRange(Range.Massif.Select(i => i.ToString()).ToArray());
                label1.Text = Range.Massif.Count.ToString();
            }
        }

        private void delete_button_Click(object sender, EventArgs e)
        {
            Range = new Msisdn();
            listBox_massif.Items.Clear();
            listBox_kit.Items.Clear();
            listBox_left.Items.Clear();
            listBox_sequence.Items.Clear();
            label_kit.Text = "";
            label_left.Text = "";
            label1.Text = "";
            label2.Text = "";
            label4.Text = "";
            label5.Text = "";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (Range.Massif != null)
            {
                Range.sortArray(Convert.ToInt16(radioButton3.Tag));
                listBox_massif.Items.Clear();
                if (Range.Massif.Count < 5440)
                listBox_massif.Items.AddRange(Range.Massif.Select(i => i.ToString()).ToArray());
                label1.Text = Range.Massif.Count.ToString();
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (Range.Massif != null)
            {
                Range.sortArray(Convert.ToInt16(radioButton4.Tag));
                listBox_massif.Items.Clear();
                if (Range.Massif.Count < 5440)
                    listBox_massif.Items.AddRange(Range.Massif.Select(i => i.ToString()).ToArray());
                label1.Text = Range.Massif.Count.ToString();
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name == tabPage1.Name)
            {
                if (Range.Massif != null)
                {
                    Range.Range(Convert.ToInt16(numericUpDown1.Value));
                    if (listBox_sequence.Items.Count > 0) listBox_sequence.Items.Clear();
                    if (Range.Sequence.Count < 5440)
                        listBox_sequence.Items.AddRange(Range.Sequence.Select(i => i.ToString()).ToArray());
                    label2.Text = Range.Sequence.Count.ToString();
                }
            }
            if (tabControl1.SelectedTab.Name == tabPage2.Name)
            {
                if (Range.Massif != null)
                {
                    Range.Kits(Convert.ToInt32(numericUpDown1.Value));
                    if (listBox_left.Items.Count > 0 || listBox_kit.Items.Count > 0)
                    {
                        listBox_left.Items.Clear();
                        listBox_kit.Items.Clear();
                    }
                    //numericUpDown1.Maximum = Range.Massif.Count;
                    if (Range.Kit.Count < 5440)
                        listBox_kit.Items.AddRange(Range.Kit.Select(i => i.ToString()).ToArray());
                    if (Range.Left.Count < 5440)
                        listBox_left.Items.AddRange(Range.Left.Select(i => i.ToString()).ToArray());
                    label_kit.Text = Range.Kit.Count.ToString();
                    label_left.Text = Range.Left.Count.ToString();
                }
            }
        }

        private void multi_button_Click(object sender, EventArgs e)
        {

            if (tabControl1.SelectedTab.Name == tabPage1.Name)
            {
                if (Range.Massif != null)
                {
                    /*if (numericUpDown1.Value > 6)
                    {
                        numericUpDown1.Maximum = 6;
                        numericUpDown1.Value = 5;
                    }*/
                    if (listBox_sequence.Items.Count > 0) listBox_sequence.Items.Clear();
                    Range.Range(Convert.ToInt16(numericUpDown1.Value));
                    if (Range.Sequence.Count < 5440)
                        listBox_sequence.Items.AddRange(Range.Sequence.Select(i => i.ToString()).ToArray());
                    label2.Text = Range.Sequence.Count.ToString();
                }
            }
            if (tabControl1.SelectedTab.Name == tabPage2.Name)
            {
                if (Range.Massif != null)
                {
                    //numericUpDown1.Maximum = Range.Massif.Count;
                    Range.Kits(Convert.ToInt32(numericUpDown1.Value));
                    if (listBox_left.Items.Count > 0 || listBox_kit.Items.Count > 0)
                    {
                        listBox_left.Items.Clear();
                        listBox_kit.Items.Clear();
                    }
                    if (Range.Kit.Count < 5440)
                        listBox_kit.Items.AddRange(Range.Kit.Select(i => i.ToString()).ToArray());
                    if (Range.Left.Count < 5440)
                        listBox_left.Items.AddRange(Range.Left.Select(i => i.ToString()).ToArray());
                    label_kit.Text = Range.Kit.Count.ToString();
                    label_left.Text = Range.Left.Count.ToString();
                }
            }
        }

        private void save1_button_Click(object sender, EventArgs e)
        {
            if (Range.Massif != null)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Range.Write(saveFileDialog1, Range.Massif);
                    label5.Text = Range.SaveFileInfo.Name;
                }
            }
        }

        private void save2_button_Click(object sender, EventArgs e)
        {
            if (Range.Sequence != null)
            {
                saveFileDialog1.FileName = "Sequence_" + Range.OpenFileInfo.Name;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Range.Write(saveFileDialog1, Range.Sequence);
                    label5.Text = Range.SaveFileInfo.Name;
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name == tabPage1.Name)
            {
                multi_button.Text = "Find Sequence";
                numericUpDown1.Maximum = 10;
                numericUpDown1.Minimum = 2;
                numericUpDown1.Value = 3;
                numericUpDown1.Increment = 1;
            }
            if (tabControl1.SelectedTab.Name == tabPage2.Name)
            {
                multi_button.Text = "Gain Suite";
                if (Range.Massif != null && Range.Massif.Count >= 100)
                    numericUpDown1.Maximum = Range.Massif.Count;
                else
                    numericUpDown1.Maximum = 500000;
                numericUpDown1.Minimum = 100;
                numericUpDown1.Value = 100;
                numericUpDown1.Increment = 100;
            }
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                int temp = listBox_massif.SelectedIndex;
                if (Range.Massif.Count < 5440) listBox_massif.Items.RemoveAt(temp);
                Range.Massif.RemoveAt(temp);
                label1.Text = Range.Massif.Count.ToString();
            }
        }

        private void save_left_button_Click(object sender, EventArgs e)
        {
            if (Range.Left != null)
            {
                saveFileDialog1.FileName = "Left_" + Range.OpenFileInfo.Name;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Range.Write(saveFileDialog1, Range.Left);
                    //label5.Text = Range.SaveFileInfo.Name;
                }
            }
        }

        private void save_kit_button_Click(object sender, EventArgs e)
        {
            if (Range.Kit != null)
            {
                saveFileDialog1.FileName = "Kit_" + Range.OpenFileInfo.Name;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Range.Write(saveFileDialog1, Range.Kit);
                    //label5.Text = Range.SaveFileInfo.Name;
                }
            }
        }
    }
}
