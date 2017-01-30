using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace FEncrypt
{
    public class EncryptDncrypt
    {
        public static string prueba(string cadena)
        {
            return "Exito " + cadena; 
        }

        ///<summary>
        /// Steve Lydford - 12/05/2008.
        ///
        /// Encrypts a file using Rijndael algorithm.
        ///</summary>
        ///<param name="inputFile"></param>
        ///<param name="outputFile"></param>
        public static Respuesta EncryptFile(string password, string contenido, string outputFile)
        {
            // inicia el objeto que regresara una respuesta
            Respuesta result = new Respuesta();

            try
            {

                // codificar los caracteres de contraseña y el vector de inicializacion (IV)
                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);

                string cryptFile = outputFile;
                FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

                // instancia el algoritmo de encriptacion Rijndael 
                RijndaelManaged RMCrypto = new RijndaelManaged();

                // define la llave codificada como acceso al algoritmo
                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateEncryptor(key, key),
                    CryptoStreamMode.Write);
                
                // vacia el contenido dentro de archivo a crear
                byte[] toBytes = Encoding.ASCII.GetBytes(contenido);

                foreach (byte b in toBytes)
                    cs.WriteByte((byte)b);

                cs.Close();
                fsCrypt.Close();

                result.status = Estatus.OK;
            }
            catch (Exception ex)
            {
                result.status = Estatus.ERROR;
                result.error = ex.Message;
            }

            return result;
        }

        ///<summary>
        /// Steve Lydford - 12/05/2008.
        ///
        /// Decrypts a file using Rijndael algorithm.
        ///</summary>
        ///<param name="inputFile"></param>
        ///<param name="outputFile"></param>
        public static Respuesta DecryptFile(string inputFile, string password)
        {
            // inicia el objeto que regresara una respuesta
            Respuesta result = new Respuesta();

            try
            {

                // codificar los caracteres de contraseña y el vector de inicializacion (IV)
                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);

                FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

                // instancia el algoritmo de encriptacion Rijndael 
                RijndaelManaged RMCrypto = new RijndaelManaged();

                // define la llave codificada como acceso al algoritmo
                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateDecryptor(key, key),
                    CryptoStreamMode.Read);

                // obtiene el contenido del archivo
                string cadena = string.Empty;

                int data;
                while ((data = cs.ReadByte()) != -1)
                    cadena += System.Text.Encoding.ASCII.GetString(new[] { (byte)data });

                result.status = Estatus.OK;
                result.resultado = cadena;
                
                cs.Close();
                fsCrypt.Close();

            }
            catch (Exception ex)
            {
                result.status = Estatus.ERROR;
                result.error = ex.Message;
            }

            return result;
        }
    }
}
