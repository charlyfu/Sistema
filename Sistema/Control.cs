using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sistema
{
    class Control
    {
        // Aqui se realizan los controles del formulario
        public string ctrlRegistro(Usuarios usuario) 
        {
            // se verifica que no esten vacios los campos
            Modelo modelo = new Modelo();
            string respuesta = "";
            if (string.IsNullOrEmpty(usuario.Usuario) || string.IsNullOrEmpty(usuario.Password) || string.IsNullOrEmpty(usuario.Conpassword) || string.IsNullOrEmpty(usuario.Nombre))
            {
                respuesta = "Debe de llenar todos los campos";
            }else
            {
                if (usuario.Password == usuario.Conpassword)
                {
                    if (modelo.existeUsuario(usuario.Usuario))
                    {
                        respuesta = "El usuario ya existe";
                    }
                    else
                    {
                        usuario.Password = generarSHA1(usuario.Password);
                        usuario.Id_tipo = 2;
                        modelo.registro(usuario);
                    }
                }
                else
                {
                    respuesta = "Las contraseñas no coinciden";
                }
            }
            return respuesta;
        }

        public string ctrlLogin(string usuario, string password) 
        {
            Modelo modelo = new Modelo();
            string respuesta = "";
            Usuarios datosUsuario = null;

            if (string.IsNullOrEmpty(usuario)|| string.IsNullOrEmpty(password))
            {
                respuesta = "Debe llenar todos los campos";
            }
            else
            {
                datosUsuario = modelo.porUsuario(usuario);

                if (datosUsuario == null)
                {
                    respuesta = "El usuario no existe";
                }
                else
                {
                    if (datosUsuario.Password != generarSHA1(password))
                    {
                        respuesta = "El usuario o contraseña no coinciden";
                    }
                    else
                    {
                        Sesion.id = datosUsuario.Id;
                        Sesion.usuario= usuario;
                        Sesion.nombre = datosUsuario.Nombre;
                        Sesion.id_tipo =datosUsuario.Id_tipo;
                    }
                }
            }
            return respuesta;
        }

        private string generarSHA1(string cadena)
        {
            UTF8Encoding enc = new UTF8Encoding();
            byte[] data = enc.GetBytes(cadena);
            byte[] result;

            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();

            result= sha1.ComputeHash(data);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                if (result[i]< 16)
                {
                    sb.Append("0");
                }
                sb.Append(result[i].ToString("x"));
            }
            return sb.ToString();
        }
    }
}
