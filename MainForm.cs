using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calc
{
    public partial class MainForm : Form
    {
        private string currentNumberText;
        private List<string> inputElements;
        public MainForm()
        {
            InitializeComponent();
            inputElements = new List<string>();
        }

        private void ButtonPool_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            this.currentNumberText += button.Text;
            CalculateAndDisplay();
        }

        private void ButtonOperator_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            var currentOperator = button.Text;

            this.inputElements.Add(this.currentNumberText);
            this.currentNumberText = string.Empty;
            this.inputElements.Add(currentOperator);
            CalculateAndDisplay();
        }

        private void ButtonEquals_Click(object sender, EventArgs e)
        {
            var tempNumber = this.currentNumberText;
            this.currentNumberText = string.Empty;
            this.inputElements.Add(tempNumber);
            CalculateAndDisplay();
            this.currentNumberText = tempNumber;
            this.inputElements.RemoveAt(this.inputElements.Count - 1);
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            this.inputElements.Clear();
            this.currentNumberText = string.Empty;
            CalculateAndDisplay();
        }


        private void CalculateAndDisplay()
        {
            var tempNumber = 0.0m;
            var currentOperator = string.Empty;
            for(var i = 0; i < this.inputElements.Count; i++)
            {
                var element = inputElements[i];
                switch (element)
                {
                    case "+":
                    case "-":
                    case "*":
                    case "/":
                        currentOperator = element;
                        break;
                    default:
                        if (string.IsNullOrEmpty(currentOperator))
                        {
                            tempNumber = decimal.Parse(element);
                        }
                        else if (currentOperator == "+")
                        {
                            tempNumber += decimal.Parse(element);
                        }
                        else if (currentOperator == "-")
                        {
                            tempNumber -= decimal.Parse(element);
                        }
                        else if (currentOperator == "*")
                        {
                            tempNumber *= decimal.Parse(element);
                        }
                        else if (currentOperator == "/")
                        {
                            tempNumber /= decimal.Parse(element);
                        }
                        break;
                }
            }

            this.ExpressionLabelValue.Text = $"{string.Join(" ", this.inputElements)} {currentNumberText}";
            this.ResultLabelValue.Text = tempNumber.ToString();
        }
    }
}
