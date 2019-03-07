using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using CondoMinicator.Models;

namespace CondoMinicator.DAL
{
    public class CondoDBInitializer : System.Data.Entity.DropCreateDatabaseAlways<CondoDBContext>
    {
        protected override void Seed(CondoDBContext context)
        {
            var usuario_tipos = new List<Usuario_Tipo>
            {
                new Usuario_Tipo{ ID=1, Tipo="Morador"},
                new Usuario_Tipo{ ID=2, Tipo="Sindico"},
                new Usuario_Tipo{ ID=3, Tipo="Administradora"},
                new Usuario_Tipo{ ID=4, Tipo="Zelador"},
            };

            usuario_tipos.ForEach(s => context.Usuario_Tipos.Add(s));
            context.SaveChanges();

            var assuntos = new List<Assunto>
            {
                new Assunto{ ID=1, Tipo="Administrativo"},
                new Assunto{ ID=2, Tipo="Condominial"},
            };

            assuntos.ForEach(s => context.Assuntos.Add(s));
            context.SaveChanges();

            var administradora = new List<Administradora>
            {
                new Administradora{ ID=1, Nome="Galante Imóveis"},
                new Administradora{ ID=2, Nome="Ivo Adm Imóveis"},
                new Administradora{ ID=3, Nome="Guastelli Adm Imóveis"},
            };

            administradora.ForEach(s => context.Administradoras.Add(s));
            context.SaveChanges();

            var condominio = new List<Condominio>
            {
                new Condominio{ ID=1, Nome="Ed. Erika Maria", AdministradoraID=1, Usuario_TipoID=2},
                new Condominio{ ID=2, Nome="Cond. das Rosas", AdministradoraID=2, Usuario_TipoID=2},
                new Condominio{ ID=3, Nome="Cond. Ana Bolena", AdministradoraID=3, Usuario_TipoID=2},
            };

            condominio.ForEach(s => context.Condominios.Add(s));
            context.SaveChanges();

            var usuarios = new List<Usuario>
            {
                new Usuario{ ID=1, Nome="Juliano", Email="juliano@gmail.com.br", CondominioID=1, Usuario_TipoID=2},
                new Usuario{ ID=2, Nome="Seu Zé", Email="seu_ze2000@gmail.com.br", CondominioID=1, Usuario_TipoID=4},
                new Usuario{ ID=3, Nome="Dona Maria", Email="dmaria@gmail.com.br", CondominioID=1, Usuario_TipoID=1},
                new Usuario{ ID=4, Nome="Dona Florinda", Email="floflo@gmail.com.br", CondominioID=1, Usuario_TipoID=1},
                new Usuario{ ID=5, Nome="Ednilson", Email="ednilson@gmail.com.br", CondominioID=1, Usuario_TipoID=3},
                new Usuario{ ID=6, Nome="João", Email="joao@gmail.com.br", CondominioID=2, Usuario_TipoID=1},
                new Usuario{ ID=7, Nome="José", Email="jose@gmail.com.br", CondominioID=2, Usuario_TipoID=1},
                new Usuario{ ID=8, Nome="Joaquim", Email="joao@gmail.com.br", CondominioID=2, Usuario_TipoID=4},
                new Usuario{ ID=9, Nome="Raquel", Email="raquel@gmail.com.br", CondominioID=3, Usuario_TipoID=2},
                new Usuario{ ID=10, Nome="Geruza", Email="geruza@gmail.com.br", CondominioID=3, Usuario_TipoID=3},
                new Usuario{ ID=11, Nome="Dona Clotilde", Email="bruxa_71@gmail.com.br", CondominioID=3, Usuario_TipoID=1},
            };

            usuarios.ForEach(s => context.Usuarios.Add(s));
            context.SaveChanges();


        }
    }
}