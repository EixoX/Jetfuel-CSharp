using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data.SqlServer
{
    [Table(DataName = "all_objects")]
    public class SqlObject
    {
        [Column]
        public object object_id { get; set; }
        public object name { get; set; }
        public object column_id { get; set; }
        public object system_type_id { get; set; }
        public object user_type_id { get; set; }
        public object max_length { get; set; }
        public object precision { get; set; }
        public object scale { get; set; }
        public object collation_name { get; set; }
        public object is_nullable { get; set; }
        public object is_ansi_padded { get; set; }
        public object is_rowguidcol { get; set; }
        public object is_identity { get; set; }
        public object is_computed { get; set; }
        public object is_filestream { get; set; }
        public object is_replicated { get; set; }
        public object is_non_sql_subscribed { get; set; }
        public object is_merge_published { get; set; }
        public object is_dts_replicated { get; set; }
        public object is_xml_document { get; set; }
        public object xml_collection_id { get; set; }
        public object default_object_id { get; set; }
        public object rule_object_id { get; set; }
        public object is_sparse { get; set; }
        public object is_column_set { get; set; }
    }
}
