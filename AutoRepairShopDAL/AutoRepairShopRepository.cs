using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity.Core;
using System.Data.Entity.Spatial;
using AutoRepairShopDTO;

namespace AutoRepairShopDAL
{
    // The class implements the IDisposable interface to ensure that the database connection is released when the object is disposed.
    public class AutoRepairShopRepository : IDisposable, IAutoRepairShopRepository
    {
        private AutoRepairDBEntities context = new AutoRepairDBEntities();
        

        public List<AutoRepairDTO> GetAllAutoRepairs()
        {
            try
            {                
               List<AutoRepairShop> autoRepairShopList = context.AutoRepairShops.ToList();
               // www.c-sharpcorner.com/UploadFile/dhananjaycoder/convert-lista-to-listb-using-convertall-in-C-Sharp/
               List<AutoRepairDTO> autoRepairDTOList = ConvertToDTOList(autoRepairShopList);
              
              return autoRepairDTOList;
            }
            catch (InvalidOperationException IOE)
            {
                // Problem with ConnectionString in config file
                // connectionString="metadata=res://*/[FileName].csdl|res://*/[FileName].ssdl|res://*/[FileName].msl;provider=System.Data.SqlClient;
                //Cannot Create the Connection!
                throw IOE;

            }
            catch (MetadataException ME)
            {
                // Problem with file =>  metadata=res://*/[FileName].csdl
                //Cannot Create the Connection!
                throw ME;

            }
            catch (System.Data.SqlClient.SqlException SqlEx)
            {
                // Inherits from System.Data.Common.DbException
                // Cannot Open the Database!
                throw SqlEx;
            }
            catch (EntityException EE)
            {
                // There was a problem reading data. Try again.
                throw EE;
            }
            catch (DataException DE)
            {
                //Unexpected Data Exception!
                throw DE;
            }
            catch (Exception Ex)
            {
                // Unexpected Error!
                throw Ex;
            }
        }


        // Find users location using Javascript
        // developer.mozilla.org/en-US/docs/Web/API/Geolocation/Using_geolocation
        // www.w3schools.com/html/html5_geolocation.asp
        // developers.google.com/maps/articles/geolocation

        // docs.shopify.com/manual/configuration/store-customization/page-specific/store-wide/get-a-visitors-location
        // www.c-sharpcorner.com/UploadFile/b19d5a/geolocation-in-html5-using-Asp-Net/
       
        // Five (5) nearest Auto Repairs
        public List<AutoRepairDTO> GetFiveNearestAutoRepairs(string userLocation)
        {
            // github.com/techbrij/nearest-places-aspnet-sql-server/blob/master/Geography/Default.aspx.cs
            //var currentLocation = DbGeography.FromText("POINT(" + hdnLocation.Value + ")");
            try
            {                                               //orderby u.Geolocation.Distance(DbGeography.FromText(userLocation))
                List<AutoRepairShop> autoRepairShopList = (from u in context.AutoRepairShops  // userLocation-> two numbers without comma between, only for decimal spot => "45.758041 15.960335"
                                                           orderby u.Geolocation.Distance(DbGeography.FromText(("POINT(" + userLocation + ")")))
                                                           select u).Take(5).ToList();
                            
               List<AutoRepairDTO> autoRepairDTOList = ConvertToDTOList(autoRepairShopList);
               
              return autoRepairDTOList;
            }
            catch (InvalidOperationException IOE)
            {
                // Problem with ConnectionString in config file
                // connectionString="metadata=res://*/[FileName].csdl|res://*/[FileName].ssdl|res://*/[FileName].msl;provider=System.Data.SqlClient;
                //Cannot Create the Connection!
                throw IOE;

            }
            catch (MetadataException ME)
            {
                // Problem with file =>  metadata=res://*/[FileName].csdl
                //Cannot Create the Connection!
                throw ME;

            }
            catch (System.Data.SqlClient.SqlException SqlEx)
            {
                // Inherits from System.Data.Common.DbException
                // Cannot Open the Database!
                throw SqlEx;
            }
            catch (EntityException EE)
            {
                // There was a problem reading data. Try again.
                throw EE;
            }
            catch (DataException DE)
            {
                //Unexpected Data Exception!
                throw DE;
            }
            catch (Exception Ex)
            {
                // Unexpected Error!
                throw Ex;
            }
        }

        //public AutoRepairShop GetNearestAutoRepair(System.Data.Entity.Spatial.DbGeography userLocation)        
        public AutoRepairDTO GetNearestAutoRepair(string userLocation)
        {
            AutoRepairDTO autoRepairDTO = null;
            try
            {
                var place = (from u in context.AutoRepairShops                // userLocation-> two numbers without comma between, only for decimal spot => "45.758041 15.960335"
                             //orderby u.Geolocation.Distance(DbGeography.FromText(userLocation))
                             orderby u.Geolocation.Distance(DbGeography.FromText(("POINT(" + userLocation + ")")))
                             select u).FirstOrDefault();
                
                if (place != null)
                    autoRepairDTO = new AutoRepairDTO()
                    {
                        ID = place.Id,
                        Name = place.Name,
                        Address = place.Address,
                        City = place.City,
                        Phone   = place.Phone,
                        Url = place.Url,
                        //Geolocation = place.Geolocation.ToString()
                        // to get only coordinates, without POINT, SRID and rest 
                        Geolocation = place.Geolocation.Latitude.ToString() + " " + place.Geolocation.Longitude.ToString()
                        //RowVersion = product.RowVersion
                    };

                return autoRepairDTO;
            }
            catch (InvalidOperationException IOE)
            {
                // Problem with ConnectionString in config file
                // connectionString="metadata=res://*/[FileName].csdl|res://*/[FileName].ssdl|res://*/[FileName].msl;provider=System.Data.SqlClient;
                //Cannot Create the Connection!
                throw IOE;

            }
            catch (MetadataException ME)
            {
                // Problem with file =>  metadata=res://*/[FileName].csdl
                //Cannot Create the Connection!
                throw ME;

            }
            catch (System.Data.SqlClient.SqlException SqlEx)
            {
                // Inherits from System.Data.Common.DbException
                // Cannot Open the Database!
                throw SqlEx;
            }
            catch (EntityException EE)
            {
                // There was a problem reading data. Try again.
                throw EE;
            }
            catch (DataException DE)
            {
                //Unexpected Data Exception!
                throw DE;
            }
            catch (Exception Ex)
            {
                // Unexpected Error!
                throw Ex;
            }
        }

        /**/
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
    
    /////////////////////////
        private List<AutoRepairDTO> ConvertToDTOList(List<AutoRepairShop> autoRepairShopList)
        {
        List<AutoRepairDTO> autoRepairDTOList = autoRepairShopList.ConvertAll(x => new AutoRepairDTO
                {
                    ID = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    City = x.City,
                    Phone = x.Phone,
                    Url = x.Url,
                    // to get only coordinates, without POINT, SRID and rest 
                    Geolocation = x.Geolocation.Latitude.ToString() + " " + x.Geolocation.Longitude.ToString()
                    //Geolocation = x.Geolocation.ToString()                    
                    //RowVersion = product.RowVersion                                         
                });

        //List<DbGeography> addresses = autoRepairDTOList.FirstOrDefault().Geolocation.Select(c => DbGeography.FromText(String.Format("POINT ({0} {1})", c.Longitude, c.Latitude)));

                return autoRepairDTOList;
        }
    
    }
}
