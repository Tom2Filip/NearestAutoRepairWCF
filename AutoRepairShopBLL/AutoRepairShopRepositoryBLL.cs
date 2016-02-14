using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoRepairShopDTO;
using AutoRepairShopDAL;

namespace AutoRepairShopBLL
{
    public class AutoRepairShopRepositoryBLL : IDisposable
    {
        private AutoRepairShopRepository context = new AutoRepairShopRepository();

        public List<AutoRepairDTO> GetAllAutoRepairs()
        {
            try
            {
                var query = context.GetAllAutoRepairs();
                return query;
            }
            catch (InvalidOperationException IOE)
            {
                // Problem with ConnectionString in config file
                // connectionString="metadata=res://*/[FileName].csdl|res://*/[FileName].ssdl|res://*/[FileName].msl;provider=System.Data.SqlClient;
                //Cannot Create the Connection!
                throw IOE;

            }
            catch (System.Data.SqlClient.SqlException SqlEx)
            {
                // Inherits from System.Data.Common.DbException
                // Cannot Open the Database!
                throw SqlEx;
            }
            catch (Exception Ex)
            {
                // Unexpected Error!
                if (Ex.InnerException!=null)
                {
                    Ex = Ex.InnerException;
                }
                throw Ex;
            }            
        }

        public List<AutoRepairDTO> GetFiveNearestAutoRepairs(string userLocation)
        {
            try
            {
                var query = context.GetFiveNearestAutoRepairs(userLocation);
                return query;
            }
            catch (InvalidOperationException IOE)
            {
                // Problem with ConnectionString in config file
                // connectionString="metadata=res://*/[FileName].csdl|res://*/[FileName].ssdl|res://*/[FileName].msl;provider=System.Data.SqlClient;
                //Cannot Create the Connection!
                throw IOE;

            }
            catch (System.Data.SqlClient.SqlException SqlEx)
            {
                // Inherits from System.Data.Common.DbException
                // Cannot Open the Database!
                throw SqlEx;
            }
            catch (Exception Ex)
            {
                // Unexpected Error!
                if (Ex.InnerException != null)
                {
                    Ex = Ex.InnerException;
                }
                throw Ex;
            }        
        }

        public AutoRepairDTO GetNearestAutoRepair(string userLocation)
        {
            try
            {
                var query = context.GetNearestAutoRepair(userLocation);
                return query;
            }
            catch (InvalidOperationException IOE)
            {
                // Problem with ConnectionString in config file
                // connectionString="metadata=res://*/[FileName].csdl|res://*/[FileName].ssdl|res://*/[FileName].msl;provider=System.Data.SqlClient;
                //Cannot Create the Connection!
                throw IOE;

            }
            catch (System.Data.SqlClient.SqlException SqlEx)
            {
                // Inherits from System.Data.Common.DbException
                // Cannot Open the Database!
                throw SqlEx;
            }
            catch (Exception Ex)
            {
                // Unexpected Error!
                if (Ex.InnerException != null)
                {
                    Ex = Ex.InnerException;
                }
                throw Ex;
            }            
        }
        
        // Dispose
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
