using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;
using NearestAutoRepairWindowsClient.NearestAutoRepairServiceRef3;
using System.ServiceModel.Security;
using System.Configuration;

using System.IdentityModel;
using System.Security.Cryptography.X509Certificates;

// netsh http add sslcert ipport=0.0.0.0:8080 certhash=‎2e19619e857b3a1f818cf29dbbdb4cab0fd595b9 appid={A95E2952-A892-44BF-96C8-762A3111015C}

/*
 * // This code goes to service.cs file -> above method for which we want use SecurityAction.Demand
    using System.Security.Permissions;
    [PrincipalPermission(SecurityAction.Demand, Role="BUILTIN\\BackupOperators")]
    public decimal ReportSales()
 * ******
 In this code, you are using the PrincipalPermission attribute to demand that users belong to a specific role in order to execute the code in the ReportSales method. 
 * For the purposes of this demo, use a role to which you do not belong.
 If you call this method and are not in the Windows built-in BackupOperators group, a security exception will occur. You will next trap for this in the client application. 
 * In the Solution Explorer, right-click the Form1 file and select View Code. Add the following to the top of the code file:
 
 * using System.ServiceModel.Security; 
 try
{
  MessageBox.Show(String.Format("Today's sales are {0:C}", proxy.ReportSales()));
}
catch (SecurityAccessDeniedException securityEx)
{
  MessageBox.Show(securityEx.Message);
}
catch (FaultException faultEx)
{
  MessageBox.Show(faultEx.Message);
}
 
 */

namespace NearestAutoRepairWindowsClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGetRepairs_Click(object sender, EventArgs e)
        {
            string result;


            object locationCertificate = "wcfclient";

            X509Store storeLocalMachine = new X509Store(StoreName.TrustedPeople, StoreLocation.CurrentUser);
            storeLocalMachine.Open(OpenFlags.ReadOnly);

            X509Certificate2Collection certColl = storeLocalMachine.Certificates.Find(X509FindType.FindBySubjectName, locationCertificate, false);

            result = certColl.ToString();
            



            string userName = null;
            string password = null;
            

            NearestAutoRepairServiceRef3.NearestAutoRepairServiceClient client = new NearestAutoRepairServiceRef3.NearestAutoRepairServiceClient();
            
            try
            {

                /*
                // www.nikola-breznjak.com/blog/c/how-to-use-app-config-in-visual-studio-c-net/
                userName = ConfigurationManager.AppSettings["username"].ToString();
                password = ConfigurationManager.AppSettings["password"].ToString();

                // Get the name of the user running the application        // to strip off the domain-> .Split('\\').Last()
               // userName2 = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\').Last();
                
                // client.ClientCredentials.UserName.UserName = "Test";
                client.ClientCredentials.UserName.UserName = userName;
                client.ClientCredentials.UserName.Password = password;
                
                var query = client.GetAllAutoRepairs();
                result = client.GetAllAutoRepairs().FirstOrDefault().Name;
               */

                /*
                foreach (var item in query)
                {
                    item.Address.Trim();
                    item.City.Trim();
                    item.Geolocation.Trim();
                    item.Name.Trim();
                    item.Phone.Trim();
                    item.Url.Trim();                    
                }
                */                

            /*
              // This is request !!
              var query2 =  client.GetAllAutoRepairsRequest("License Key", "45 16").FirstOrDefault();
              result = query2.Name + "\n" + query2.Address + "\n" + "This is GetAllAutoRepairsRequest";
            */

                /*
              // This is request !!
              var query3 = client.GetFiveNearestAutoRepairRequest("License Key", "48 14").FirstOrDefault();
              result = query3.Name + "\n" + query3.Address + "\n" + "This is GetFiveNearestAutoRepairRequest";
            */
                /*
              // This is request !!
              string loc = "45 17";
              string name = "Name";
              string address = "Address";
              string city = "City";
              string phonenr = "01344";
              string www = "Website";
              var query4 = client.GetNearestAutoRepairRequest("License Key", ref loc, out name, out address, out city, out phonenr, out www);
              result = query4.GetType() + "\n" + query4.GetTypeCode() + "\n" + query4.ToString() + "\n";
              result += name + " " + address + " " + www + " " + phonenr + " " + city + " " + loc;
                */                
            }            
            catch (TimeoutException ex)
            {
                result = "The service operation timed out. " + ex.Message;
            }
            catch (MessageSecurityException)
            {
                result = "Authentication failed: " + "You do not have permission to call this service. Check your UserName and Password."; // +msgEx.ToString();
            }
            catch (SecurityAccessDeniedException securityEx)
            {
                // SecurityAccessDeniedException is thrown if user isn't in Role that has permission to call this operation/method
                result = "SecurityAccessDeniedException: " + securityEx.ToString();
            }
            catch (FaultException<AutoRepairFault> ex)
            {
                result = "AutoRepairFault: " + ex.Detail.ErrorMessage;
            }
            catch (FaultException ex)
            {
                result = "Unknown Fault: " + ex.ToString();
            }                                        
            catch (CommunicationException ex)
            {
                // If Sessions are used
                // csharp-video-tutorials.blogspot.hr/2014/03/part-39-persession-wcf-services_1.html
                if (client.State == CommunicationState.Faulted)
                {
                   //client = new NearestAutoRepairServiceClient();
                    result = "Session timed out. Your existing session will be lost. A new session will now be created";                    
                }
                else
                {
                    result = "There was a communication problem. " + ex.Message + ex.StackTrace;
                }                
            }
            catch (Exception ex)
            {
                result = "Other exception: " + ex.Message;
            }

            lbl1.Text = result.Trim() + "\n" + "Hello user: " + userName + "!"; 
        }


    }
}
