using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace P2BPowerChecker.Models
{
    public class PCMessenger
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string PhoneNumber { get; set; }
        public string SmsMessage { get; set; }
    }
}
