﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AngApp.Models
{
    public class EmployeeModel
    {
        public int EM_ID { get; set; }
        public string EM_CODE { get; set; }
        public string EM_NAME { get; set; }
        public string EM_LASTNAME { get; set; }
        public string EM_ADDRESS { get; set; }
        public string EM_PHONE { get; set; }
        public string EM_GENDER { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> EM_DOB { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> EM_DOJ { get; set; }
        public string EM_DOBC { get; set; }
        public string EM_DOJC { get; set; }
        public string EM_PHOTO { get; set; }
        public string EM_COUNTRY { get; set; }
        public Nullable<bool> EM_ACTIVE { get; set; }
        public string COMPCD { get; set; }
        public string EM_GEN { get; set; }
        public string EM_MAIL { get; set; }
        public string EM_USERNAME { get; set; }
        public string EM_PASSWORD { get; set; }
        public string EM_DEPT { get; set; }
    }
}