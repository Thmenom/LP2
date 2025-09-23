using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pimc
{
    public partial class Form1 : Form
    {
        private string lastValidPesoText = "0.00";
        private string lastValidAlturaText = "0.00";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            try
            {
                // Parse input values from txtPeso and txtAltura
                decimal peso = decimal.Parse(txtPeso.Text);
                decimal altura = decimal.Parse(txtAltura.Text);

                // Check for zero values (not allowed)
                if (peso == 0 || altura == 0)
                {
                    MessageBox.Show("Peso e altura devem ser maiores que zero.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtIMC.Text = string.Empty;
                    return;
                }

                // Calculate IMC
                decimal imc = peso / (altura * altura);

                // Format to two decimal places and show in txtIMC
                txtIMC.Text = imc.ToString("F2");

                string classificacao;

                if (imc < 18.5m)
                    classificacao = "Magreza";
                else if (imc < 25m)
                    classificacao = "Normal";
                else if (imc < 30m)
                    classificacao = "Sobrepeso, Obesidade Grau 1";
                else if (imc < 40m)
                    classificacao = "Obesidade, Grau 2";
                else
                    classificacao = "Obesidade Grave, Grau 3";

                MessageBox.Show("Classificação do IMC: " + classificacao);

            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor, insira valores válidos para peso e altura.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtIMC.Text = string.Empty;
            }
        }

        private void mtxtPeso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',')
            {
                e.Handled = true;
            }

            MaskedTextBox txt = sender as MaskedTextBox;
            if ((e.KeyChar == '.' || e.KeyChar == ',') && (txt.Text.Contains(",") || txt.Text.Contains(".")))
            {
                e.Handled = true;
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtPeso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPeso_TextChanged(object sender, EventArgs e)
        {
            TextBox peso = sender as TextBox;

            peso.TextChanged -= txtPeso_TextChanged;

            // Extract digits only
            string digits = new string(peso.Text.Where(char.IsDigit).ToArray());

            if (string.IsNullOrEmpty(digits))
            {
                peso.Text = "0.00";
                lastValidPesoText = peso.Text;
            }
            else
            {
                // Parse as decimal assuming last two digits are cents
                decimal value = decimal.Parse(digits) / 100m;

                // Only update if value is within allowed range
                if (value <= 999.99m)
                {
                    peso.Text = value.ToString("N2");
                    lastValidPesoText = peso.Text;
                }
                else
                {
                    // Revert to last valid input
                    peso.Text = lastValidPesoText;
                }
            }

            // Restore cursor to end
            peso.SelectionStart = peso.Text.Length;

            peso.TextChanged += txtPeso_TextChanged;

        }

        private void txtAltura_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits and control keys (like Backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtAltura_TextChanged(object sender, EventArgs e)
        {
            TextBox altura = sender as TextBox;

            altura.TextChanged -= txtAltura_TextChanged;

            // Keep only digits
            string digits = new string(altura.Text.Where(char.IsDigit).ToArray());

            if (string.IsNullOrEmpty(digits))
            {
                altura.Text = "0.00";
                lastValidAlturaText = altura.Text;
            }
            else
            {
                // Parse input as decimal with two decimal places
                decimal value = decimal.Parse(digits) / 100m;

                // Limit max height to 2.50 meters
                if (value <= 3.00m)
                {
                    altura.Text = value.ToString("N2");
                    lastValidAlturaText = altura.Text;
                }
                else
                {
                    // Revert to last valid value
                    altura.Text = lastValidAlturaText;
                }
            }

            // Move cursor to end
            altura.SelectionStart = altura.Text.Length;

            altura.TextChanged += txtAltura_TextChanged;
        }

        private void btnLimpa_Click(object sender, EventArgs e)
        {
            txtPeso.Text = "0.00";
            txtAltura.Text = "0.00";
            txtIMC.Text = string.Empty;
        

            // Optionally set focus back to peso or altura
            txtPeso.Focus();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
