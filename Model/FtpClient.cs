using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebApi.Model
{
    
    #region MyRegion
    public class UsersRootObject
    {
        public List<FtpClient> UserList { get; set; }
    }

    public class FtpClient
    {
        public string Name { get; set; }
        public object Password { get; set; }
        public int StorageUsed { get; set; }
        public int StorageMax { get; set; }
        public object CreationLog { get; set; }
        public object CreationDestinataire { get; set; }
    } 
    #endregion





}
