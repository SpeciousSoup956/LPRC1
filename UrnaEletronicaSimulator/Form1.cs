namespace UrnaEletronicaSimulator
{

    // Classe Form1 (onde est� sua interface e l�gica)
    public partial class Form1 : Form
    {
        // Aqui criamos uma lista de candidatos para simular o banco de dados
        List<Candidato> candidatos = new List<Candidato>
        {
            new Candidato {Numero = "13", Nome = "Jo�o Silva", Partido = "PT"},
            new Candidato {Numero = "22", Nome = "Maria Duarte", Partido = "PL"},
            new Candidato {Numero = "45", Nome = "Ricardo Neves", Partido = "PSDB"}
        };

        // Lista para armazenar os votos
        List<Voto> votos = new List<Voto>();

        // Vari�vel para armazenar o n�mero digitado
        string numeroDigitado = "";

        // Vari�vel para contar d�gitos
        int contadorDigitos = 0;

        public Form1()
        {
            InitializeComponent();
        }
        // Aqui v�o os m�todos de clique, l�gica, etc.

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            Button botao = (Button)sender;
            string numero = botao.Text;

            if (contadorDigitos == 0)
            {
                txtNumero1.Text = numero;
                contadorDigitos++;
            }
            else if (contadorDigitos == 1)
            {
                txtNumero2.Text = numero;
                contadorDigitos++;

                // Agora temos os dois d�gitos, vamos buscar o candidato
                string numeroCompleto = txtNumero1.Text + txtNumero2.Text;

                var candidato = candidatos.FirstOrDefault(c => c.Numero == numeroCompleto);
                if (candidato != null)
                {
                    lblNome.Text = candidato.Nome;
                    lblPartido.Text = candidato.Partido;
                }
                else
                {
                    lblNome.Text = "N�mero inv�lido";
                    lblPartido.Text = "";
                }
            }
        }

        

        private void btnBranco_Click(object sender, EventArgs e)
        {// Verifica se h� d�gitos preenchidos
            if (!string.IsNullOrEmpty(txtNumero1.Text) || !string.IsNullOrEmpty(txtNumero2.Text))
            {
                DialogResult resposta = MessageBox.Show(
                    "Voc� digitou n�meros. Tem certeza que deseja votar em branco?",
                    "Confirma��o de Voto em Branco",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (resposta == DialogResult.No)
                {
                    return; // Cancela o voto em branco
                }
            }

            // Executa o voto em branco
            lblNome.Text = "Voto em Branco";
            lblPartido.Text = "Voto em Branco";
            txtNumero1.Text = "";
            txtNumero2.Text = "";
            contadorDigitos = 0;


        }

        private void btnCorrige_Click(object sender, EventArgs e)
        {
            txtNumero1.Text = "";
            txtNumero2.Text = "";
            lblNome.Text = "";
            lblPartido.Text = "";
            contadorDigitos = 0;


        }

        private void bntConfirma_Click(object sender, EventArgs e)
        {
            string numeroCompleto = txtNumero1.Text + txtNumero2.Text;
            Voto voto = new Voto();

            if (lblNome.Text == "Voto em Branco")
            {
                voto.Numero = "";
                voto.Tipo = "Branco";
            }
            else if (lblNome.Text == "N�mero inv�lido")
            {
                voto.Numero = numeroCompleto;
                voto.Tipo = "Nulo";
            }
            else
            {
                voto.Numero = numeroCompleto;
                voto.Tipo = "Candidato";
            }

            votos.Add(voto);
            MessageBox.Show("Voto confirmado!");
            btnCorrige_Click(sender, e);
        }


        private void bntResultado_Click(object sender, EventArgs e)
        {
         
            var votosCandidatos = votos.Where(v => v.Tipo == "Candidato")
                                       .GroupBy(v => v.Numero)
                                       .Select(g => new
                                       {
                                           Numero = g.Key,
                                           Quantidade = g.Count()
                                       });

            int votosBrancos = votos.Count(v => v.Tipo == "Branco");
            int votosNulos = votos.Count(v => v.Tipo == "Nulo");

            string resultado = "RESULTADO DA VOTA��O:\n\n";

            foreach (var item in votosCandidatos)
            {
                var candidato = candidatos.FirstOrDefault(c => c.Numero == item.Numero);
                string nome = candidato != null ? candidato.Nome : "Desconhecido";
                resultado += $"{nome} ({item.Numero}) - {item.Quantidade} voto(s)\n";
            }

            resultado += $"\nVotos em Branco: {votosBrancos}";
            resultado += $"\nVotos Nulos: {votosNulos}";

            MessageBox.Show(resultado);
        }
    }
    
}
