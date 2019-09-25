using ShelterLib;
using SportingAppFW.Data.Common;
using SportingAppFW.Data.Common.DB.Sqlite;
using SportingAppFW.SaWindows.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace ShelterHttpServer
{
    public class TableHelper
    {
        //for query
        public static string QueryData(string model, string jsonInput)
        {
            ShelterDB helper = ShelterDB.Instance;
            DbConnection db = ((SaSQLiteDBClass)helper).db;
            var serializer = new JavaScriptSerializer();
            object wheresql = serializer.DeserializeObject(jsonInput);
            string filter = wheresql.ToString();

            if (db.State != ConnectionState.Open)
            {
                db.Open();
            }
            SaDataTableFN table = null;
            string json = string.Empty;
            if (model == "disaster")
            {
                Disaster disaster = new Disaster(db);
                table = disaster.SelectWhere(filter);
                SaFields[] fields = disaster.ToFields(table);
                json = new JavaScriptSerializer().Serialize(fields);
            }
            else if (model == "family")
            {
                Family family = new Family(db);
                table = family.SelectWhere(filter);
                SaFields[] fields = family.ToFields(table);
                json = new JavaScriptSerializer().Serialize(fields);
            }
            else if (model == "familymissing")
            {
                FamilyMissing familymissing = new FamilyMissing(db);
                table = familymissing.SelectWhere(filter);
                SaFields[] fields = familymissing.ToFields(table);
                json = new JavaScriptSerializer().Serialize(fields);
            }
            else if (model == "materialuse")
            {
                MaterialUse materialuse = new MaterialUse(db);
                table = materialuse.SelectWhere(filter);
                SaFields[] fields = materialuse.ToFields(table);
                json = new JavaScriptSerializer().Serialize(fields);
            }
            else if (model == "relyrelation")
            {
                RelyRelation relyrelation = new RelyRelation(db);
                table = relyrelation.SelectWhere(filter);
                SaFields[] fields = relyrelation.ToFields(table);
                json = new JavaScriptSerializer().Serialize(fields);
            }
            else if (model == "shelter")
            {
                Shelter shelter = new Shelter(db);
                table = shelter.SelectWhere(filter);
                SaFields[] fields = shelter.ToFields(table);
                json = new JavaScriptSerializer().Serialize(fields);
            }
            else if (model == "victim")
            {
                Victim victim = new Victim(db);
                table = victim.SelectWhere(filter);
                SaFields[] fields = victim.ToFields(table);
                json = new JavaScriptSerializer().Serialize(fields);

            }
            return json;
        }

        //for add,update
        public static bool InsertUpdate(string model, string jsonInput)
        {
            ShelterDB helper = ShelterDB.Instance;
            DbConnection db = ((SaSQLiteDBClass)helper).db;

            if (db.State != ConnectionState.Open)
            {
                db.Open();
            }
            var serializer = new JavaScriptSerializer();

            DbTransaction transaction = db.BeginTransaction();
            try
            {
                if (model == "disaster")
                {
                    DisasterFields[] disasters = serializer.Deserialize<DisasterFields[]>(jsonInput);
                    Disaster disaster = new Disaster(db);
                    return disaster.InsertUpdate( disasters);
                }
                else if (model == "family")
                {
                    FamilyFields[] families = serializer.Deserialize<FamilyFields[]>(jsonInput);
                    Family family = new Family(db);
                    return family.InsertUpdate(families);
                }
                else if (model == "familymissing")
                {
                    FamilyMissingFields[] familymissings = serializer.Deserialize<FamilyMissingFields[]>(jsonInput);
                    FamilyMissing familymissing = new FamilyMissing(db);
                    return familymissing.InsertUpdate( familymissings);
                }
                else if (model == "materialuse")
                {
                    MaterialUseFields[] materialuses = serializer.Deserialize<MaterialUseFields[]>(jsonInput);
                    MaterialUse materialuse = new MaterialUse(db);
                    return materialuse.InsertUpdate( materialuses);
                }
                else if (model == "relyrelation")
                {
                    RelyRelationFields[] relyrelations = serializer.Deserialize<RelyRelationFields[]>(jsonInput);
                    RelyRelation relyrelation = new RelyRelation(db);
                    return relyrelation.InsertUpdate( relyrelations);
                }
                else if (model == "shelter")
                {
                    ShelterFields[] shelters = serializer.Deserialize<ShelterFields[]>(jsonInput);
                    Shelter shelter = new Shelter(db);
                    return shelter.InsertUpdate( shelters);
                }
                else if (model == "victim")
                {
                    VictimFields[] victims = serializer.Deserialize<VictimFields[]>(jsonInput);
                    Victim victim = new Victim(db);
                    return victim.InsertUpdate( victims);
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return false;
            }
            finally
            {
                transaction.Commit();
            }
        }

        public static bool Delete(string model, string jsonInput)
        {
            ShelterDB helper = ShelterDB.Instance;
            DbConnection db = ((SaSQLiteDBClass)helper).db;

            if (db.State != ConnectionState.Open)
            {
                db.Open();
            }
            var serializer = new JavaScriptSerializer();

            DbTransaction transaction = db.BeginTransaction();
            try
            {
                if (model == "disaster")
                {
                    DisasterFields[] disasters = serializer.Deserialize<DisasterFields[]>(jsonInput);
                    Disaster disaster = new Disaster(db);
                    return disaster.Delete( disasters);
                }
                else if (model == "family")
                {
                    FamilyFields[] families = serializer.Deserialize<FamilyFields[]>(jsonInput);
                    Family family = new Family(db);
                    return family.Delete(families);
                }
                else if (model == "familymissing")
                {
                    FamilyMissingFields[] familymissings = serializer.Deserialize<FamilyMissingFields[]>(jsonInput);
                    FamilyMissing familymissing = new FamilyMissing(db);
                    return familymissing.Delete( familymissings);
                }
                else if (model == "materialuse")
                {
                    MaterialUseFields[] materialuses = serializer.Deserialize<MaterialUseFields[]>(jsonInput);
                    MaterialUse materialuse = new MaterialUse(db);
                    return materialuse.Delete( materialuses);
                }
                else if (model == "relyrelation")
                {
                    RelyRelationFields[] relyrelations = serializer.Deserialize<RelyRelationFields[]>(jsonInput);
                    RelyRelation relyrelation = new RelyRelation(db);
                    return relyrelation.Delete( relyrelations);
                }
                else if (model == "shelter")
                {
                    ShelterFields[] shelters = serializer.Deserialize<ShelterFields[]>(jsonInput);
                    Shelter shelter = new Shelter(db);
                    return shelter.Delete( shelters);
                }
                else if (model == "victim")
                {
                    VictimFields[] victims = serializer.Deserialize<VictimFields[]>(jsonInput);
                    Victim victim = new Victim(db);
                    return victim.Delete( victims);
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return false;
            }
            finally
            {
                transaction.Commit();
            }
        }
    }
}
