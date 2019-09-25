using SportingAppFW.Data.Common;
using SportingAppFW.Data.Common.DB.Sqlite;
using SportingAppFW.SaWindows.Data;
using System.Data;

namespace ShelterLib
{
    /// <summary>
    /// 收容所
    /// </summary>
    public class Shelter : SaTable,ISaTable
    {
         public Shelter(IDbConnection db):base(db, "Shelter", new ShelterFields())
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
        public bool InsertUpdate(ShelterFields[] fields)
        {
            SaDataTableFN table = null;
            foreach (ShelterFields field in fields)
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
                    field.Id = syscntm.GetNextVal("SHE");

                    if (((SaTable)this).InsertRow(field) < 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        //delete
        public bool Delete(ShelterFields[] fields)
        {
            int affectRows = ((SaTable)this).DeleteRows(fields);
            return affectRows == fields.Length;
        }
    }

    public class ShelterFields : SaFields
    {
        [SaFieldsAttribute(PrimaryKeyEnum.IsPKey)]
        [SaUIFieldsAttribute("收容所Id", FilterFieldEnum.FilterField)]
        public string Id { get; set; }

        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("收容所編號", FilterFieldEnum.FilterField)]
        public string Tag { get; set; }

        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("收容所名稱", FilterFieldEnum.FilterField)]
        public string Name { get; set; }

        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("收容所開設日期", FilterFieldEnum.FilterField)]
        public string ShelterDate { get; set; }

        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("對應災害Id", FilterFieldEnum.FilterField)]
        public string Disaster_Id { get; set; }

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

        public ShelterFields(string id, string name,string tag,string shelterDate , string disasterId,string createDate,string createTime,string updateDate,string updateTime  )
        {
            Id = id;
            Name = name;
            Tag = tag;
            ShelterDate = ShelterDate;
            Disaster_Id = disasterId;
            CreateDate = createDate;
            CreateTime = createTime;
            UpdateDate = updateDate;
            UpdateTime = updateTime;
        }

        public ShelterFields():base(){}
    }
}
