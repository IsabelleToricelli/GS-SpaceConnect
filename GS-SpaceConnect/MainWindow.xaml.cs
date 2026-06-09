using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GS_SpaceConnect.Models;
using GS_SpaceConnect.Service;

namespace GS_SpaceConnect
{
   
    public partial class MainWindow : Window
    {
        
    
       private Models.PropriedadeRural _fazendaAtiva;

    
       private readonly AnalisadorAgricoloService _analisador;

        public MainWindow()
        {
            InitializeComponent();
            _analisador = new AnalisadorAgricoloService();

        }

        //Controla se esta sendo adicionada uma nova análise ou editando uma análise existente.
        private int idEmEdicao = 0;

       
        public MainWindow(Models.PropriedadeRural fazendaSelecionada)
        {
            InitializeComponent();

            _fazendaAtiva = fazendaSelecionada;
            // Carregar os dados da tabela ao iniciar a aplicação
            _analisador = new AnalisadorAgricoloService();

            AtualizarTabela();
        }

        private void AtualizarTabela()
        {
            if(_fazendaAtiva == null)
            {
                MessageBox.Show("Fazenda ativa não encontrada. Por favor, selecione uma fazenda para carregar os dados.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                using (var context = new Data.AppDbContext())
                {
                    var dadosFiltrados = context.AnaliseAgricolas
                         .Where(a => a.PropriedadeRuralId == _fazendaAtiva.Id)
                         .ToList();

                    dgAnalises.ItemsSource = dadosFiltrados;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar os dados: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        //Ao clicar no botão de Salvar análise, os dados serão salvos no banco de dados
        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            //Validação para não salvar dados vazios
            if (string.IsNullOrWhiteSpace(txtNomeSensor.Text) ||
               string.IsNullOrWhiteSpace(txtTemperatura.Text) ||
               string.IsNullOrWhiteSpace(txtUmidade.Text) ||
               string.IsNullOrWhiteSpace(txtLuminosidade.Text))
            {
                MessageBox.Show("Por favor, preencha todos os campos.", "Atenção",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if(!double.TryParse(txtTemperatura.Text, out double temperatura ) ||
               !double.TryParse(txtUmidade.Text, out double umidade ) ||
               !double.TryParse(txtLuminosidade.Text, out double luminosidade))
            {
                MessageBox.Show("Por favor, insira valores numéricos válidos para Temperatura, Umidade e Luminosidade.", "Erro de Formato", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {

                using (var context = new Data.AppDbContext())
                {
                    if (idEmEdicao == 0)
                    {
                        var novaAnalise = new Models.AnaliseAgricola
                        {
                            NomeSensor = txtNomeSensor.Text,
                            Temperatura = temperatura,
                            Umidade = umidade,
                            Luminosidade = luminosidade,
                            PropriedadeRuralId = _fazendaAtiva.Id
                        };

                        context.AnaliseAgricolas.Add(novaAnalise);
                       
                        MessageBox.Show("Análise salva com sucesso!", 
                            "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        var analiseExistente = context.AnaliseAgricolas.Find(idEmEdicao);

                        if(analiseExistente != null) {
                            analiseExistente.NomeSensor = txtNomeSensor.Text;
                            analiseExistente.Temperatura = temperatura;
                            analiseExistente.Umidade = umidade;
                            analiseExistente.Luminosidade = luminosidade;
                            analiseExistente.PropriedadeRuralId = _fazendaAtiva.Id;

                            context.AnaliseAgricolas.Update(analiseExistente);
                           
                            MessageBox.Show("Análise atualizada com sucesso!", 
                                "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        idEmEdicao = 0;
                    }
                    context.SaveChanges();

                }
             
                //Limpa os campos de texto após salvar
                txtNomeSensor.Clear();
                txtTemperatura.Clear();
                txtUmidade.Clear();
                txtLuminosidade.Clear();

                //Atualiza a tabela para mostrar a nova análise salva
                AtualizarTabela();


            }
           
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar os dados: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }


        //Botão analisar
        private void BtnAnalisar_Click(object sender, RoutedEventArgs e)
        {
            var analiseSelecionada = dgAnalises.SelectedItem as Models.AnaliseAgricola;

            if (analiseSelecionada == null)
            {
                MessageBox.Show("Por favor, selecione uma análise para diagnosticar.",
                    "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                txtDiagnostico.Text = _analisador.ObterDiagnostico(analiseSelecionada,
                    _fazendaAtiva.Regiao);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao obter o diagnóstico: " +
                    $"{ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            //Variável que armazena a linha que o usuário cliclou 
            var selecionado = dgAnalises.SelectedItem as Models.AnaliseAgricola;
            if (selecionado == null)
            {
                MessageBox.Show("Por favor, selecione a análise para editar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            //Insere os dados salvos na caixa de formúlario para o usuário poder alterar
            txtNomeSensor.Text = selecionado.NomeSensor;
            txtTemperatura.Text = selecionado.Temperatura.ToString();
            txtUmidade.Text = selecionado.Umidade.ToString();
            txtLuminosidade.Text = selecionado.Luminosidade.ToString();


            //Guarda o id que está sendo alterado para o botão de salvar saber o que fazer
            idEmEdicao = selecionado.Id;

            MessageBox.Show("Dados carregados no formulário lateral! Modifique os valores desejados e clique em 'Salvar análise'", "Modo de edição", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void BtnDeletar_Click(Object sender, RoutedEventArgs e)
        {

            var selecionado = dgAnalises.SelectedItem as Models.AnaliseAgricola;
            if (selecionado == null)
            {
                MessageBox.Show("Por favor, selecione uma linha na tabela para excluir.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                var resultado = MessageBox.Show("Tem certeza que deseja excluir esta análise?", "Confirmação", 
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (resultado != MessageBoxResult.Yes)
                {
                    return;
                }

                using (var context = new Data.AppDbContext())
                {
                    var entity = context.AnaliseAgricolas
                        .FirstOrDefault(a => a.Id == selecionado.Id);

                    if (entity == null)
                    {
                        MessageBox.Show("Registro não encontrado no banco.", "Erro",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    context.AnaliseAgricolas.Remove(entity);
                    context.SaveChanges();
                }
                MessageBox.Show("Análise excluída com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                AtualizarTabela();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao excluir os dados: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnLimparDiagnostico_Click(Object sender, RoutedEventArgs e)
        {
            txtDiagnostico.Text = string.Empty;
            txtNomeSensor.Clear();
            txtTemperatura.Clear();
            txtUmidade.Clear();
            txtLuminosidade.Clear();

            dgAnalises.SelectedItem = null;

            idEmEdicao = 0; 
        }

        private void BtnVoltarLogin_Click(object sender, RoutedEventArgs e)
        {
            Login telaLogin = new Login();
            telaLogin.Show();
            this.Close();
        }
    }
}