using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlazorCRUDApp.Data
{
    [Table("students")]
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; } = "";

        [ForeignKey("ClassId")]
        public Class Class { get; set; }

        [Column("class_id")]
        public int ClassId { get; set; }

        [ForeignKey("CountryId")]
        public Country Country { get; set; }

        [Column("country_id")]
        public int CountryId { get; set; }
        [Column("date_of_birth"), DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        [Column("created_date"), DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Column("modified_date"), DataType(DataType.Date)]
        public DateTime? ModifiedDate { get; set; }

        public string GetClassName()
        {
            return Class != null ? Class.ClassName : "";
        }
        public string GetCountryName()
        {
            return Country != null ? Country.Name : "";
        }
    }
}
