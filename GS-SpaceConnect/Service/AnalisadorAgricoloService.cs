using GS_SpaceConnect.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace GS_SpaceConnect.Service
{
    internal class AnalisadorAgricoloService
    {

        private readonly string _caminhoJson;
        public AnalisadorAgricoloService()
        {
            _caminhoJson = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "regras_agricolas.json");
        }

        public string ObterDiagnostico(AnaliseAgricola analiseSelecionada, string regiaoFazenda)
        {
            if (analiseSelecionada == null)
                return "Nenhuma análise selecionada.";

            if (!File.Exists(_caminhoJson))
                return "Arquivo de regras agrícolas não encontrado.";

            try
            {
                string jsonTexto = File.ReadAllText(_caminhoJson);

                var opcoes = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var regras = JsonSerializer.Deserialize<List<RegraAgricola>>(jsonTexto, opcoes);

                if (regras == null || !regras.Any() )
                    return "Nenhuma regra agrícola encontrada no arquivo.";

                RegraAgricola melhorRegra = null;
                int melhorScore = 0;

                foreach (var regra in regras) {
                    int score = 0;
                    if (!string.IsNullOrWhiteSpace(regiaoFazenda) && regra.Regiao.Equals(regiaoFazenda, StringComparison.OrdinalIgnoreCase))
                    {
                        score += 2;
                    }

                    if (regra.ValidarCondicao(
                        analiseSelecionada.Temperatura,
                        analiseSelecionada.Umidade,
                        analiseSelecionada.Luminosidade)
                        )
                    {
                        score += 3;
                    }
                    if (score > melhorScore)
                    {
                        melhorScore = score;
                        melhorRegra = regra;
                    }

                }
                    if (melhorRegra == null)
                    return "Nenhuma regra agrícola corresponde aos dados da análise selecionada.";

                    return $"[CENÁRIO MAPEADO: {melhorRegra.Cenario}]\n\n{melhorRegra.Recomendacao}"; 
            }
            catch (Exception ex)
            {
                return $"Erro ao processar as regras agrícolas: {ex.Message}";
            }


        }
    }
}
