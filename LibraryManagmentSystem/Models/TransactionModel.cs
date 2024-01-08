using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel;

namespace LibraryManagementSystem.Models
{
    public class TransactionModel
    {
        [Key]
        [DisplayName("Transaction ID")]
        public int transaction_id { get; set; }
        [DisplayName("Book ID")]
        public int book_id { get; set; }
        [DisplayName("Title")]
        public string book_title { get; set; }
        [DisplayName("User Name")]
        public string user_name { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DisplayName("Issue Date")]
        public DateTime issue_date { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DisplayName("Expected Return Date")]
        public DateTime expected_return_date { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DisplayName("Return Date")]
        public DateTime return_date { get; set; }
        [DisplayName("Past Due")]
        public int past_due_day { get; set; }
        public DateTime created_date { get; set; }
        public DateTime modified_date { get; set; }
        [DisplayName("Issue Status")]
        public string issue_status { get; set; }
        [DisplayName("Return Status")]
        public string return_status { get; set; }
    }
}
