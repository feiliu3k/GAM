using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAM.Models
{
    public class User
    {      
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }       
        public DateTime CreateAt { get; set; }
        public bool IsActive { get; set; }        
    }

    public class EnterpriseType
    {
        public int Id { get; set; }
        public string Typename { get; set; }
        public string Remark { get; set; }        
        public bool IsDelflag { get; set; }
    }

    public class EnterpriseCategory
    {
        public int Id { get; set; }
        public string Catename { get; set; }
        public string Remark { get; set; }
        public bool IsDelflag { get; set; }
    }


    public class Area
    {
        public int Id { get; set; }
        public string Areaname { get; set; }
        public string Remark { get; set; }
        public bool IsDelflag { get; set; }
    }
    public class CommonMan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birth { get; set; }
        public string Sex { get; set; }       
        public string Addr { get; set; }
        public string Phone { get; set; }
        public string Remark { get; set; }
        public bool IsDelflag { get; set; }
    }

    public class ChargeMan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birth { get; set; }
        public string Sex { get; set; }
        public string Addr { get; set; }
        public string Phone { get; set; }
        public string Remark { get; set; }
        public bool IsDelflag { get; set; }

    }

    public class Enterprise
    {
        public int Id { get; set; }
        public string Eid { get; set; }        
        public string Name { get; set; }
        public string Addr { get; set; }
        public string Catename { get; set; }
        public string Typename { get; set; }
        public string Areaname { get; set; }
        public string Apname { get; set; }
        public string Aptelphone { get; set; }
        public string Apcellphone { get; set; }
        public DateTime Createat { get; set; }
        public string Remark { get; set; }
        public bool IsDelflag { get; set; }

    }

    public class DbEntityEnterprise
    {
        public int Id { get; set; }
        public string Eid { get; set; }
        public string Name { get; set; }
        public string Addr { get; set; }
        public int CateId { get; set; }
        public int TypeId { get; set; }
        public int AreaId { get; set; }
        public string Apname { get; set; }
        public string Aptelphone { get; set; }
        public string Apcellphone { get; set; }
        public DateTime Createat { get; set; }
        public string Remark { get; set; }
        public bool IsDelflag { get; set; }
    }

    public class CheckAction
    {
        public List<CommonMan> choiceCommonMen { get; set; }
        public List<ChargeMan> choiceChargeMen { get; set; }
        public Area choiceArea { get; set; }
        public EnterpriseCategory choiceCate { get; set; }
        public List<DbEntityEnterprise> choiceEnts { get; set; }
    }
}
