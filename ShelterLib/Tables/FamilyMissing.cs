using SportingAppFW.Data.Common;
using SportingAppFW.SaWindows.Data;
using System;
using System.Data;

namespace ShelterLib
{
    /// <summary>
    /// 家庭中是否有其他成員失蹤
    /// </summary>
    public class FamilyMissing : SaTable, ISaTable
    {
        public FamilyMissing(IDbConnection db) : base(db, "FamilyMissing", new FamilyMissingFields())
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
        public bool InsertUpdate(FamilyMissingFields[] fields)
        {
            SaDataTableFN table = null;
            foreach (FamilyMissingFields field in fields)
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
                    object res = shelterDb.ExecuteSQLFetchFirstData(string.Format("SELECT MAX(SeqNo) FROM FamilyMissing WHERE Victim_Id = {0}", field.Victim_Id));
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
        public bool Delete(FamilyMissingFields[] fields)
        {
            int affectRows = ((SaTable)this).DeleteRows(fields);
            return affectRows == fields.Length;
        }
    }

    public class FamilyMissingFields : SaFields
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
        [SaUIFieldsAttribute("最後看見日期", FilterFieldEnum.FilterField)]
        public string SeenDate { get; set; }
        
        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("最後看見時間", FilterFieldEnum.FilterField)]
        public string SeenTime { get; set; }

        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("最後看見地點", FilterFieldEnum.FilterField)]
        public string SeenPlace { get; set; }
        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("備註", FilterFieldEnum.FilterField)]
        public string Remark { get; set; }


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

        public FamilyMissingFields(string victim_id, decimal seqno, string name,
            string relation, double age, string sex, string seendate,
            string seentime,string seenplace,string remark,
            string createDate, string createTime, string updateDate, string updateTime)
        {
            Victim_Id = victim_id;
            SeqNo = seqno;
            Name = name;
            Relation = relation;
            Age = age;
            Sex = sex;
            SeenDate = seendate;
            SeenTime = seentime;
            SeenPlace = seenplace;
            Remark = remark;
            CreateDate = createDate;
            CreateTime = createTime;
            UpdateDate = updateDate;
            UpdateTime = updateTime;
        }

        public FamilyMissingFields() : base() { }
    }
}
