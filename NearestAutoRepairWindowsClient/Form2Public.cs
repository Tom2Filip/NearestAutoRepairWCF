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
using NearestAutoRepairWindowsClient.NearestServicePublicRef;
using System.ServiceModel.Security;
using System.Configuration;

namespace NearestAutoRepairWindowsClient
{
    public partial class Form2Public : Form
    {
        public Form2Public()
        {
            InitializeComponent();
        }

        private void btnGetPublic_Click(object sender, EventArgs e)
        {
            string userName = null;
            string password = null;
            string result;
            
            userName = ConfigurationManager.AppSettings["username"].ToString();
            password = ConfigurationManager.AppSettings["password"].ToString();
            
            NearestServicePublicRef.NearestAutoRepairServiceClient client = new NearestAutoRepairServiceClient();

            try
            {
                client.ClientCredentials.UserName.UserName = userName;
                client.ClientCredentials.UserName.Password = password;                              
                
                var query = client.GetAllAutoRepairs().FirstOrDefault();
                result = query.Name + "\n" + query.Url;
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

            lbl1.Text = result;
        }
    }
}
