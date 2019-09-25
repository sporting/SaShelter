using SportingAppFW.Data.Common;
using SportingAppFW.Data.Common.DB.Sqlite;
using SportingAppFW.SaWindows.Data;
using System.Data;
using System.Data.Common;

namespace ShelterLib
{
    /// <summary>
    /// 災害發生表
    /// </summary>
    public class Disaster: SaTable,ISaTable
    {
         public Disaster(IDbConnection db):base(db,"Disaster",new DisasterFields())
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
        public bool InsertUpdate(DisasterFields[] fields)
        {
            SaDataTableFN table = null;
            foreach (DisasterFields field in fields)
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
                    field.Id = syscntm.GetNextVal("DIS");

                    if (((SaTable)this).InsertRow(field) < 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        //delete
        public bool Delete(DisasterFields[] fields)
        {
            int affectRows = ((SaTable)this).DeleteRows(fields);
            return affectRows == fields.Length;
        }
    }

    public class DisasterFields : SaFields
    {
        [SaFieldsAttribute(PrimaryKeyEnum.IsPKey)]
        [SaUIFieldsAttribute("災害Id", FilterFieldEnum.FilterField)]
        public string Id { get; set; }

        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("災害名稱", FilterFieldEnum.FilterField)]
        public string Name { get; set; }

        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("災害日期", FilterFieldEnum.FilterField)]
        public string OccurDate { get; set; }

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

        public DisasterFields(string id, string name,string occurDate,string createDate,string createTime,string updateDate,string updateTime  )
        {
            Id = id;
            Name = name;
            OccurDate = occurDate;
            CreateDate = createDate;
            CreateTime = createTime;
            UpdateDate = updateDate;
            UpdateTime = updateTime;
        }

        public DisasterFields():base(){}
    }
}
