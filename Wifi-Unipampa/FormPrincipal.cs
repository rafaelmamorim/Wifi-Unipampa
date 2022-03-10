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
 * Arquivos contidos no resources são do projeto https://github.com/rozmansi/WLANSetEAPUserData
 * 
 */

namespace Wifi_Unipampa
{
    public partial class FormPrincipal : Form
    {
        public string diretorioPrograma { get; set; }
        public string wLanSetupUserDataEXE { get; set; }

        public FormPrincipal()
        {
            InitializeComponent();
            diretorioPrograma =  AppDomain.CurrentDomain.BaseDirectory;
            wLanSetupUserDataEXE = Path.Combine(diretorioPrograma, "WLANSetEAPUserData.exe");
        }

        public Boolean excluirArquivos()
        {
            Boolean saida = false;
            try
            {
                File.Delete(Path.Combine(diretorioPrograma, "user_cred.xml"));
                File.Delete(wLanSetupUserDataEXE);
                saida = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                throw;
            }
            return saida;
        }

        public string mensagemSetEAPUserData(int codigoSaida)
        {
            string saida = "";
            switch (codigoSaida)
            {
                case 0:
                    saida = "Success(on at least one WLAN interface)";
                    break;
                case 100:
                    saida = "CommandLineToArgvW() failed";
                    break;
                case 101:
                    saida = "Not enough arguments";
                    break;
                case 200:
                    saida = "CoInitialize() failed";
                    break;
                case 300:
                    saida = "CoCreateInstance(CLSID_DOMDocument2) failed";
                    break;
                case 301:
                    saida = "IXMLDOMDocument::load() failed";
                    break;
                case 302:
                    saida = "IXMLDOMDocument::load() reported an error in the XML document";
                    break;
                case 304:
                    saida = "IXMLDOMDocument::get_xml() failed";
                    break;
                case 400:
                    saida = "WlanOpenHandle() failed";
                    break;
                case 401:
                    saida = "WlanEnumInterfaces() failed";
                    break;
                case 402:
                    saida = "WlanSetProfileEapXmlUserData() failed on all WLAN interfaces for the given profile";
                    break;
                case 403:
                    saida = "No ready WLAN interfaces found";
                    break;
                default:
                    break;
            }
            return saida;
        }

        private void buttonConfigurar_Click(object sender, EventArgs e)
        {
            excluirArquivos();

            #region  determinando versão do SO  e extraindo executavel do Resources
            if (System.Environment.Is64BitOperatingSystem) 
            {
                File.WriteAllBytes(wLanSetupUserDataEXE, Wifi_Unipampa.Properties.Resources.WLANSetEAPUserDatax64);
            }
            else
            {
                File.WriteAllBytes(wLanSetupUserDataEXE, Wifi_Unipampa.Properties.Resources.WLANSetEAPUserDatax86);
            }
            #endregion

            /* Bloco comentado pois função de gravar no SO usuário e senha não funcionou como deveria
             * Mantido em comentário caso se encontre uma solução
             * if (String.IsNullOrEmpty(textBoxLogin.Text) || String.IsNullOrEmpty(textBoxSenha.Text)){
                MessageBox.Show("É obrigatório preenchimento dos campos usuário " +
                    " institucional e senha", "Campos obrigatórios", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } else {*/

                WlanClient client = new WlanClient();
                if (client.Interfaces.Count() == 0)
                {
                    MessageBox.Show("Nenhum dispositivo de rede sem fio ativo encontrado neste equipamento", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                foreach (WlanClient.WlanInterface wlanIface in client.Interfaces)
                {

                    string nomeRede = "unipampa"; // nome do SSID
                    #region perfilRedeXML
                    string perfilRedeXML =  "<?xml version=\"1.0\"?>" +
                                            "<WLANProfile xmlns=\"http://www.microsoft.com/networking/WLAN/profile/v1\">" +
                                            "	<name>unipampa</name>" +
                                            "	<SSIDConfig>" +
                                            "		<SSID>" +
                                            "			<hex>756E6970616D7061</hex>" +
                                            "			<name>unipampa</name>" +
                                            "		</SSID>" +
                                            "		<nonBroadcast>false</nonBroadcast>" +
                                            "	</SSIDConfig>" +
                                            "	<connectionType>ESS</connectionType>" +
                                            "	<connectionMode>auto</connectionMode>" +
                                            "	<autoSwitch>false</autoSwitch>" +
                                            "	<MSM>" +
                                            "		<security>" +
                                            "			<authEncryption>" +
                                            "				<authentication>WPA2</authentication>" +
                                            "				<encryption>AES</encryption>" +
                                            "				<useOneX>true</useOneX>" +
                                            "			</authEncryption>" +
                                            "			<OneX xmlns=\"http://www.microsoft.com/networking/OneX/v1\">" +
                                            "				<cacheUserData>true</cacheUserData>" +
                                            "				<authMode>user</authMode>" +
                                            "				<EAPConfig>" +
                                            "          <EapHostConfig xmlns=\"http://www.microsoft.com/provisioning/EapHostConfig\">" +
                                            "            <EapMethod>" +
                                            "              <Type xmlns=\"http://www.microsoft.com/provisioning/EapCommon\">25</Type>" +
                                            "              <VendorId xmlns=\"http://www.microsoft.com/provisioning/EapCommon\">0</VendorId>" +
                                            "              <VendorType xmlns=\"http://www.microsoft.com/provisioning/EapCommon\">0</VendorType>" +
                                            "              <AuthorId xmlns=\"http://www.microsoft.com/provisioning/EapCommon\">0</AuthorId>" +
                                            "            </EapMethod><Config xmlns=\"http://www.microsoft.com/provisioning/EapHostConfig\">" +
                                            "              <Eap xmlns=\"http://www.microsoft.com/provisioning/BaseEapConnectionPropertiesV1\"><Type>25</Type><EapType xmlns=\"http://www.microsoft.com/provisioning/MsPeapConnectionPropertiesV1\">" +
                                            "                <ServerValidation>" +
                                            "                <DisableUserPromptForServerValidation>false</DisableUserPromptForServerValidation>" +
                                            "                  <ServerNames></ServerNames>" +
                                            "                </ServerValidation>" +
                                            "                <FastReconnect>true</FastReconnect>" +
                                            "                <InnerEapOptional>false</InnerEapOptional>" +
                                            "                <Eap xmlns=\"http://www.microsoft.com/provisioning/BaseEapConnectionPropertiesV1\"><Type>26</Type><EapType xmlns=\"http://www.microsoft.com/provisioning/MsChapV2ConnectionPropertiesV1\"><UseWinLogonCredentials>false</UseWinLogonCredentials>" +
                                            "                </EapType>" +
                                            "                </Eap>" +
                                            "                <EnableQuarantineChecks>false</EnableQuarantineChecks>" +
                                            "                <RequireCryptoBinding>false</RequireCryptoBinding>" +
                                            "                <PeapExtensions>" +
                                            "                <PerformServerValidation xmlns=\"http://www.microsoft.com/provisioning/MsPeapConnectionPropertiesV2\">false</PerformServerValidation>" +
                                            "              <AcceptServerName xmlns=\"http://www.microsoft.com/provisioning/MsPeapConnectionPropertiesV2\">false</AcceptServerName>" +
                                            "              </PeapExtensions>" +
                                            "              </EapType>" +
                                            "              </Eap>" +
                                            "            </Config>" +
                                            "          </EapHostConfig>" +
                                            "        </EAPConfig>" +
                                            "			</OneX>" +
                                            "		</security>" +
                                            "	</MSM>" +
                                            "</WLANProfile>";
                #endregion

                    #region perfilUsuarioXML
                    /* Bloco comentado pois função de gravar no SO usuário e senha não funcionou como deveria
                     * Mantido em comentário caso se encontre uma solução
                    string perfilUsuarioXML =   "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" +
                                                    "<EapHostUserCredentials xmlns=\"http://www.microsoft.com/provisioning/EapHostUserCredentials\" " +
                                                    "		xmlns:eapCommon=\"http://www.microsoft.com/provisioning/EapCommon\" " +
                                                    "		xmlns:baseEap=\"http://www.microsoft.com/provisioning/BaseEapMethodUserCredentials\">" +
                                                    "	<EapMethod>" +
                                                    "		<eapCommon:Type>21</eapCommon:Type>" +
                                                    "		<eapCommon:AuthorId>311</eapCommon:AuthorId>" +
                                                    "	</EapMethod>" +
                                                    "	<Credentials xmlns=\"http://www.microsoft.com/provisioning/EapHostUserCredentials\">" +
                                                    "		<EapTtls xmlns=\"http://www.microsoft.com/provisioning/EapTtlsUserPropertiesV1\">" +
                                                    "			<Username>"+textBoxLogin.Text+"</Username>" +
                                                    "			<Password>"+textBoxSenha.Text+"</Password>" +
                                                    "		</EapTtls>" +
                                                    "	</Credentials>" +
                                                    "</EapHostUserCredentials>";
                        File.WriteAllText(Path.Combine(diretorioPrograma,"user_cred.xml"), perfilUsuarioXML);*/
                    #endregion

                //removendo redes já cadastradas
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
                            if (rede.profileName == "UNIPAMPA")
                            {
                                wlanIface.DeleteProfile("UNIPAMPA");
                            }
                        }
                    }
                    catch (Exception)
                    {
                        labelSituacao.Text = "Não foi permitido remover a rede unipampa existente";
                        excluirArquivos();
                        return;
                    }

                    /* Bloco comentado pois função de gravar no SO usuário e senha não funcionou como deveria
                    * Mantido em comentário caso se encontre uma solução
                    //registrando usuário e senha nas configurações
                    labelSituacao.Text = "Registrando dados do usuário no Sistema Operacional";
                
                    string userCredParam = nomeRede + " 0 " + Path.Combine(diretorioPrograma, "user_cred.xml") + "/i";
                    Process proc = new Process();
                    proc.StartInfo.FileName = wLanSetupUserDataEXE;
                    proc.StartInfo.Arguments = userCredParam;
                    proc.Start();
                    proc.WaitForExit();
                    int retUsercred = proc.ExitCode;
                    proc.Close();

                    MessageBox.Show(mensagemSetEAPUserData(retUsercred));
                    */

                    //Criando rede nova
                    labelSituacao.Text = "Criando nova rede Unipampa";
                    wlanIface.SetProfile(Wlan.WlanProfileFlags.User, perfilRedeXML, true);

                    //tentando conectar a rede nova
                    labelSituacao.Text = "Tentado conectar a rede Unipampa";
                    wlanIface.Connect(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, nomeRede);

                    if (!excluirArquivos())
                    {
                        MessageBox.Show("Rede configurada com sucesso. Porém, ocorreu um erro ao excluir arquivos temporários. Entre em contato com o STIC do seu Campus", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    } else {
                        MessageBox.Show(" Rede sem fio unipampa configurada com sucesso. \n "+
                            "Ao fazer a primeira conexão à rede unipampa, será " + 
                            "necessário digitar seu usuário e senha institucional.\n\n" + 
                            "Caso necessite trocar a senha, acesse o site http://www.unipampa.edu.br/servicos", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        labelSituacao.Text = "";
                    }
                }
            //}
        }
    

        private void buttonFechar_Click(object sender, EventArgs e)
        {
            excluirArquivos();
            Application.Exit();
        }

        private void buttonAjuda_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://dtic.unipampa.edu.br/redes-wifi/");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            String stringSobre = "Desenvolvido pelo ATI Rafael Amorim (STIC - Campus Santana do Livramento) com auxílio dos colegas listados abaixo:\n\n" +
                "- ATI Angelo Miralha (STIC - Campus Uruguaiana)\n" +
                "- ATI Maurício Fiorenza (DTIC - DIR) \n" +
                "- TTI Wagner Campos (STIC - Campus Santana do Livramento)\n";
            MessageBox.Show(stringSobre, "Sobre este aplicativo",
                MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void FormPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            excluirArquivos();
        }
    }
}
