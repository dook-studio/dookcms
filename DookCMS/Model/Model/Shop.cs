using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace Dukey.Model
{
    [Serializable]
    public class Shop
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// CID
        /// </summary>
        public string CID { get; set; }
        /// <summary>
        /// STID
        /// </summary>
        public int STID { get; set; }
        /// <summary>
        /// UserID
        /// </summary>
        public long UserID { get; set; }
        /// <summary>
        /// PerID
        /// </summary>
        public long PerID { get; set; }
        /// <summary>
        /// Nick
        /// </summary>
        public string Nick { get; set; }
        /// <summary>
        /// ShopLevel
        /// </summary>
        public int ShopLevel { get; set; }
        /// <summary>
        /// ShopName
        /// </summary>
        public string ShopName { get; set; }
        /// <summary>
        /// Domain
        /// </summary>
        public string Domain { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// ShopDesc
        /// </summary>
        public string ShopDesc { get; set; }
        /// <summary>
        /// Bulletin
        /// </summary>
        public string Bulletin { get; set; }
        /// <summary>
        /// PicPath
        /// </summary>
        public string PicPath { get; set; }
        /// <summary>
        /// ModifyDate
        /// </summary>
        public DateTime ModifyDate { get; set; }
        /// <summary>
        /// ItemScore
        /// </summary>
        public double ItemScore { get; set; }
        /// <summary>
        /// ServiceScore
        /// </summary>
        public double ServiceScore { get; set; }
        /// <summary>
        /// DeliveryScore
        /// </summary>
        public double DeliveryScore { get; set; }
        /// <summary>
        /// SkinID
        /// </summary>
        public int SkinID { get; set; }
        /// <summary>
        /// Created
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// status
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// Body
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// AreaIDs
        /// </summary>
        public string AreaIDs { get; set; }
        /// <summary>
        /// Locality
        /// </summary>
        public string Locality { get; set; }
        /// <summary>
        /// Address
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// PostCode
        /// </summary>
        public string PostCode { get; set; }
        /// <summary>
        /// Phone
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Nature
        /// </summary>
        public int Nature { get; set; }
        /// <summary>
        /// Supplyer
        /// </summary>
        public int Supplyer { get; set; }
        /// <summary>
        /// IsShopOffline
        /// </summary>
        public bool IsShopOffline { get; set; }
        /// <summary>
        /// IsFactory
        /// </summary>
        public bool IsFactory { get; set; }
        /// <summary>
        /// Operator
        /// </summary>
        public long Operator { get; set; }
        /// <summary>
        /// OpTime
        /// </summary>
        public DateTime OpTime { get; set; }
        /// <summary>
        /// Rank
        /// </summary>
        public int Rank { get; set; }
        /// <summary>
        /// VisitNo
        /// </summary>
        public int VisitNo { get; set; }
        /// <summary>
        /// UpdateTime
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// AttestationPic
        /// </summary>
        public string AttestationPic { get; set; }
        /// <summary>
        /// AttestationState
        /// </summary>
        public int AttestationState { get; set; }
        /// <summary>
        /// ThemeConfig
        /// </summary>
        public string ThemeConfig { get; set; }
        /// <summary>
        /// email
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// qq
        /// </summary>
        public string qq { get; set; }
        /// <summary>
        /// mobile
        /// </summary>
        public string mobile { get; set; }
    }

    [Serializable]
    public class ShopCat
    {
        /// <summary>
        /// ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// CID
        /// </summary>
        public int pid { get; set; }
        /// <summary>
        /// STID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// UserID
        /// </summary>
        public string cname { get; set; }
        /// <summary>
        /// PerID
        /// </summary>
        public string picurl { get; set; }
        /// <summary>
        /// Nick
        /// </summary>
        public int px { get; set; }
        /// <summary>
        /// ShopLevel
        /// </summary>
        public int status { get; set; }    
    }
}
