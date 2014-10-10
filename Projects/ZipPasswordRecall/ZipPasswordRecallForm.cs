using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Collections;
// Add Reference : "Ionic.Zip.dll"
using Ionic.Zip;
using Ionic.Zlib;

/*!
 * ZipPasswordRecall
 * https://github.com/criticalbreak5/ZipPasswordRecall
 *
 * Copyright 2014 criticalbreak5's
 * Released under the Microsoft Public License (Ms-PL)
 * http://opensource.org/licenses/MS-PL
 *
 * Date: 2014-10-10T23:30Z
 */
namespace ZipPasswordRecall
{
    public partial class ZipPasswordRecallForm : Form
    {

        private bool isStop;

        public ZipPasswordRecallForm()
        {
            InitializeComponent();
        }
        
        private void ZipPasswordRecallForm_Load(object sender, EventArgs e)
        {

            this.isStop = false;
            this.GiveUpButton.Enabled = false;
            this.BrowseButton.Enabled = true;
            this.RecallButton.Enabled = true;

            this.TrialDataTextBox.Enabled = false;

            this.CheckBox48to57.Checked = true;
            this.CheckBox65to90.Checked = true;
            this.CheckBox97to122.Checked = true;
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            this.zipFileDialog.Title = "Please select the zip file.";
            this.zipFileDialog.Filter = "(*.zip)|*.zip";
            DialogResult dialogResult = this.zipFileDialog.ShowDialog();
            if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                this.ZipFileTextBox.Text = this.zipFileDialog.FileName;
            }
        }
        
        private void RecallButton_Click(object sender, EventArgs e)
        {
            this.RecallButton.Enabled = false;
            this.BrowseButton.Enabled = false;
            this.GiveUpButton.Enabled = true;

            _RecallButton_Click(sender, e);

            this.isStop = false;
            this.GiveUpButton.Enabled = false;
            this.BrowseButton.Enabled = true;
            this.RecallButton.Enabled = true;
        }

        private void _RecallButton_Click(object sender, EventArgs e)
        {

            String zipFilePath = this.ZipFileTextBox.Text;

            // RecallButton.
            if (zipFilePath == "")
            {
                MessageBox.Show("Please select the zip file!", "Error");
                return;
            }

            ArrayList _targetCharList = new ArrayList();
            if (this.CheckBox48to57.Checked)
            {
                // 48-57:0-9
                for (int i = 48; i <= 57; i++)
                {
                    _targetCharList.Add(((char)i).ToString());
                }
            }
            if (this.CheckBox65to90.Checked)
            {
                // 65-90:A-Z
                for (int i = 65; i <= 90; i++)
                {
                    _targetCharList.Add(((char)i).ToString());
                }
            }
            if (this.CheckBox97to122.Checked)
            {
                // 97-122:a-z
                for (int i = 97; i <= 122; i++)
                {
                    _targetCharList.Add(((char)i).ToString());
                }
            }

            // SP:32
            if (this.CheckBox32.Checked) _targetCharList.Add(((char)32).ToString());
            // !:33
            if (this.CheckBox33.Checked) _targetCharList.Add(((char)33).ToString());
            // ":34
            if (this.CheckBox34.Checked) _targetCharList.Add(((char)34).ToString());
            // #:35
            if (this.CheckBox35.Checked) _targetCharList.Add(((char)35).ToString());
            // $:36
            if (this.CheckBox36.Checked) _targetCharList.Add(((char)36).ToString());
            // %:37
            if (this.CheckBox37.Checked) _targetCharList.Add(((char)37).ToString());
            // &:38
            if (this.CheckBox38.Checked) _targetCharList.Add(((char)38).ToString());
            // ':39
            if (this.CheckBox39.Checked) _targetCharList.Add(((char)39).ToString());
            // (:40
            if (this.CheckBox40.Checked) _targetCharList.Add(((char)40).ToString());
            // ):41
            if (this.CheckBox41.Checked) _targetCharList.Add(((char)41).ToString());
            // *:42
            if (this.CheckBox42.Checked) _targetCharList.Add(((char)42).ToString());
            // +:43
            if (this.CheckBox43.Checked) _targetCharList.Add(((char)43).ToString());
            // ,:44
            if (this.CheckBox44.Checked) _targetCharList.Add(((char)44).ToString());
            // -:45
            if (this.CheckBox45.Checked) _targetCharList.Add(((char)45).ToString());
            // .:46
            if (this.CheckBox46.Checked) _targetCharList.Add(((char)46).ToString());
            // /:47
            if (this.CheckBox47.Checked) _targetCharList.Add(((char)47).ToString());
            // ::58
            if (this.CheckBox58.Checked) _targetCharList.Add(((char)58).ToString());
            // ;:59
            if (this.CheckBox59.Checked) _targetCharList.Add(((char)59).ToString());
            // <:60
            if (this.CheckBox60.Checked) _targetCharList.Add(((char)60).ToString());
            // =:61
            if (this.CheckBox61.Checked) _targetCharList.Add(((char)61).ToString());
            // >:62
            if (this.CheckBox62.Checked) _targetCharList.Add(((char)62).ToString());
            // ?:63
            if (this.CheckBox63.Checked) _targetCharList.Add(((char)63).ToString());
            // @:64
            if (this.CheckBox64.Checked) _targetCharList.Add(((char)64).ToString());
            // [:91
            if (this.CheckBox91.Checked) _targetCharList.Add(((char)91).ToString());
            // ¥:92
            if (this.CheckBox92.Checked) _targetCharList.Add(((char)92).ToString());
            // ]:93
            if (this.CheckBox93.Checked) _targetCharList.Add(((char)93).ToString());
            // ^:94
            if (this.CheckBox94.Checked) _targetCharList.Add(((char)94).ToString());
            // _:95
            if (this.CheckBox95.Checked) _targetCharList.Add(((char)95).ToString());
            // `:96
            if (this.CheckBox96.Checked) _targetCharList.Add(((char)96).ToString());
            // {:123
            if (this.CheckBox123.Checked) _targetCharList.Add(((char)123).ToString());
            // |:124
            if (this.CheckBox124.Checked) _targetCharList.Add(((char)124).ToString());
            // }:125
            if (this.CheckBox125.Checked) _targetCharList.Add(((char)125).ToString());
            // ~:126
            if (this.CheckBox126.Checked) _targetCharList.Add(((char)126).ToString());

            String[] targetCharList = (String[])_targetCharList.ToArray(typeof(String));

            for (int i = 1; i <= 100; i++)
            {
                if (generate(zipFilePath, "", targetCharList, i))
                {
                    break;
                }
            }
        }

        private bool generate(String zipFilePath, String prefix, String[] targetCharList, int digit)
        {

            for (int i = 0; i < targetCharList.Length; i++)
            {

                if (digit == 1)
                {

                    String targetChar = prefix + targetCharList[i];
                    targetChar = this.PrefixDataTextBox.Text + targetChar;
                    this.TrialDataTextBox.Text = targetChar;
                    Application.DoEvents(); // Measures to no response.

                    try
                    {
                        if (ZipFile.CheckZipPassword(zipFilePath, targetChar))
                        {
                            MessageBox.Show("Recall password!", "Result");
                            return true;
                        }
                        if (isStop) {
                            MessageBox.Show("Gave up!", "Result");
                            return true;
                        }
                    }
                    catch (Exception e)
                    {
                    }
                }
                else
                {
                    if (generate(zipFilePath, prefix + targetCharList[i], targetCharList, digit - 1))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void GiveUpButton_Click(object sender, EventArgs e)
        {
            this.GiveUpButton.Enabled = false;
            this.isStop = true;
        }
    }
}
