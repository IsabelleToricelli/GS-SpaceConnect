using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GS_SpaceConnect
{
    /// <summary>
    /// Lógica interna para CadastroPropriedadeAgricola.xaml
    /// </summary>
    public partial class CadastroPropriedadeAgricola : Window
    {
        public CadastroPropriedadeAgricola()
        {
            InitializeComponent();
            cbRegiao.SelectedIndex = 0; // Define a região Sudeste como padrão
        }
        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            //Se o campo de nome estiver vazio, exibe uma mensagem de erro e retorna
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Por favor, insira o nome da propriedade rural.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //Obtém a região selecionada no ComboBox
            var itemSelecionado = cbRegiao.SelectedItem as ComboBoxItem;
            //salva a região selecionada em uma variavel ou define como desconhecida 
            string regiaoSelecionada = itemSelecionado?.Content.ToString() ?? "Desconhecida";

            try
            {  //Chama o método para salvar a propriedade rural no banco de dados

                using (var context = new Data.AppDbContext())
                {
                    var novaPropriedade = new Models.PropriedadeRural
                    {
                        Nome = txtNome.Text.Trim(),
                        Regiao = regiaoSelecionada
                    };
                    context.PropriedadesRurais.Add(novaPropriedade);
                    context.SaveChanges();
                }
                MessageBox.Show("Propriedade rural cadastrada com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true; // Avisa a tela de login que o cadastro deu certo
                this.Close();// Fecha a janela de cadastro
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro ao salvar a propriedade rural: {ex.Message}", "Erro",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
