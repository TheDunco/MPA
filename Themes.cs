using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPA
{
    class Themes
    {

        // add a theme by adding the function, combo box entry, and combo box entry switch

        public Themes(RichTextBox answerBox, TextBox inputBox, ComboBox themeCB, Label instLab, Action<Color> fun)
        {
            AnswerBox = answerBox;
            InputBox = inputBox;
            ThemeCB = themeCB;
            InstructionsLab = instLab;
            FormBackFunction = fun;
        }

        RichTextBox AnswerBox;
        TextBox InputBox;
        ComboBox ThemeCB;
        Label InstructionsLab;
        Action<Color> FormBackFunction;

        private Color currentBackColor = Color.White;
        private Color currentTextBoxColor = Color.White;
        private Color currentTextColor = Color.Black;

        public void BoW()
        {
            currentBackColor = Color.White;
            currentTextBoxColor = Color.White;
            currentTextColor = Color.Black;
            ThemeCB.SelectedItem = "BoW";
            ApplyTheme();
        }

        public void WoB()
        {
            currentBackColor = Color.Black;
            currentTextBoxColor = Color.Black;
            currentTextColor = Color.White;
            ThemeCB.SelectedItem = "WoB";
            ApplyTheme();
        }

        public void Pulse()
        {
            currentBackColor = Color.Black;
            currentTextBoxColor = Color.Black;
            currentTextColor = Color.FromArgb(0, 255, 255);
            ThemeCB.SelectedItem = "Pulse";
            ApplyTheme();
        }

        public void CBUpdateTheme()
        {
            switch(ThemeCB.SelectedItem)
            {
                case "BoW":
                    BoW();
                    break;
                case "WoB":
                    WoB();
                    break;
                case "Pulse":
                    Pulse();
                    break;
                default:
                    break;
            }
        }

        internal void ApplyTheme()
        {
            // Answer box
            AnswerBox.BackColor = currentTextBoxColor;
            AnswerBox.ForeColor = currentTextColor;

            // Input box
            InputBox.BackColor = currentTextBoxColor;
            InputBox.ForeColor = currentTextColor;

            // Theme selection combo box
            ThemeCB.BackColor = currentTextBoxColor;
            ThemeCB.ForeColor = currentTextColor;

            // Instructions label
            InstructionsLab.ForeColor = currentTextColor;

            // Entire form background
            FormBackFunction(currentBackColor);

        }
    }
}
