using SportingAppFW.Data.Common;
using SportingAppFW.SaWindows.Data;
using System;
using System.Data;

namespace ShelterLib
{
    /// <summary>
    /// 可依親友
    /// </summary>
    public class RelyRelation : SaTable, ISaTable
    {
        public RelyRelation(IDbConnection db) : base(db, "RelyRelation", new RelyRelationFields())
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
        public bool InsertUpdate(RelyRelationFields[] fields)
        {
            SaDataTableFN table = null;
            foreach (RelyRelationFields field in fields)
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
                    object res = shelterDb.ExecuteSQLFetchFirstData(string.Format("SELECT MAX(SeqNo) FROM RelyRelation WHERE Victim_Id = {0}", field.Victim_Id));
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
        public bool Delete(RelyRelationFields[] fields)
        {
            int affectRows = ((SaTable)this).DeleteRows(fields);
            return affectRows == fields.Length;
        }
    }

    public class RelyRelationFields : SaFields
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
        [SaUIFieldsAttribute("職業", FilterFieldEnum.FilterField)]
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

        public RelyRelationFields(string victim_id, decimal seqno, string name,
            string relation, double age, string sex, string career,
            string livecity, string livedist, string livevil, string liverd,
            string livest, string livesec, string liveln, string livealy, string liveno, string livef,
            string remark,
            string createDate, string createTime, string updateDate, string updateTime)
        {
            Victim_Id = victim_id;
            SeqNo = seqno;
            Name = name;
            Relation = relation;
            Age = age;
            Sex = sex;
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
            Remark = remark;
            CreateDate = createDate;
            CreateTime = createTime;
            UpdateDate = updateDate;
            UpdateTime = updateTime;
        }

        public RelyRelationFields() : base() { }
    }
}
