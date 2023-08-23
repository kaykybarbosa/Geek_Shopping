using GeekShopping.Email.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.Email.Model
{
    [Table("EMAIL_LOGS")]
    public class EmailLog : BaseEntity
    {
        [Column("EMAIL")]
        public string Email { get; set; }   
        
        [Column("LOG")]
        public string Log { get; set; }
        
        [Column("SENT_DATE")]
        public DateTime SentDate { get; set; }
    }
}