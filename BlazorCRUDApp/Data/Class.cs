using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlazorCRUDApp.Data
{
    [Table("class")]
    public class Class
    {
        [Key, Column("id")]
        public int Id { get; set; }
        [Column("class_name")]
        public string ClassName { get; set; }
        [Column("created_date"), DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Column("modified_date"), DataType(DataType.Date)]
        public DateTime? ModifiedDate { get; set; }
    }
}
