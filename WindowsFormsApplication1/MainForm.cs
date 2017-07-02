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
using System.Text.RegularExpressions;


namespace WindowsFormsApplication1
{

    public partial class Form1 : Form
    {


        //this is what runs at initialization
        public Form1()
        {

            InitializeComponent();
            FunctionWordTextBox.Text = Repeatalizer.Properties.Resources.function_word_list.ToString();

            foreach(var encoding in Encoding.GetEncodings())
            {
                EncodingDropdown.Items.Add(encoding.Name);
            }
            EncodingDropdown.SelectedIndex = EncodingDropdown.FindStringExact(Encoding.Default.BodyName);


        }







        private void button1_Click(object sender, EventArgs e)
        {

            if (Convert.ToUInt32(WordWindowSizeTextbox.Text) < 2)
            {
                MessageBox.Show("Word Window Size must be >= 2.", "Problem with settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Convert.ToUInt32(PhraseLengthTextbox.Text) > Convert.ToUInt32(WordWindowSizeTextbox.Text) - 1)
            {
                MessageBox.Show("Max Phrase Length must be less\r\n than the Word Window Size.", "Problem with settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FolderBrowser.Description = "Please choose the location of your .txt files";
            FolderBrowser.ShowDialog();
            string TextFileFolder = FolderBrowser.SelectedPath.ToString();

            if (TextFileFolder != "")
            {

                saveFileDialog.FileName = "Repeatalizer.csv";

                saveFileDialog.InitialDirectory = TextFileFolder;
                saveFileDialog.ShowDialog();

                
                string OutputFileLocation = saveFileDialog.FileName;

                if (OutputFileLocation != "") { 
                    button1.Enabled = false;
                    WordWindowSizeTextbox.Enabled = false;
                    FunctionWordTextBox.Enabled = false;
                    ScanSubfolderCheckbox.Enabled = false;
                    PunctuationBox.Enabled = false;
                    EncodingDropdown.Enabled = false;
                    PhraseLengthTextbox.Enabled = false;
                    BigWordTextBox.Enabled = false;
                    BgWorker.RunWorkerAsync(new string[] {TextFileFolder, OutputFileLocation});
                }
            } 

        }

        




        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            uint WordWindowSize = 100;
            uint MaxPhraseLength = 3;
            uint BigWordSize = 6;
            
            //set up our sentence boundary detection
            Regex NewlineClean = new Regex(@"[\r\n]+", RegexOptions.Compiled);

            //selects the text encoding based on user selection
            Encoding SelectedEncoding = null;
            this.Invoke((MethodInvoker)delegate ()
            {
                SelectedEncoding = Encoding.GetEncoding(EncodingDropdown.SelectedItem.ToString());
                WordWindowSize = Convert.ToUInt32(WordWindowSizeTextbox.Text);
                MaxPhraseLength = Convert.ToUInt32(PhraseLengthTextbox.Text);
                BigWordSize = Convert.ToUInt32(BigWordTextBox.Text);
            });

            if (WordWindowSize < 2) WordWindowSize = 2;
            if (MaxPhraseLength > WordWindowSize - 1) MaxPhraseLength = WordWindowSize - 1;
            if (MaxPhraseLength < 1) MaxPhraseLength = 1;


            //the very first thing that we want to do is set up our function word lists
            List<string> FunctionWordWildcardList = new List<string>();
            List<string> FunctionWordsToHash = new List<string>();
            
            string[] OriginalFunctionWordList = NewlineClean.Split(FunctionWordTextBox.Text.ToLower());
            OriginalFunctionWordList = OriginalFunctionWordList.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            foreach(string Word in OriginalFunctionWordList)
            {
                string WordToParse = Word.Trim();

                if (WordToParse.Contains('*'))
                {
                    FunctionWordWildcardList.Add(WordToParse.Replace("*", ""));
                }
                else
                {
                    FunctionWordsToHash.Add(WordToParse);
                }

            }

            //remove duplicates
            FunctionWordWildcardList = FunctionWordWildcardList.Distinct().ToList();
            FunctionWordsToHash = FunctionWordsToHash.Distinct().ToList();

            HashSet<string> HashedFuncWords = new HashSet<string>(FunctionWordsToHash);
            string[] FunctionWordWildCards = FunctionWordWildcardList.ToArray();

            FunctionWordsToHash = null;
            FunctionWordWildcardList = null;




            //get the list of files
            var SearchDepth = SearchOption.TopDirectoryOnly;
            if (ScanSubfolderCheckbox.Checked)
            {
                SearchDepth = SearchOption.AllDirectories;
            }
            var files = Directory.EnumerateFiles( ((string[])e.Argument)[0], "*.txt", SearchDepth);



            try { 
            using (StreamWriter outputFile = new StreamWriter(((string[])e.Argument)[1]))
            {

                string HeaderString = "\"Filename\",\"WC\",\"BigWordPercent\",\"AvgUniqueWPWindow\",\"Overall_Repeat_1word\",\"Funct_Repeat_1word\",\"Content_Repeat_1word\",\"BigWordRepeat\"";

                for (ushort i = 2; i <= MaxPhraseLength; i += 1){
                    HeaderString += ",\"Overall_Repeat_" + i.ToString() + "word\"";
                }

                outputFile.WriteLine(HeaderString);


                foreach (string fileName in files)
                {



                    //set up our variables to report
                    string Filename_Clean = Path.GetFileName(fileName);
                    int TotalNumberOfWords = 0;

                    double AvgUniqueWPWindow = 0;
                    double TotalRepetition = 0.0;
                    //double AvgWPS = 0.0;

                    double FunctionWordRepetition = 0.0;
                    double ContentWordRepetition = 0.0;
                    double SixLtrWordRepetition = 0;
                    ulong SixLtrWordsTotal = 0;

                    //sets up our word phrase dictionaries
                    Dictionary<int, double> PhraseDict = new Dictionary<int, double>();
                    for (ushort i = 2; i <= MaxPhraseLength; i += 1)
                    {
                        PhraseDict.Add(i, 0.0);
                    }


                        //report what we're working on
                        FilenameLabel.Invoke((MethodInvoker)delegate
                    {
                        FilenameLabel.Text = "Analyzing: " + Filename_Clean;
                    });


                                            

                    //do stuff here
                    string readText = File.ReadAllText(fileName, SelectedEncoding).ToLower();


                    readText = NewlineClean.Replace(readText, " ");

                    //remove all the junk punctuation
                    foreach (char c in PunctuationBox.Text)
                    {
                        readText = readText.Replace(c, ' ');
                    }

                    

                    //splits everything out into words
                    string[] Words = readText.Trim().Split(' ');
                    Words = Words.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                    for (int i = 0; i < Words.Length; i++) if (Words[i].Length > BigWordSize - 1) SixLtrWordsTotal++;

                        TotalNumberOfWords += Words.Count();
                    UInt64 ContentWordsDenominator = 0;
                    UInt64 FunctionWordsDenominator = 0;

                    UInt64 WordWindowIterations = 0;
                    //make sure that the text is at least long enough to analyze
                    if (TotalNumberOfWords >= WordWindowSize) {

                            
                        //this is where we make a moving window
                        for (uint BigCounter = 0; BigCounter <= (Words.Length - WordWindowSize); BigCounter += 1)
                        {
                            WordWindowIterations += 1;

                            var WordWindow = new string[WordWindowSize];
                            Array.Copy(Words, BigCounter, WordWindow, 0, WordWindowSize);

                            //do our full phrase repetition measures
                            for (int i = 2; i <= MaxPhraseLength; i += 1)
                                {
                                    var PhraseWindow = new string[WordWindowSize - (i - 1)];
                                    for (int j = 0; j <= (WordWindowSize - i); j += 1)
                                    {
                                        string[] temp_phrase = new string[i];
                                        Array.Copy(Words, j, temp_phrase, 0, i);
                                        PhraseWindow[j] = String.Join(" ", temp_phrase);
                                    }
                                    //add in the unique phrase percentage
                                    PhraseDict[i] += PhraseWindow.Distinct().ToArray().Length / ((double) WordWindowSize - (i - 1));
                                }

                            //AvgWPS += Words.Count();
                            AvgUniqueWPWindow += WordWindow.Distinct().ToArray().Length;
                            TotalRepetition += WordWindow.Distinct().ToArray().Length / (double)WordWindowSize;

                            //now we go through and redo the same thing, separately for function words and content words
                            //the first thing that we need to do is separate out the function words from the content words
                            List<string> FunctionWords = new List<string>();
                            List<string> ContentWords = new List<string>();
                            List<string> SixLtrWords = new List<string>();

                            for (int i = 0; i < WordWindow.Length; i++)
                            {

                                //check the length of the word
                                if (WordWindow[i].Length > BigWordSize - 1) SixLtrWords.Add(WordWindow[i]);

                                //first, check with the hashset
                                if (HashedFuncWords.Contains(WordWindow[i]))
                                {
                                    FunctionWords.Add(WordWindow[i]);
                                    continue;
                                }

                                //if it wasn't found in the hashset, we'll loop through the wildcard function words
                                for (int j = 0; j < FunctionWordWildCards.Count(); j++)
                                {
                                    if (WordWindow[i].StartsWith(FunctionWordWildCards[j]))
                                    {
                                        FunctionWords.Add(WordWindow[i]);
                                        continue;
                                    }
                                }

                                //if we haven't moved on to the next word yet, then this is a content word
                                ContentWords.Add(WordWindow[i]);

                            }

                            if (ContentWords.Count() > 0)
                            {
                                ContentWordRepetition += ContentWords.Distinct().ToArray().Length / (double)ContentWords.Count();
                                ContentWordsDenominator += 1;
                            }
                            if (FunctionWords.Count() > 0)
                            {
                                FunctionWordRepetition += FunctionWords.Distinct().ToArray().Length / (double)FunctionWords.Count();
                                FunctionWordsDenominator += 1;
                            }

                            if (SixLtrWords.Count() > 0) SixLtrWordRepetition += SixLtrWords.Distinct().ToArray().Length / (double) SixLtrWords.Count();

                            }

                    }

                        







                    //divide everything by the number of sentences
                    TotalRepetition = (float) TotalRepetition / (TotalNumberOfWords - (WordWindowSize - 1));
                    FunctionWordRepetition = (float) FunctionWordRepetition / FunctionWordsDenominator;
                    ContentWordRepetition = (float) ContentWordRepetition / ContentWordsDenominator;
                    SixLtrWordRepetition = (float) SixLtrWordRepetition / (TotalNumberOfWords - (WordWindowSize - 1));
                    AvgUniqueWPWindow = (float) AvgUniqueWPWindow / (TotalNumberOfWords - (WordWindowSize - 1));


                    if (TotalNumberOfWords >= WordWindowSize) { 

                    string[] OutputString = new string[8 + MaxPhraseLength - 1];

                            OutputString[0] = '"' + Filename_Clean + '"';
                            OutputString[1] = TotalNumberOfWords.ToString();
                            OutputString[2] = Math.Round((SixLtrWordsTotal / (double)TotalNumberOfWords) * 100, 3).ToString();
                            OutputString[3] = Math.Round(AvgUniqueWPWindow, 3).ToString();
                            OutputString[4] = Math.Round((1 - TotalRepetition) * 100, 3).ToString();
                            OutputString[5] = Math.Round((1 - FunctionWordRepetition) * 100, 3).ToString();
                            OutputString[6] = Math.Round((1 - ContentWordRepetition) * 100, 3).ToString();
                            OutputString[7] = Math.Round((1 - SixLtrWordRepetition) * 100, 3).ToString();

                            for (int i = 0; i < MaxPhraseLength - 1; i += 1)
                            {
                                OutputString[8 + i] = Math.Round((1 - (PhraseDict[i + 2] / ((float) TotalNumberOfWords - (WordWindowSize -  1)))) * 100, 3).ToString();
                            }

                            outputFile.WriteLine(String.Join(",", OutputString));
                    }
                    else
                    {
                        outputFile.WriteLine('"' + Filename_Clean + '"' + "," + TotalNumberOfWords.ToString());
                    }

                }


            }

            }
            catch
            {
                MessageBox.Show("Repeatalizer could not open your output file\r\nfor writing. Is the file open in another application?");
            }



            
        }

        private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            button1.Enabled = true;
            WordWindowSizeTextbox.Enabled = true;
            FunctionWordTextBox.Enabled = true;
            ScanSubfolderCheckbox.Enabled = true;
            PunctuationBox.Enabled = true;
            EncodingDropdown.Enabled = true;
            PhraseLengthTextbox.Enabled = true;
            BigWordTextBox.Enabled = true;
            FilenameLabel.Text = "Finished!";
            MessageBox.Show("Repeatalizer has finished analyzing your texts.", "Analysis Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void WordWindowSizeTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void PhraseLengthTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void BigWordTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

    }
    


}
