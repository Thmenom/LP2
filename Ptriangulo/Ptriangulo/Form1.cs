using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ptriangulo
{
    public partial class frmTriangulo : Form
    {
        private void OnlyAllowNumbers(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;

            // Allow control characters (like Backspace)
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            // Allow only one '.' (decimal point)
            if (e.KeyChar == '.')
            {
                if (txt.Text.Contains('.'))
                {
                    e.Handled = true; // Block second decimal point
                }
                return;
            }

            // Block if not a digit
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public frmTriangulo()
        {
            InitializeComponent();

            txtA.KeyPress += OnlyAllowNumbers;
            txtB.KeyPress += OnlyAllowNumbers;
            txtC.KeyPress += OnlyAllowNumbers;
         
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtA_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            // Tenta converter os valores digitados
            if (!double.TryParse(txtA.Text, out double a) ||
                !double.TryParse(txtB.Text, out double b) ||
                !double.TryParse(txtC.Text, out double c))
            {
                MessageBox.Show("Por favor, insira valores numéricos válidos em todos os campos.", "Entrada inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verifica se todos os lados são positivos
            if (a <= 0 || b <= 0 || c <= 0)
            {
                MessageBox.Show("Os comprimentos dos lados devem ser maiores que zero.", "Entrada inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verifica a desigualdade triangular
            if (a + b > c && a + c > b && b + c > a)
            {
                string tipoTriangulo;

                if (a == b && b == c)
                {
                    tipoTriangulo = "Triângulo Equilátero (todos os lados são iguais)";
                }
                else if (a == b || a == c || b == c)
                {
                    tipoTriangulo = "Triângulo Isósceles (dois lados são iguais)";
                }
                else
                {
                    tipoTriangulo = "Triângulo Escaleno (todos os lados são diferentes)";
                }

                MessageBox.Show($"Os valores PODEM formar um triângulo.\n\nTipo: {tipoTriangulo}", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Os valores NÃO PODEM formar um triângulo.", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtA.Text = "";
            txtB.Text = "";
            txtC.Text = "";

            // Opcional: coloca o foco no primeiro campo
            txtA.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
