using SportingAppFW.Data.Common;
using SportingAppFW.SaWindows.Data;
using System;
using System.Data;

namespace ShelterLib
{
    /// <summary>
    /// 物資領用表
    /// </summary>
    public class MaterialUse: SaTable,ISaTable
    {
         public MaterialUse(IDbConnection db):base(db, "MaterialUse", new MaterialUseFields())
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
        public bool InsertUpdate(MaterialUseFields[] fields)
        {
            SaDataTableFN table = null;
            foreach (MaterialUseFields field in fields)
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
                    object res = shelterDb.ExecuteSQLFetchFirstData(string.Format("SELECT MAX(SeqNo) FROM MaterialUse WHERE Victim_Id = {0}", field.Victim_Id));
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
        public bool Delete(MaterialUseFields[] fields)
        {
            int affectRows = ((SaTable)this).DeleteRows(fields);
            return affectRows == fields.Length;
        }
    }

    public class MaterialUseFields : SaFields
    {
        [SaFieldsAttribute(PrimaryKeyEnum.IsPKey)]
        [SaUIFieldsAttribute("收容民眾Id", FilterFieldEnum.FilterField)]
        public string Victim_Id { get; set; }

        [SaFieldsAttribute(PrimaryKeyEnum.IsPKey)]
        [SaUIFieldsAttribute("序號", FilterFieldEnum.FilterField)]
        public decimal SeqNo { get; set; }

        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("物資內容", FilterFieldEnum.FilterField)]
        public string Material { get; set; }

        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("物資數量", FilterFieldEnum.FilterField)]
        public double Amount { get; set; }

        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("領用日期", FilterFieldEnum.FilterField)]
        public string UseDate { get; set; }

        [SaFieldsAttribute()]
        [SaUIFieldsAttribute("領用時間", FilterFieldEnum.FilterField)]
        public string UseTime { get; set; }

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

        public MaterialUseFields(string victim_id, decimal seqno,
            string material,double amount,string usedate,string usetime,
            string createDate,string createTime,string updateDate,string updateTime  )
        {
            Victim_Id = victim_id;
            SeqNo = seqno;
            Material = material;
            Amount = amount;
            UseDate = usedate;
            UseTime = usetime;
            CreateDate = createDate;
            CreateTime = createTime;
            UpdateDate = updateDate;
            UpdateTime = updateTime;
        }

        public MaterialUseFields():base(){}
    }
}
