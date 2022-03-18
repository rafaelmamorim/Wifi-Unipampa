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
using Wifi_Unipampa.Properties;

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
            WlanClient client = new WlanClient();
            if (client.Interfaces.Count() == 0)
            {
                MessageBox.Show(Resources.mensagemNenhumDispositivoWifi, Resources.tituloMensagemErro, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (WlanClient.WlanInterface wlanIface in client.Interfaces)
            {

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
                labelSituacao.Text = Resources.mensagemRemovendoRedes;
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
                    labelSituacao.Text = Resources.mensagemCurtaErroRemovendoRedes;
                    MessageBox.Show(Resources.mensagemLongaErroRemovendoRedes, Resources.tituloMensagemErro, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //Criando rede nova
                labelSituacao.Text = Resources.mensagemCriandoRede;
                try
                {
                    wlanIface.SetProfile(Wlan.WlanProfileFlags.AllUser, perfilRedeXML, true);
                }
                catch (Exception)
                {
                    labelSituacao.Text = Resources.mensagemCurtaErroCriandoRede;
                    MessageBox.Show(Resources.mensagemLongaErroCriandoRede, Resources.tituloMensagemErro, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //tentando conectar a rede nova
                labelSituacao.Text = Resources.mensagemTentandoConectar;
                try
                {
                    wlanIface.Connect(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, Resources.nomeRedeSemFio);
                }
                catch (Exception)
                {
                    labelSituacao.Text = Resources.mensagemCurtaErroTentandoConectar;
                    MessageBox.Show(Resources.mensagemLongaErroTentandoConectar, Resources.tituloMensagemErro, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }

                MessageBox.Show(Resources.mensagemConfiguracaoConcluida, Resources.tituloMensagemSucesso, MessageBoxButtons.OK, MessageBoxIcon.Information);
                labelSituacao.Text = "";
            }
        }

        private void buttonFechar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonAjuda_Click(object sender, EventArgs e)
        {
            Process.Start("https://sites.unipampa.edu.br/atendimento/redes-wi-fi/");
        }

        private void pictureBoxLogoSTIC_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Resources.mensagemSobreOPrograma, Resources.tituloMensagemSobreOPrograma,
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
