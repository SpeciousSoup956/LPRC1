using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculadoraIMC
{
    public partial class calculadoraIMC : Form
    {
        public calculadoraIMC()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                CalcularIMC();
            }

        }
        private bool ValidarCampos()
        {
            // Validação do nome
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Por favor, informe o nome.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Focus();
                return false;
            }

            // Validação da idade
            if (!int.TryParse(txtIdade.Text, out int idade) || idade <= 0 || idade > 150)
            {
                MessageBox.Show("Por favor, informe uma idade válida (1-150).", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtIdade.Focus();
                return false;
            }

            // Validação do peso
            if (!double.TryParse(txtPeso.Text, out double peso) || peso <= 0 || peso > 500)
            {
                MessageBox.Show("Por favor, informe um peso válido (ex: 70.5).", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPeso.Focus();
                return false;
            }

            // Validação da altura
            if (!double.TryParse(txtAltura.Text, out double altura) || altura <= 0 || altura > 3)
            {
                MessageBox.Show("Por favor, informe uma altura válida em metros (ex: 1.75).", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAltura.Focus();
                return false;
            }

            return true;
        }
        private void CalcularIMC()
        {
            double peso = Convert.ToDouble(txtPeso.Text);
            double altura = Convert.ToDouble(txtAltura.Text);

            // Cálculo do IMC: peso / (altura * altura)
            double imc = peso / (altura * altura);

            // Exibir resultados
            lblIMC.Text = $"IMC: {imc:F2}";
            lblSituacao.Text = $"Situação: {ClassificarIMC(imc)}";

            groupBoxResultado.Visible = true;
        }

        private string ClassificarIMC(double imc)
        {
            if (imc < 18.5)
                return "Abaixo do peso";
            else if (imc < 25)
                return "Peso normal";
            else if (imc < 30)
                return "Sobrepeso";
            else if (imc < 35)
                return "Obesidade Grau I";
            else if (imc < 40)
                return "Obesidade Grau II";
            else
                return "Obesidade Grau III";
        }
        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void LimparCampos()
        {
            txtNome.Clear();
            txtIdade.Clear();
            txtPeso.Clear();
            txtAltura.Clear();
            groupBoxResultado.Visible = false;
            txtNome.Focus();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Configurações iniciais
            groupBoxResultado.Visible = false;
        }

        private void calculadoraIMC_Load(object sender, EventArgs e)
        {

        }

        private void txtIdade_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir apenas números e backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void txtPeso_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, vírgula e backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;
            }

            // Permitir apenas uma vírgula
            if (e.KeyChar == ',' && ((TextBox)sender).Text.Contains(","))
            {
                e.Handled = true;
            }
        }

        private void txtAltura_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, vírgula e backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;
            }

            // Permitir apenas uma vírgula
            if (e.KeyChar == ',' && ((TextBox)sender).Text.Contains(","))
            {
                e.Handled = true;
            }
        }

        private void btnCalcular_Clic(object sender, EventArgs e)
        {

            if (ValidarCampos())
            {
                CalcularIMC();
            }

        }
    }
}

    

