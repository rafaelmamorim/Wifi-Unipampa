using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NativeWifi;
using System.Reflection;

/***
 * Configurador de Rede Wifi Unipampa
 * 
 * @author Rafael Amorim <rafaelamorim@unipampa.edu.br>
 * @link https://www.unipampa.edu.br/livramento
 * @license GPL v3
 * @year 2022
 * 
 */

namespace Wifi_Unipampa
{
    public partial class FormPrincipal : Form
    {
        public string diretorioPrograma { get; set; }

        public FormPrincipal()
        {
            InitializeComponent();
            diretorioPrograma = AppDomain.CurrentDomain.BaseDirectory;
        }

        private void buttonConfigurar_Click(object sender, EventArgs e)
        {

            #region  determinando versão do SO  e extraindo executavel do Resources
            /*if (System.Environment.Is64BitOperatingSystem) 
            {
                File.WriteAllBytes(wLanSetupUserDataEXE, Wifi_Unipampa.Properties.Resources.WLANSetEAPUserDatax64);
            }
            else
            {
                File.WriteAllBytes(wLanSetupUserDataEXE, Wifi_Unipampa.Properties.Resources.WLANSetEAPUserDatax86);
            }*/
            #endregion

            WlanClient client = new WlanClient();
            if (client.Interfaces.Count() == 0)
            {
                MessageBox.Show("Nenhum dispositivo de rede sem fio ativo encontrado neste equipamento", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (WlanClient.WlanInterface wlanIface in client.Interfaces)
            {

                string nomeRede = "unipampa";
                #region perfilRedeXML
                string perfilRedeXML = "<?xml version=\"1.0\"?>" +
                                        "<WLANProfile xmlns=\"http://www.microsoft.com/networking/WLAN/profile/v1\">" +
                                        "    <name>unipampa</name>" +
                                        "    <SSIDConfig>" +
                                        "        <SSID>" +
                                        "            <hex>756E6970616D7061</hex>" +
                                        "            <name>unipampa</name>" +
                                        "        </SSID>" +
                                        "        <nonBroadcast>false</nonBroadcast>" +
                                        "    </SSIDConfig>" +
                                        "    <connectionType>ESS</connectionType>" +
                                        "    <connectionMode>auto</connectionMode>" +
                                        "    <autoSwitch>false</autoSwitch>" +
                                        "    <MSM>" +
                                        "        <security>" +
                                        "            <authEncryption>" +
                                        "                <authentication>WPA2</authentication>" +
                                        "                <encryption>AES</encryption>" +
                                        "                <useOneX>true</useOneX>" +
                                        "            </authEncryption>" +
                                        "            <OneX xmlns=\"http://www.microsoft.com/networking/OneX/v1\">" +
                                        "                <cacheUserData>true</cacheUserData>" +
                                        "                <authMode>user</authMode>" +
                                        "                <EAPConfig>" +
                                        "                    <EapHostConfig xmlns=\"http://www.microsoft.com/provisioning/EapHostConfig\">" +
                                        "                        <EapMethod>" +
                                        "                            <Type xmlns=\"http://www.microsoft.com/provisioning/EapCommon\">25</Type>" +
                                        "                            <VendorId xmlns=\"http://www.microsoft.com/provisioning/EapCommon\">0</VendorId>" +
                                        "                            <VendorType xmlns=\"http://www.microsoft.com/provisioning/EapCommon\">0</VendorType>" +
                                        "                            <AuthorId xmlns=\"http://www.microsoft.com/provisioning/EapCommon\">0</AuthorId>" +
                                        "                        </EapMethod>" +
                                        "                        <Config xmlns=\"http://www.microsoft.com/provisioning/EapHostConfig\">" +
                                        "                            <Eap xmlns=\"http://www.microsoft.com/provisioning/BaseEapConnectionPropertiesV1\">" +
                                        "                                <Type>25</Type>" +
                                        "                                <EapType xmlns=\"http://www.microsoft.com/provisioning/MsPeapConnectionPropertiesV1\">" +
                                        "                                    <ServerValidation>" +
                                        "                                        <DisableUserPromptForServerValidation>false</DisableUserPromptForServerValidation>" +
                                        "                                        <ServerNames></ServerNames>" +
                                        "                                    </ServerValidation>" +
                                        "                                    <FastReconnect>true</FastReconnect>" +
                                        "                                    <InnerEapOptional>false</InnerEapOptional>" +
                                        "                                    <Eap xmlns=\"http://www.microsoft.com/provisioning/BaseEapConnectionPropertiesV1\">" +
                                        "                                        <Type>26</Type>" +
                                        "                                        <EapType xmlns=\"http://www.microsoft.com/provisioning/MsChapV2ConnectionPropertiesV1\">" +
                                        "                                            <UseWinLogonCredentials>false</UseWinLogonCredentials>" +
                                        "                                        </EapType>" +
                                        "                                    </Eap>" +
                                        "                                    <EnableQuarantineChecks>false</EnableQuarantineChecks>" +
                                        "                                    <RequireCryptoBinding>false</RequireCryptoBinding>" +
                                        "                                    <PeapExtensions>" +
                                        "                                        <PerformServerValidation xmlns=\"http://www.microsoft.com/provisioning/MsPeapConnectionPropertiesV2\">false</PerformServerValidation>" +
                                        "                                        <AcceptServerName xmlns=\"http://www.microsoft.com/provisioning/MsPeapConnectionPropertiesV2\">false</AcceptServerName>" +
                                        "                                        <PeapExtensionsV2 xmlns=\"http://www.microsoft.com/provisioning/MsPeapConnectionPropertiesV2\">" +
                                        "                                                <AllowPromptingWhenServerCANotFound xmlns=\"http://www.microsoft.com/provisioning/MsPeapConnectionPropertiesV3\">true</AllowPromptingWhenServerCANotFound>" +
                                        "                                        </PeapExtensionsV2 >" +
                                        "                                    </PeapExtensions>" +
                                        "                                </EapType>" +
                                        "                            </Eap>" +
                                        "                        </Config>" +
                                        "                    </EapHostConfig>" +
                                        "                </EAPConfig>" +
                                        "            </OneX>" +
                                        "        </security>" +
                                        "    </MSM>" +
                                        "</WLANProfile>";
                #endregion

                //removendo redes unipampa cadastradas
                labelSituacao.Text = "Excluindo redes Unipampa existentes";
                try
                {
                    Wlan.WlanProfileInfo[] redesRegistradas = wlanIface.GetProfiles();
                    foreach (Wlan.WlanProfileInfo rede in redesRegistradas)
                    {
                        if (rede.profileName == "unipampa")
                        {
                            wlanIface.DeleteProfile("unipampa");
                        }
                        if (rede.profileName == "Unipampa")
                        {
                            wlanIface.DeleteProfile("Unipampa");
                        }
                        if (rede.profileName == "UNIPAMPA")
                        {
                            wlanIface.DeleteProfile("UNIPAMPA");
                        }
                    }
                }
                catch (Exception)
                {
                    labelSituacao.Text = "Não foi permitido remover a rede unipampa existente";
                    return;
                }

                //Criando rede nova
                labelSituacao.Text = "Criando nova rede Unipampa";
                wlanIface.SetProfile(Wlan.WlanProfileFlags.AllUser, perfilRedeXML, true);

                //tentando conectar a rede nova
                labelSituacao.Text = "Tentado conectar a rede Unipampa";
                wlanIface.Connect(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, nomeRede);

                MessageBox.Show("Rede sem fio unipampa configurada com sucesso. \n " +
                    "Ao fazer a primeira conexão à rede unipampa, será " +
                    "necessário digitar seu usuário e senha institucional.\n\n" +
                    "Caso necessite trocar a senha, acesse o site http://www.unipampa.edu.br/servicos", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                labelSituacao.Text = "";
            }
        }


        private void buttonFechar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonAjuda_Click(object sender, EventArgs e)
        {
            Process.Start("https://dtic.unipampa.edu.br/redes-wifi/");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            String stringSobre = "Desenvolvido pelo ATI Rafael Amorim (STIC - Campus Santana do Livramento) com auxílio dos colegas listados abaixo:\n\n" +
                "- ATI Angelo Miralha (STIC - Campus Uruguaiana)\n" +
                "- ATI Carlos André da Silva(STIC - Campus Dom Pedrito)\n" +
                "- ATI Maurício Fiorenza (DTIC - DIR) \n" +
                "- TTI Wagner Campos (STIC - Campus Santana do Livramento)\n";
            MessageBox.Show(stringSobre, "Sobre este aplicativo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
