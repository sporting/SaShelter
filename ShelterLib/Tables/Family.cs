using SportingAppFW.Data.Common;
using SportingAppFW.Data.Common.DB.Sqlite;
using SportingAppFW.SaWindows.Data;
using System;
using System.Data;

namespace ShelterLib
{
    /// <summary>
    /// 家庭成員表
    /// </summary>
    public class Family : SaTable, ISaTable
    {
        public Family(IDbConnection db) : base(db, "Family", new FamilyFields())
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
        public bool InsertUpdate(FamilyFields[] fields)
        {
            SaDataTableFN table = null;
            foreach (FamilyFields field in fields)
            {
                table = SelectWhere(string.Format("Victim_Id='{0}' and SeqNo={1}", field.Victim_Id, field.SeqNo));
                if (table.Rows.Count > 0)
                {
                    if (((SaTable)this).UpdateRow(field, field) < 1)
                    {
                        return false;
                    }
                }
                else
                {
                    ShelterDB shelterDb = ShelterDB.Instance;
                    object res = shelterDb.ExecuteSQLFetchFirstData(string.Format("SELECT MAX(SeqNo) FROM Family WHERE Victim_Id = {0}", field.Victim_Id));
                    field.SeqNo = res == null ? 1 : Convert.ToDecimal(res) + 1;

                    if (((SaTable)this).InsertRow(field) < 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        //delete
        public bool Delete(FamilyFields[] fields)
        {
            int affectRows = ((SaTable)this).DeleteRows(fields);
            return affectRows == fields.Length;
        }
    }
    public class FamilyFields : SaFields
    {
        [SaFieldsAttribute(PrimaryKeyEnum.IsPKey)]
        [SaUIFieldsAttribute("收容民眾Id", FilterFieldEnum.FilterField)]
        public string Victim_Id { get; set; }

        [SaFieldsAttribute(PrimaryKeyEnum.IsPKey)]
        [SaUIFieldsAttribute("序號", FilterFieldEnum.FilterField)]
        public decimal SeqNo { get; set; }

        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("姓名", FilterFieldEnum.FilterField)]
        public string Name { get; set; }

        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("關係", FilterFieldEnum.FilterField)]
        public string Relation { get; set; }

        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("年齡", FilterFieldEnum.FilterField)]
        public double Age { get; set; }

        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("性別", FilterFieldEnum.FilterField)]
        public string Sex { get; set; }
        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("房間號/床號", FilterFieldEnum.FilterField)]
        public string VictimRoom { get; set; }

        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("報到日期", FilterFieldEnum.FilterField)]
        public string CheckInDate { get; set; }

        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("離開日期", FilterFieldEnum.FilterField)]
        public string LeaveDate { get; set; }
        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("重新安置處地址", FilterFieldEnum.FilterField)]
        public string ShelterAddress { get; set; }
        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("安置處電話", FilterFieldEnum.FilterField)]
        public string ShelterPhone { get; set; }

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

        public FamilyFields(string victim_id, decimal seqno, string name,
            string relation, double age, string sex, string victimroom,
            string checkindate, string leavedate, string shelteraddress, string shelterphone,
            string createDate, string createTime, string updateDate, string updateTime)
        {
            Victim_Id = victim_id;
            SeqNo = seqno;
            Name = name;
            Relation = relation;
            Age = age;
            Sex = sex;
            VictimRoom = victimroom;
            CheckInDate = checkindate;
            LeaveDate = leavedate;
            ShelterAddress = shelteraddress;
            ShelterPhone = shelterphone;
            CreateDate = createDate;
            CreateTime = createTime;
            UpdateDate = updateDate;
            UpdateTime = updateTime;
        }

        public FamilyFields() : base() { }
    }
}
