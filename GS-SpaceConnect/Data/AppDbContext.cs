using GS_SpaceConnect.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS_SpaceConnect.Data
{
    public class AppDbContext : DbContext
    {
        //DbContexto traduz comandos em c# para SQL
        //Tabela do MySQL
        public DbSet<AnaliseAgricola> AnaliseAgricolas { get; set; }

        public DbSet<PropriedadeRural> PropriedadesRurais { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //String de conexão com o MySQL
            string conexao = "Server = localhost;Database=Gs_SpaceConnect;User=root;Password=1234;";
            
            optionsBuilder.UseMySql(conexao, ServerVersion.AutoDetect(conexao));
        }

    }
}
