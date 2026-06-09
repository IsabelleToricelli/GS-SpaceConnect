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
    /// Lógica interna para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            CarregarPropriedadesRurais();

        }

        private void CarregarPropriedadesRurais()
        {
            try
            {
                using(var context = new Data.AppDbContext())
                {
                    var listaFazendas = context.PropriedadesRurais.ToList();

                    if (!listaFazendas.Any())
                    {
                        var fazendaPadrao = new Models.PropriedadeRural
                        {
                            Nome = "Estufa Matriz",
                            Regiao = "Sudeste",
                        };
                        context.PropriedadesRurais.Add(fazendaPadrao);
                        context.SaveChanges();
                        listaFazendas.Add(fazendaPadrao);
                    }
                    cbFazendas.ItemsSource = listaFazendas;
                    cbFazendas.SelectedIndex = 0; 

                }
            }catch(Exception ex)
            {
                MessageBox.Show($"Erro ao carregar as propriedades rurais: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void BtnEntrar_Click(object sender, RoutedEventArgs e)
        {
            var fazendaSelecionada = cbFazendas.SelectedItem as Models.PropriedadeRural;
            if (fazendaSelecionada != null)
            {
                MainWindow telaprincipal = new MainWindow(fazendaSelecionada);
                telaprincipal.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma propriedade rural para entrar.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


                private void BtnCadastrar_Click(object sender, RoutedEventArgs e)
                {
                    CadastroPropriedadeAgricola telaCadastro = new CadastroPropriedadeAgricola();
                    bool? resultado = telaCadastro.ShowDialog();
                    if (resultado == true)
                    {
                        CarregarPropriedadesRurais();
                    }
                }
    }
}
