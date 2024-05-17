﻿using SistemaDeCadastro.Data;
using SistemaDeCadastro.Models;
using System;

namespace SistemaDeCadastro.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {

        private readonly BancoContext _bancoContext;
        public UsuarioRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public UserModel Adicionar(UserModel usuario)
        {
            _bancoContext.Usuarios.Add(usuario);
            _bancoContext.SaveChanges();
            return usuario;
        }

        public UserModel BuscarPorLogin(string login)
        {
            Console.WriteLine(login);
            UserModel usuario = _bancoContext.Usuarios.FirstOrDefault(c => c.Login.ToUpper() == login.ToUpper());
            return usuario;

        }

        public List<UserModel> GetUserList()
        {
            return _bancoContext.Usuarios.ToList();
        }

        public UserModel InfoUsuario(int id)
        {
            var usuario = _bancoContext.Usuarios.FirstOrDefault(c => c.id == id);
            return usuario;
        }
        public UserModel EditarUsuario(UserModel NovoUsuario, int id)
        {
            var usuarioDB = InfoUsuario(id);
            if (usuarioDB == null) throw new Exception("Erro na atualização de dados");

            usuarioDB.name = NovoUsuario.name;
            usuarioDB.Email = NovoUsuario.Email;
            usuarioDB.Login = NovoUsuario.Login;
            usuarioDB.Perfil = NovoUsuario.Perfil;
            usuarioDB.Senha = NovoUsuario.Senha;
            usuarioDB.DataAtualizacao = DateTime.Now;
            
            _bancoContext.Usuarios.Update(usuarioDB);
            _bancoContext.SaveChanges();
            return usuarioDB;
        }
        public bool DeletarUsuario(int id) {
            try
            {
                var usuario = _bancoContext.Usuarios.FirstOrDefault(c => c.id == id);
                if (usuario == null) throw new Exception("Erro ao deletar usuario");
                _bancoContext.Usuarios.Remove(usuario);
                _bancoContext.SaveChanges();
                return true;
            } catch (Exception ex)
            {
                return false;
            }
        }

    }
}