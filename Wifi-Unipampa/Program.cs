using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wifi_Unipampa
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            #region Extraindo DLL para execução da aplicação
            try
            {
                File.WriteAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ManagedWifi.dll"), Wifi_Unipampa.Properties.Resources.ManagedWifi);
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro ao descompactar o arquivo ManagedWifi.dll. Tente novamente ou entre em contato com o STIC do Campus para configurar seu equipamento", "Erro ao descompactar DLL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormPrincipal());
        }
    }
}
