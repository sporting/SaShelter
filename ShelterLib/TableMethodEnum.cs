using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShelterLib
{
    public enum TableMethodEnum
    {
        Query, Update, Delete,None
    }

    public class TableMethodMapping
    {
        public static TableMethodEnum Mapping(string text)
        {
            text = text.ToLower();
            if (text == "query")
            {
                return TableMethodEnum.Query;
            }
            else if (text == "update")
            {
                return TableMethodEnum.Update;
            }
            else if (text == "delete")
            {
                return TableMethodEnum.Delete;
            }
            else
            {
                return TableMethodEnum.None;
            }
        }
    }
}
