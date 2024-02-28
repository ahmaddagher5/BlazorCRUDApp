using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorCRUDApp.Data
{
    [Table("country")]
    public class Country
    {
        [Key, Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("created_date"), DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Column("modified_date"), DataType(DataType.Date)]
        public DateTime? ModifiedDate { get; set; }
    }
}
