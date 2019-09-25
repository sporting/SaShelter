using SportingAppFW.Data.Common;
using SportingAppFW.Data.Common.DB.Sqlite;
using SportingAppFW.SaWindows.Data;
using System.Data;

namespace ShelterLib
{
    /// <summary>
    /// 收容民眾
    /// </summary>
    public class Victim : SaTable, ISaTable
    {
        public Victim(IDbConnection db) : base(db, "Victim", new VictimFields())
        {
            Initialize();
        }


        public void CreateTable()
        {
            CreateTableIfNotExists(Fields);
        }

        public void Initialize()
        {
            CreateTable();
        }


        //insert & update
        public bool InsertUpdate(VictimFields[] fields)
        {
            SaDataTableFN table = null;
            foreach (VictimFields field in fields)
            {
                table = SelectWhere(string.Format("Id='{0}'", field.Id));
                if (table.Rows.Count > 0)
                {
                    if (((SaTable)this).UpdateRow(field, field) < 1)
                    {
                        return false;
                    }
                }
                else
                {
                    IDbConnection db = this.DBConnection;
                    SYSCNTM syscntm = new SYSCNTM(ref db);
                    field.Id = syscntm.GetNextVal("VIC");

                    if (((SaTable)this).InsertRow(field) < 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        //delete
        public bool Delete(VictimFields[] fields)
        {
            int affectRows = ((SaTable)this).DeleteRows(fields);
            return affectRows == fields.Length;
        }
    }

    public class VictimFields : SaFields
    {
        [SaFieldsAttribute(PrimaryKeyEnum.IsPKey)]
        [SaUIFieldsAttribute("收容民眾Id", FilterFieldEnum.FilterField)]
        public string Id { get; set; }

        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("姓名", FilterFieldEnum.FilterField)]
        public string Name { get; set; }

        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("身份證號", FilterFieldEnum.FilterField)]
        public string PatId { get; set; }

        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("其它證明文件", FilterFieldEnum.FilterField)]
        public string OtherId { get; set; }

        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("安置庇護所人數", FilterFieldEnum.FilterField)]
        public int ShelterPersons { get; set; }

        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("實際家庭人數", FilterFieldEnum.FilterField)]
        public int FamilyPersons { get; set; }

        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("血型", FilterFieldEnum.FilterField)]
        public string BloodType { get; set; }

        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("信仰", FilterFieldEnum.FilterField)]
        public string Faith { get; set; }
        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("電話", FilterFieldEnum.FilterField)]
        public string Tel { get; set; }
        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("手機", FilterFieldEnum.FilterField)]
        public string Mobile { get; set; }
        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("出生年月日", FilterFieldEnum.FilterField)]
        public string Birthday { get; set; }
        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("職業及專長", FilterFieldEnum.FilterField)]
        public string Career { get; set; }
        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("現住住址_市", FilterFieldEnum.FilterField)]
        public string LiveCity { get; set; }
        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("現住住址_區", FilterFieldEnum.FilterField)]
        public string LiveDist { get; set; }
        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("現住住址_里", FilterFieldEnum.FilterField)]
        public string LiveVil { get; set; }
        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("現住住址_路", FilterFieldEnum.FilterField)]
        public string LiveRd { get; set; }
        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("現住住址_街", FilterFieldEnum.FilterField)]
        public string LiveSt { get; set; }
        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("現住住址_段", FilterFieldEnum.FilterField)]
        public string LiveSec { get; set; }
        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("現住住址_巷", FilterFieldEnum.FilterField)]
        public string LiveLn { get; set; }

        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("現住住址_弄", FilterFieldEnum.FilterField)]
        public string LiveAly { get; set; }
        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("現住住址_號", FilterFieldEnum.FilterField)]
        public string LiveNo { get; set; }
        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("現住住址_樓", FilterFieldEnum.FilterField)]
        public string LiveF { get; set; }

        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("戶籍地址_市", FilterFieldEnum.FilterField)]
        public string HouseCity { get; set; }
        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("戶籍地址_區", FilterFieldEnum.FilterField)]
        public string HouseDist { get; set; }
        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("戶籍地址_里", FilterFieldEnum.FilterField)]
        public string HouseVil { get; set; }
        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("戶籍地址_路", FilterFieldEnum.FilterField)]
        public string HouseRd { get; set; }
        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("戶籍地址_街", FilterFieldEnum.FilterField)]
        public string HouseSt { get; set; }
        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("戶籍地址_段", FilterFieldEnum.FilterField)]
        public string HouseSec { get; set; }
        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("戶籍地址_巷", FilterFieldEnum.FilterField)]
        public string HouseLn { get; set; }

        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("戶籍地址_弄", FilterFieldEnum.FilterField)]
        public string HouseAly { get; set; }
        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("戶籍地址_號", FilterFieldEnum.FilterField)]
        public string HouseNo { get; set; }
        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("戶籍地址_樓", FilterFieldEnum.FilterField)]
        public string HouseF { get; set; }


        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("疏散時交通工具", FilterFieldEnum.FilterField)]
        public string Transportation { get; set; }
        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("受災狀況", FilterFieldEnum.FilterField)]
        public string Situation { get; set; }
        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("醫療需求", FilterFieldEnum.FilterField)]
        public string MedicalFlag { get; set; }
        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("病名", FilterFieldEnum.FilterField)]
        public string Disease { get; set; }
        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("特殊飲食需求", FilterFieldEnum.FilterField)]
        public string SpecialFood { get; set; }

        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("安置需求", FilterFieldEnum.FilterField)]
        public string ShelterNeed { get; set; }
        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("分配房號/帳篷號", FilterFieldEnum.FilterField)]
        public string VictimRoom { get; set; }
        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("安置民眾證件號碼", FilterFieldEnum.FilterField)]
        public string VictimNo { get; set; }
        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("收容所Id", FilterFieldEnum.FilterField)]
        public string Shelter_Id { get; set; }
        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("建檔日期", FilterFieldEnum.FilterField)]
        public string CreateDate { get; set; }

        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("建檔時間", FilterFieldEnum.FilterField)]
        public string CreateTime { get; set; }

        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("最後更新日期", FilterFieldEnum.FilterField)]
        public string UpdateDate { get; set; }

        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("最後更新時間", FilterFieldEnum.FilterField)]
        public string UpdateTime { get; set; }

        public VictimFields(string id, string name, string patid,
            string otherid, int shelterpersons, int familypersons,
            string bloodtype, string faith, string tel, string mobile,
            string birthday, string career, string livecity, string livedist, string livevil, string liverd,
            string livest, string livesec, string liveln, string livealy, string liveno, string livef,
            string housecity, string housedist, string housevil, string houserd,
            string housest, string housesec, string houseln, string housealy, string houseno, string housef,
            string transportation, string situation, string medicalflag, string disease, string specialfood,
            string shelterneed, string victimroom, string victimno, string shelter_id,
            string createDate, string createTime, string updateDate, string updateTime)
        {
            Id = id;
            Name = name;
            PatId = patid;
            OtherId = otherid;
            ShelterPersons = shelterpersons;
            FamilyPersons = familypersons;
            BloodType = bloodtype;
            Faith = faith;
            Tel = tel;
            Mobile = mobile;
            Birthday = birthday;
            Career = career;
            LiveCity = livecity;
            LiveDist = livedist;
            LiveVil = livevil;
            LiveRd = liverd;
            LiveSt = livest;
            LiveSec = livesec;
            LiveLn = liveln;
            LiveAly = livealy;
            LiveNo = liveno;
            LiveF = livef;
            HouseCity = housecity;
            HouseDist = housedist;
            HouseVil = housevil;
            HouseRd = houserd;
            HouseSt = housest;
            HouseSec = housesec;
            HouseLn = houseln;
            HouseAly = housealy;
            HouseNo = houseno;
            HouseF = housef;
            Transportation = transportation;
            Situation = situation;
            MedicalFlag = medicalflag;
            Disease = disease;
            SpecialFood = specialfood;
            ShelterNeed = shelterneed;
            VictimRoom = victimroom;
            VictimNo = victimno;
            Shelter_Id = shelter_id;
            CreateDate = createDate;
            CreateTime = createTime;
            UpdateDate = updateDate;
            UpdateTime = updateTime;
        }

        public VictimFields() : base() { }
    }
}
